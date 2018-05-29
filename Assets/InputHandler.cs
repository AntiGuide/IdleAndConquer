using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour {
    public float DeadZoneDrag;
    public float CameraMaxX;
    public float CameraMinX;
    public float CameraMaxZ;
    public float CameraMinZ;
    public float MinZoom;
    public float MaxZoom;
    public MainMenueController MainMenueControll;

    private Vector2 startPos;
    private Vector3 startPosCamera;
    private bool movedDuringTouch = false;
    private bool blockMapMovement = false;
    private Vector3 screenSize;
    private float startOrtographicSize;
    private float screenScale;

    // Use this for initialization
    void Start() {
        this.screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        this.startOrtographicSize = gameObject.GetComponent<Camera>().orthographicSize;
        this.screenScale = 4500f / Screen.width;
    }

    // Update is called once per frame
    void Update() {
        // Debug PC Zoom
        if (Input.GetKeyDown(KeyCode.I)) {
            gameObject.GetComponent<Camera>().orthographicSize = 2000f;
            this.MoveInBounds(transform.position);
        } else if (Input.GetKeyDown(KeyCode.O)) {
            gameObject.GetComponent<Camera>().orthographicSize = 3000f;
            this.MoveInBounds(transform.position);
        } else if (Input.GetKeyDown(KeyCode.P)) {
            gameObject.GetComponent<Camera>().orthographicSize = 4000f;
            this.MoveInBounds(transform.position);
        }
        
        // Handle native touch events
        // TODO Mobile Pointer 0
        if (Input.touchCount == 2 && !EventSystem.current.IsPointerOverGameObject()) {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (gameObject.GetComponent<Camera>().orthographic) {
                // ... change the orthographic size based on the change in distance between the touches.
                gameObject.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * 0.5f;

                // Make sure the orthographic size never drops below zero.
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
