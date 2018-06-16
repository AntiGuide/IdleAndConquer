using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary> 
/// Class to hold the details of a mission 
/// </summary> 
public class MissionDetails : MonoBehaviour {
    /// <summary>The name of the mission, e.g. Beat Juri</summary> 
    public string MissionName;
    private Ray touchRay;
    private int layerMask;
    private RaycastHit hitInformation;

    private void Update() {
        if (Input.GetMouseButton(0)) {
            if (EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject()) {
            } else {
                this.touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                this.layerMask = LayerMask.GetMask("MissionLocation");
                Physics.Raycast(this.touchRay.origin, this.touchRay.direction, out this.hitInformation, 3000.0f, this.layerMask);
                if (this.hitInformation.collider != null) {
                    Debug.Log(this.hitInformation.collider.gameObject.GetComponent<MissionDetails>().MissionName);
                }
            }
        }
    }
}