using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildBuilding : MonoBehaviour {
    
    public GameObject[] buildings;
    public float cellSize;
    public float percentageXOffsetUI;
    public float percentageYOffsetUI;
    public GameObject buildConfirmUI;
    public MenueController buildingMenueController;

    private bool playerBuilding;
    private Ray touchRay;
    private int layerMask;
    private RaycastHit hitInformation;
    private GameObject newBuilding;
    private int newBuildingXTiles;
    private int newBuildingZTiles;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButton(0)) {
            if (EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject()) {    // is the touch on the GUI
                //Debug.Log("GUI");
            } else if (playerBuilding) {
                touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                layerMask = LayerMask.GetMask("Plane");
                Physics.Raycast(Camera.main.transform.position, touchRay.direction, out hitInformation, 1000.0f, layerMask);
                if (hitInformation.collider != null) {
                    Bounds bounds = newBuilding.GetComponentInChildren<Renderer>().bounds;
                    Vector3 cent = bounds.center;
                    hitInformation.point = new Vector3(hitInformation.point.x, 0, hitInformation.point.z);
                    //hitInformation.point = new Vector3(hitInformation.point.x, hitInformation.point.y + newBuilding.GetComponentInChildren<MeshRenderer>().bounds.size.y / 2, hitInformation.point.z);
                    newBuilding.transform.position = toGrid(hitInformation.point);
                }
            }
        }
    }

    private void LateUpdate() {
        if (playerBuilding) {
            buildConfirmUI.SetActive(true);
            //Confirm Build UI

            Vector2 screenPoint = Camera.main.WorldToScreenPoint(newBuilding.transform.position);
            
            //newBuilding.GetComponentInChildren<MeshRenderer>().bounds.size
            //Offset Replacement
            screenPoint = new Vector2(screenPoint.x - (Screen.width * percentageXOffsetUI), screenPoint.y - (Screen.height * percentageYOffsetUI));
            buildConfirmUI.transform.position = screenPoint;

        }
    }

    void OnDrawGizmos() {
        if (playerBuilding) {
            Bounds bounds = newBuilding.GetComponentInChildren<Renderer>().bounds;
            Gizmos.color = Color.red;
            //Gizmos.DrawWireCube(bounds.center, bounds.size);
            Gizmos.DrawSphere(newBuilding.transform.position, 1.0f);
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
        newBuilding.transform.position = toGrid(new Vector3(-250, 0, 0));
        
        Bounds bounds = newBuilding.GetComponentInChildren<Renderer>().bounds;
        Debug.Log(bounds.center.ToString() + System.Environment.NewLine + bounds.extents.ToString() + System.Environment.NewLine + bounds.size.ToString());

        newBuildingXTiles = Mathf.RoundToInt(bounds.size.x / cellSize);
        newBuildingZTiles = Mathf.RoundToInt(bounds.size.z / cellSize);
        //Debug.Log(newBuildingXTiles + " " + newBuildingZTiles);
        playerBuilding = true;
        buildingMenueController.Unexpand(true);
    }

    public void CancelBuildingProcess() {
        if (newBuilding != null) {
            Destroy(newBuilding);
            playerBuilding = false;
            buildConfirmUI.SetActive(false);
        }
    }

    public void ConfirmBuildingProcess() {
        newBuilding = null;
        playerBuilding = false;
        buildConfirmUI.SetActive(false);
    }
}
