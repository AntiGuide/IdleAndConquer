using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Handles input in MissionMap scene. Can zoom and navigate.</summary>
public class InputHandler : MonoBehaviour {
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

    /// <summary>Reference to the MainMenueController</summary>
    public MainMenueController MainMenueControll;

    /// <summary>The position where the touch began</summary>
    private Vector2 startPos;

    /// <summary>The position where the camera was when the touch began</summary>
    private Vector3 startPosCamera;

    /// <summary>Marks wether the finger moved after the touch began</summary>
    private bool movedDuringTouch = false;

    /// <summary>Marks wether the map movement is blocked (e.g. during zoom)</summary>
    private bool blockMapMovement = false;

    private Ray lastPosRay;

    /// <summary>Use this for initialization</summary>
    void Start() {
    }

    /// <summary>Update is called once per frame</summary>
    void Update() {
        // TODO Mobile Pointer 0
        if (Input.touchCount == 2 && !EventSystem.current.IsPointerOverGameObject()) {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            Camera tmpCam = gameObject.GetComponent<Camera>();
            if (tmpCam.orthographic) {
                tmpCam.orthographicSize = Mathf.Max(Mathf.Min(tmpCam.orthographicSize + deltaMagnitudeDiff * 0.5f, this.MinZoom), this.MaxZoom);
                this.MoveInBounds(transform.position);
            }
        } else if (!EventSystem.current.IsPointerOverGameObject()) {
            foreach (Touch touch in Input.touches) {
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
                    startPosCamera = transform.position;
                    this.movedDuringTouch = false;
                }
                
                break;
            case TouchPhase.Moved:
                if (!this.blockMapMovement) {
                    if (!this.movedDuringTouch && Vector2.Distance(this.startPos, touchPosition) > this.DeadZoneDrag) {
                        this.movedDuringTouch = true;
                    }

                    if (this.movedDuringTouch) {
                        Ray curPosRay = Camera.main.ScreenPointToRay(touchPosition);
                        RaycastHit curHitInfo;
                        if (Physics.Raycast(curPosRay, out curHitInfo, 2000.0f, LayerMask.GetMask("Plane"))) {
                            RaycastHit lastHitInfo;
                            if (Physics.Raycast(lastPosRay, out lastHitInfo, 2000.0f, LayerMask.GetMask("Plane"))) {
                                Vector3 deltaPos = curHitInfo.point - lastHitInfo.point;
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
                } else if (!movedDuringTouch) {
                    Ray touchRay;
                    if (Camera.allCamerasCount > 1) {
                        touchRay = Camera.allCameras[1].ScreenPointToRay(touchPosition);
                    } else {
                        touchRay = Camera.allCameras[0].ScreenPointToRay(touchPosition);
                    }
                    
                    int layerMask = LayerMask.GetMask("MissionLocation");
                    RaycastHit hitInformation;
                    Physics.Raycast(touchRay.origin, touchRay.direction, out hitInformation, 700.0f, layerMask);
                    if (hitInformation.collider != null) {
                        if (hitInformation.collider.tag.Equals("MissionLocation")) {
                            Debug.Log(hitInformation.collider.GetComponent<MissionDetails>().MissionName);
                            MissionDetails missionToLoad = hitInformation.collider.GetComponent<MissionDetails>();
                            MainMenueControll.ToggleMenue(1);
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
        transform.position = Vector3.Min(Vector3.Max(newCamPosition, this.CameraMin), this.CameraMax);
    }
}
