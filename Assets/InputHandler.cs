using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Handles input in MissionMap scene. Can zoom and navigate.</summary>
public class InputHandler : MonoBehaviour {
    /// <summary>Disables camera movement when true</summary>
    public static bool BlockCameraMovement;

    public static bool MoveCamForBuilding;

    /// <summary>The distance which the user must drag until a drag is registered as such versus a click</summary>
    public float DeadZoneDrag;

    /// <summary>The maximum position the camera may have without zoom</summary>
    public Vector3 CameraMax;

    /// <summary>The minimum position the camera may have without zoom</summary>
    public Vector3 CameraMin;

    /// <summary>The minimum zoom the camera may have</summary>
    public float MinZoom;

    /// <summary>The maximum zoom the camera may have</summary>
    public float MaxZoom;

    /// <summary>Determines how fast the camera will zoom</summary>
    public float OrthoZoomSpeed = 0.5f;

    public bool IsMissionMap;

    public float BuildCamMoveSpeed = 1f;

    public float DoubleTapMaxLatency = 0.2f;

    public BaseSwitcher BaseSwitch;

    /// <summary>The position where the touch began</summary>
    private Vector2 startPos;

    /// <summary>Marks wether the finger moved after the touch began</summary>
    private bool movedDuringTouch = false;

    /// <summary>Marks wether the map movement is blocked (e.g. during zoom)</summary>
    private bool blockMapMovement = false;

    /// <summary>Saves the ray from the position of the input when the touch started</summary>
    private Ray lastPosRay;

    private Ray touchRay;
    private int layerMask;
    private RaycastHit hitInformation;
    private float doubleTapTimer;

    /// <summary>Update is called once per frame</summary>
    private void Update() {
        if (Input.touchCount == 2 && !EventSystem.current.IsPointerOverGameObject()) {
            this.blockMapMovement = true;

            // Store both touches.
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ... change the orthographic size based on the change in distance between the touches.
            Camera.main.orthographicSize += deltaMagnitudeDiff * this.OrthoZoomSpeed;
            Camera.main.orthographicSize = Mathf.Min(this.MaxZoom, Mathf.Max(this.MinZoom, Camera.main.orthographicSize));

            // Make sure the orthographic size never drops below zero.
        } else if (!EventSystem.current.IsPointerOverGameObject()) {
            foreach (var touch in Input.touches) {
                this.HandleTouch(touch.fingerId, touch.position, touch.phase);
            }
        }

        // Simulate touch events from mouse events
        if (Input.touchCount != 0) return;
        if (Input.GetMouseButtonDown(0)) {
            this.HandleTouch(-1, Input.mousePosition, TouchPhase.Began);
        }

        if (Input.GetMouseButton(0)) {
            this.HandleTouch(-1, Input.mousePosition, TouchPhase.Moved);
        }

        if (Input.GetMouseButtonUp(0)) {
            this.HandleTouch(-1, Input.mousePosition, TouchPhase.Ended);
        }
    }

