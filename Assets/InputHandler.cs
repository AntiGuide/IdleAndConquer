using UnityEngine;

public class InputHandler : MonoBehaviour {
    public float deadZoneDrag;
    public float cameraMaxX;
    public float cameraMinX;

    private Vector2 startPos;
    private Vector3 startPosCamera;
    private bool movedDuringTouch = false;


    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        // Handle native touch events
        foreach (Touch touch in Input.touches) {
            HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        // Simulate touch events from mouse events
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
                startPos = touchPosition;
                startPosCamera = transform.position;
                movedDuringTouch = false;
                break;
            case TouchPhase.Moved:
                if (!movedDuringTouch && Vector2.Distance(startPos, touchPosition) > deadZoneDrag) {
                    movedDuringTouch = true;
                }
                if (movedDuringTouch) {
                    //TODO move to touch position
                    float newX = startPosCamera.x + (touchPosition.x - startPos.x);
                    newX = newX < cameraMaxX ? newX : cameraMaxX;
                    newX = newX > cameraMinX ? newX : cameraMinX;
                    transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                }
                break;
            case TouchPhase.Ended:
                if (!movedDuringTouch) {
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
