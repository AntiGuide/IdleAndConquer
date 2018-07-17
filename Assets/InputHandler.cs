using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Handles input in MissionMap scene. Can zoom and navigate.</summary>
public class InputHandler : MonoBehaviour {
    /// <summary>Disables camera movement when true</summary>
    public static bool BlockCameraMovement;

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

    /// <summary>Update is called once per frame</summary>
    private void Update() {
        // TODO Mobile Pointer 0
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
        // TODO Mobile Pointer 0
        if (Input.touchCount == 0) {
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
                if (!this.blockMapMovement && !BlockCameraMovement) {
                    if (!this.movedDuringTouch && Vector2.Distance(this.startPos, touchPosition) > this.DeadZoneDrag) {
                        this.movedDuringTouch = true;
                    }

                    if (this.movedDuringTouch) {
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
                        }
                    }
                }

                break;
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