    /// <summary>
    /// Method handles all touch inputs on MissionMap, splits into TouchPhases and contains the movement logic
    /// </summary>
    /// <param name="touchFingerId">Which finger does the input come from (Counted up)</param>
    /// <param name="touchPosition">Position on screen</param>
    /// <param name="touchPhase">Latest TouchPhase</param>
    private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase) {
        switch (touchPhase) {
            case TouchPhase.Began:
                if (EventSystem.current.IsPointerOverGameObject(touchFingerId)) {
                    this.blockMapMovement = true;
                } else {
                    startPos = touchPosition;
                    lastPosRay = Camera.main.ScreenPointToRay(startPos);
                    // startPosCamera = transform.position;
                    this.movedDuringTouch = false;
                }

                break;
            case TouchPhase.Moved:
                if (!this.blockMapMovement) {
                    if (!this.movedDuringTouch && Vector2.Distance(this.startPos, touchPosition) > this.DeadZoneDrag) {
                        this.movedDuringTouch = true;
                    }

                    if (this.movedDuringTouch && !BlockCameraMovement) {
                        var curPosRay = Camera.main.ScreenPointToRay(touchPosition);
                        RaycastHit curHitInfo;
                        if (Physics.Raycast(curPosRay, out curHitInfo, 2000.0f, LayerMask.GetMask("Plane"))) {
                            RaycastHit lastHitInfo;
                            if (Physics.Raycast(this.lastPosRay, out lastHitInfo, 2000.0f, LayerMask.GetMask("Plane"))) {
                                var deltaPos = curHitInfo.point - lastHitInfo.point;
                                deltaPos.y = 0;
                                deltaPos *= -1;
                                this.MoveInBounds(transform.position + deltaPos);
                            }
                        }
                    }

                    if (this.movedDuringTouch && MoveCamForBuilding) {
                        var screenPointPercentage = new Vector2 {
                            x = Mathf.Min(Mathf.Max(touchPosition.x / (float)Camera.main.pixelWidth, 0f), 1f),
                            y = Mathf.Min(Mathf.Max(touchPosition.y / (float)Camera.main.pixelHeight, 0f), 1f)
                        };

                        var moveZoneX = new Vector2(0.3f, 0.7f);
                        var moveZoneY = new Vector2(0.3f, 0.7f);
                        var moveDirection = new Vector2();
                        if (screenPointPercentage.x > moveZoneX.y) {
                            moveDirection.x = screenPointPercentage.x - moveZoneX.y;
                        } else if (screenPointPercentage.x < moveZoneX.x) {
                            moveDirection.x = screenPointPercentage.x - moveZoneX.x;
                        }

                        if (screenPointPercentage.y > moveZoneY.y) {
                            moveDirection.y = screenPointPercentage.y - moveZoneY.y;
                        } else if (screenPointPercentage.y < moveZoneY.x) {
                            moveDirection.y = screenPointPercentage.y - moveZoneY.x;
                        }
                        var camMove = Camera.main.transform.right * moveDirection.x;
                        camMove += Camera.main.transform.up * moveDirection.y * 3;
                        this.MoveInBounds(transform.position + camMove * BuildCamMoveSpeed);
                    }
                }

                break;
            case TouchPhase.Ended:
                if (this.blockMapMovement) {
                    this.blockMapMovement = false;
                } else if (!this.movedDuringTouch) {
                    if (!EventSystem.current.IsPointerOverGameObject(0) && !EventSystem.current.IsPointerOverGameObject()) {
                        this.touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (IsMissionMap) {
                            this.layerMask = LayerMask.GetMask("MissionLocation");
                            Physics.Raycast(this.touchRay.origin, this.touchRay.direction, out this.hitInformation, 3000.0f, this.layerMask);
                            if (this.hitInformation.collider != null) {
                                this.hitInformation.collider.gameObject.GetComponent<MissionDetails>().OnClick();
                            }
                        } else {
                            if (Time.time - doubleTapTimer < DoubleTapMaxLatency) {
                                this.layerMask = LayerMask.GetMask("Buildings");
                                Physics.Raycast(this.touchRay.origin, this.touchRay.direction, out this.hitInformation, 3000.0f, this.layerMask);
                                //Activate Sell/Move:
                                //Give money + energy back
                                //Destroy
                                //Build a Building
                                if (this.hitInformation.collider != null && this.hitInformation.collider.gameObject.GetComponent<BuildingManager>().CanBeDestroyed) {
                                    var buildingManager = this.hitInformation.collider.gameObject.GetComponent<BuildingManager>();//.OnClick();
                                    var id = buildingManager.BuildingID;
                                    var buildCost = buildingManager.BuildCost;
                                    var costEnergy = buildingManager.CostEnergy;
                                    Destroy(this.hitInformation.collider.gameObject);
                                    BaseSwitch.GetBuilder().BuildABuilding(id, buildCost, costEnergy);
                                }
                                //Update dependancies
                            } else {
                                doubleTapTimer = Time.time;
                            }
                        }
                    }
                }

                break;
            case TouchPhase.Stationary:
                break;
            case TouchPhase.Canceled:
                break;
            default:
                throw new ArgumentOutOfRangeException("touchPhase", touchPhase, null);
        }
    }

    /// <summary>
    /// Moves the transform (camera) inside of the given bounds
    /// </summary>
    /// <param name="newCamPosition">Position that the user ties to move the camera to</param>
    private void MoveInBounds(Vector3 newCamPosition) {
        transform.position = newCamPosition;
        transform.localPosition = Vector3.Min(Vector3.Max(transform.localPosition, this.CameraMin), this.CameraMax);
    }
}
