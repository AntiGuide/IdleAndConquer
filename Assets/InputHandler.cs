using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour {
    public float deadZoneDrag;
    public float cameraMaxX;
    public float cameraMinX;
    public float cameraMaxZ;
    public float cameraMinZ;

    private Vector2 startPos;
    private Vector3 startPosCamera;
    private bool movedDuringTouch = false;
    private bool blockMapMovement = false;
    private Vector3 ScreenSize;
    private float startOrtographicSize;


    // Use this for initialization
    void Start () {
        ScreenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        startOrtographicSize = gameObject.GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update() {
        //Debug PC Zoom
        //if (Input.GetKeyDown(KeyCode.I)) {
        //    gameObject.GetComponent<Camera>().orthographicSize = 70f;
        //}else if(Input.GetKeyDown(KeyCode.O)) {
        //    gameObject.GetComponent<Camera>().orthographicSize = 140f;
        //}else if (Input.GetKeyDown(KeyCode.P)) {
        //    gameObject.GetComponent<Camera>().orthographicSize = 280f;
        //}

        // Handle native touch events
        //TODO Mobile Pointer 0
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
                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Max(gameObject.GetComponent<Camera>().orthographicSize, 70f);
                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Min(gameObject.GetComponent<Camera>().orthographicSize, 280f);

                float zoomScale = gameObject.GetComponent<Camera>().orthographicSize / startOrtographicSize;
                float screenScale = 315f / Screen.width;

                float newX = startPosCamera.x + (transform.position.x - startPos.x) * screenScale * zoomScale;
                newX = newX < cameraMaxX + (160f - (zoomScale * 160f)) ? newX : cameraMaxX + (160f - (zoomScale * 160f));
                newX = newX > cameraMinX - (160f - (zoomScale * 160f)) ? newX : cameraMinX - (160f - (zoomScale * 160f));
                float newZ = startPosCamera.z + (transform.position.y - startPos.y) * screenScale * zoomScale;
                newZ = newZ < cameraMaxZ + (280f - (zoomScale * 280f)) ? newZ : cameraMaxZ + (280f - (zoomScale * 280f));
                newZ = newZ > cameraMinZ - (280f - (zoomScale * 280f)) ? newZ : cameraMinZ - (280f - (zoomScale * 280f));
                transform.position = new Vector3(newX, transform.position.y, newZ);
            }
        } else if(!EventSystem.current.IsPointerOverGameObject()) {
            foreach (Touch touch in Input.touches) {
                HandleTouch(touch.fingerId, touch.position, touch.phase);
            }
        }

        // Simulate touch events from mouse events
        //TODO Mobile Pointer 0
        if (Input.touchCount == 0) {
            if (Input.GetMouseButtonDown(0)) {
                HandleTouch(10, Input.mousePosition, TouchPhase.Began);
            }
            if (Input.GetMouseButton(0)) {
                HandleTouch(10, Input.mousePosition, TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0)) {
                HandleTouch(10, Input.mousePosition, TouchPhase.Ended);
            }
        }
    }

    private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase) {
        switch (touchPhase) {
            case TouchPhase.Began:
                if (EventSystem.current.IsPointerOverGameObject()) {
                    blockMapMovement = true;
                } else {
                    startPos = touchPosition;
                    startPosCamera = transform.position;
                    movedDuringTouch = false;
                }
                
                break;
            case TouchPhase.Moved:
                if (!blockMapMovement) {
                    if (!movedDuringTouch && Vector2.Distance(startPos, touchPosition) > deadZoneDrag) {
                        movedDuringTouch = true;
                    }
                    if (movedDuringTouch) {
                        float zoomScale = gameObject.GetComponent<Camera>().orthographicSize / startOrtographicSize;
                        float screenScale = 315f / Screen.width;

                        float newX = startPosCamera.x + (touchPosition.x - startPos.x) * screenScale * zoomScale;
                        newX = newX < cameraMaxX + (160f - (zoomScale * 160f)) ? newX : cameraMaxX + (160f - (zoomScale * 160f));
                        newX = newX > cameraMinX - (160f - (zoomScale * 160f)) ? newX : cameraMinX - (160f - (zoomScale * 160f));
                        float newZ = startPosCamera.z + (touchPosition.y - startPos.y) * screenScale * zoomScale;
                        newZ = newZ < cameraMaxZ + (280f - (zoomScale * 280f)) ? newZ : cameraMaxZ + (280f - (zoomScale * 280f));
                        newZ = newZ > cameraMinZ - (280f - (zoomScale * 280f)) ? newZ : cameraMinZ - (280f - (zoomScale * 280f));
                        transform.position = new Vector3(newX, transform.position.y, newZ);
                    }
                }
                break;
            case TouchPhase.Ended:
                if (blockMapMovement) {
                    blockMapMovement = false;
                }else if (!movedDuringTouch) {
                    Ray touchRay = Camera.main.ScreenPointToRay(touchPosition);
                    int layerMask = LayerMask.GetMask("MissionLocation");
                    RaycastHit hitInformation;
                    Physics.Raycast(touchRay.origin, touchRay.direction, out hitInformation, 700.0f, layerMask);
                    if (hitInformation.collider != null) {
                        if (hitInformation.collider.tag.Equals("MissionLocation")) {
                            Debug.Log(hitInformation.collider.GetComponent<MissionDetails>().missionName);
                        }
                    }
                }
                break;
        }
    }
}
