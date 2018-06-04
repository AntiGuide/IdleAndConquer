using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>Handles input in MissionMap scene. Can zoom and navigate.</summary>
public class InputHandler : MonoBehaviour {
    /// <summary>The distance which the user must drag until a drag is registered as such versus a click</summary>
    public float DeadZoneDrag;

    /// <summary>The maximum X position the camera may have without zoom</summary>
    public float CameraMaxX;

    /// <summary>The minimum X position the camera may have without zoom</summary>
    public float CameraMinX;

    /// <summary>The maximum Z position the camera may have without zoom</summary>
    public float CameraMaxZ;

    /// <summary>The minimum Z position the camera may have without zoom</summary>
    public float CameraMinZ;

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

    /// <summary>Worldposition at the position of maximum screen height and length</summary>
    private Vector3 screenSize;

    /// <summary>Ortographic size after start</summary>
    private float startOrtographicSize;

    /// <summary>Variable in relation to the screen scale</summary>
    private float screenScale;

    /// <summary>Use this for initialization</summary>
    void Start() {
        this.screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        this.startOrtographicSize = gameObject.GetComponent<Camera>().orthographicSize;
        this.screenScale = 4500f / Screen.width;
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
            if (gameObject.GetComponent<Camera>().orthographic) {
                gameObject.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * 0.5f;
                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Max(gameObject.GetComponent<Camera>().orthographicSize, this.MaxZoom);
                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Min(gameObject.GetComponent<Camera>().orthographicSize, this.MinZoom);

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
                this.HandleTouch(10, Input.mousePosition, TouchPhase.Began);
            }

            if (Input.GetMouseButton(0)) {
                this.HandleTouch(10, Input.mousePosition, TouchPhase.Moved);
            }

            if (Input.GetMouseButtonUp(0)) {
                this.HandleTouch(10, Input.mousePosition, TouchPhase.Ended);
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
                if (EventSystem.current.IsPointerOverGameObject()) {
                    this.blockMapMovement = true;
                } else {
                    startPos = touchPosition;
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
                        this.MoveInBounds(touchPosition);
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
                            Debug.Log(hitInformation.collider.GetComponent<MissionDetails>().missionName);
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
    /// <param name="touchPosition">Position that the user ties to move to</param>
    private void MoveInBounds(Vector2 touchPosition) {
        float zoomScale = gameObject.GetComponent<Camera>().orthographicSize / this.startOrtographicSize;
        float aspectRatio = Camera.main.aspect;
        float newX = this.startPosCamera.x + (((touchPosition.x - this.startPos.x) * this.screenScale) * zoomScale);
        newX = newX < this.CameraMaxX + ((this.startOrtographicSize * aspectRatio) - (zoomScale * (this.startOrtographicSize * aspectRatio))) ? newX : this.CameraMaxX + ((this.startOrtographicSize * aspectRatio) - (zoomScale * (this.startOrtographicSize * aspectRatio)));
        newX = newX > this.CameraMinX - ((this.startOrtographicSize * aspectRatio) - (zoomScale * (this.startOrtographicSize * aspectRatio))) ? newX : this.CameraMinX - ((this.startOrtographicSize * aspectRatio) - (zoomScale * (this.startOrtographicSize * aspectRatio)));
        float newZ = this.startPosCamera.z + (((touchPosition.y - this.startPos.y) * this.screenScale) * zoomScale);
        newZ = newZ < this.CameraMaxZ + (this.startOrtographicSize - (zoomScale * this.startOrtographicSize)) ? newZ : this.CameraMaxZ + (this.startOrtographicSize - (zoomScale * this.startOrtographicSize));
        newZ = newZ > this.CameraMinZ - (this.startOrtographicSize - (zoomScale * this.startOrtographicSize)) ? newZ : this.CameraMinZ - (this.startOrtographicSize - (zoomScale * this.startOrtographicSize));
        transform.position = new Vector3(newX, transform.position.y, newZ);
    }
}
