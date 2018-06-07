using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class to controll the camera in the base
/// </summary>
public class CameraMovement : MonoBehaviour {
    /// <summary>The maximum position the camera may have without zoom</summary>
    public Vector3 CameraMax;

    /// <summary>The minimum position the camera may have without zoom</summary>
    public Vector3 CameraMin;

    public static bool BlockCameraMovement;

    /// <summary>The distance which the user must drag until a drag is registered as such versus a click</summary>
    public float DeadZoneDrag;

    /// <summary>The position where the touch began</summary>
    private Vector2 startPos;

    /// <summary>The position where the camera was when the touch began</summary>
    private Vector3 startPosCamera;

    /// <summary>Marks wether the map movement is blocked (e.g. during zoom)</summary>
    private bool blockMapMovement = false;

    /// <summary>Marks wether the finger moved after the touch began</summary>
    private bool movedDuringTouch = false;

    /// <summary>Use this for initialization</summary>
    void Start() {
    }

    /// <summary>Update is called once per frame</summary>
    void Update() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            foreach (Touch touch in Input.touches) {
                this.HandleTouch(touch.fingerId, touch.position, touch.phase);
            }
        }

        // Simulate touch events from mouse events
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
                    startPosCamera = transform.position;
                    this.movedDuringTouch = false;
                }

                break;
            case TouchPhase.Moved:
                if (!this.blockMapMovement && !BlockCameraMovement) {
                    if (!this.movedDuringTouch && Vector2.Distance(this.startPos, touchPosition) > this.DeadZoneDrag) {
                        this.movedDuringTouch = true;
                    }

                    if (this.movedDuringTouch) {
                        Vector3 desiredPosition;
                        desiredPosition = this.startPosCamera + (transform.right * (this.startPos.x - touchPosition.x)) + (transform.forward * (this.startPos.y - touchPosition.y));
                        this.MoveInBounds(desiredPosition);
                    }
                }

                break;
            case TouchPhase.Ended:
                this.blockMapMovement = false;
                break;
        }
    }

    /// <summary>
    /// Moves the transform (camera) inside of the given bounds
    /// </summary>
    /// <param name="positionToMoveTo">Position that the user ties to move to</param>
    private void MoveInBounds(Vector3 positionToMoveTo) {
        float newX = positionToMoveTo.x;
        newX = newX < this.CameraMax.x ? newX : this.CameraMax.x;
        newX = newX > this.CameraMin.x ? newX : this.CameraMin.x;
        float newZ = positionToMoveTo.z;
        newZ = newZ < this.CameraMax.z ? newZ : this.CameraMax.z;
        newZ = newZ > this.CameraMin.z ? newZ : this.CameraMin.z;
        transform.position = new Vector3(newX, this.startPosCamera.y, newZ);
    }
}
