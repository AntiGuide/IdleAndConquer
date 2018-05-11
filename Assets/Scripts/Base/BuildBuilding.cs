using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildBuilding : MonoBehaviour {
    
    public GameObject[] buildings;
    public float cellSize;

    private bool playerBuilding;
    private Ray touchRay;
    private int layerMask;
    private RaycastHit hitInformation;
    private GameObject newBuilding;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            if (EventSystem.current.IsPointerOverGameObject()) {    // is the touch on the GUI
                //Debug.Log("GUI");
            } else if (playerBuilding) {
                touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                layerMask = LayerMask.GetMask("Plane");
                Physics.Raycast(Camera.main.transform.position, touchRay.direction, out hitInformation, 1000.0f, layerMask);
                if (hitInformation.collider != null) {
                    newBuilding.transform.position = toGrid(hitInformation.point);
                }
            }
        }
    }

    public Vector3 toGrid(Vector3 allignToGrid) {
        //TODO Dont use variables
        float x, y, z;
        x = Mathf.Round(allignToGrid.x / cellSize) * cellSize;
        y = allignToGrid.y;
        z = Mathf.Round(allignToGrid.z / cellSize) * cellSize;
        return new Vector3(x, y, z);
    }

    public void buildBuilding(int buildingID) {
        buildingID--;
        newBuilding = Instantiate(buildings[buildingID]);
        playerBuilding = true;
    }
}
