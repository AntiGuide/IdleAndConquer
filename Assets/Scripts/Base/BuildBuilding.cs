using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildBuilding : MonoBehaviour {
    
    public GameObject[] buildings;
    public float cellSize;
    public GameObject buildConfirmUI;
    public MenueController buildingMenueController;
    public Vector3 buildUIOffset;

    private Ray touchRay;
    private int layerMask;
    private RaycastHit hitInformation;
    private GameObject newBuilding;
    private int newBuildingXTiles;
    private int newBuildingZTiles;
    private BuildColorChanger buildColorChanger;

    private static bool playerBuilding;
    public static bool PlayerBuilding {
        get {
            return playerBuilding;
        }

        set {
            playerBuilding = value;
        }
    }

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
            //int paddingRight = 5;
            //int paddingLeft = 5;
            //int paddingDown = 5;
            //int paddingUp = 5;
            Bounds bounds = newBuilding.GetComponentInChildren<Renderer>().bounds;
            Vector3 onlyXZ = new Vector3(bounds.size.x,0,bounds.size.z);
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(newBuilding.transform.position + buildUIOffset + onlyXZ);
            //int newX;
            //int newY;
            //if (screenPoint.x > Camera.main.pixelWidth - paddingRight) {
            //    newX = Camera.main.pixelWidth - paddingRight;
            //} else if (screenPoint.x < paddingLeft) {
            //    newX = paddingLeft;
            //} else {
            //    newX = Mathf.RoundToInt(screenPoint.x);
            //}

            //if (screenPoint.y > Camera.main.pixelWidth - paddingUp) {
            //    newY = Camera.main.pixelWidth - paddingUp;
            //} else if (screenPoint.y < paddingDown) {
            //    newY = paddingDown;
            //} else {
            //    newY = Mathf.RoundToInt(screenPoint.y);
            //}

            //screenPoint = new Vector2(screenPoint.x - (Screen.width * percentageXOffsetUI), screenPoint.y - (Screen.height * percentageYOffsetUI));
            buildConfirmUI.transform.position = screenPoint;//new Vector2((float)newX, (float)newY);

        }
    }

    void OnDrawGizmos() {
        if (playerBuilding) {
            Bounds bounds = newBuilding.GetComponentInChildren<Renderer>().bounds;
            Gizmos.color = Color.red;
            //Gizmos.DrawWireCube(bounds.center, bounds.size);
            //Gizmos.DrawSphere(newBuilding.transform.position, 1.0f);
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
        buildColorChanger = newBuilding.GetComponentInChildren<BuildColorChanger>();
        newBuilding.transform.position = toGrid(new Vector3(-250, 0, 0));
        
        Bounds bounds = newBuilding.GetComponentInChildren<Renderer>().bounds;
        //Debug.Log(bounds.center.ToString() + System.Environment.NewLine + bounds.extents.ToString() + System.Environment.NewLine + bounds.size.ToString());

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
        if (buildColorChanger.CollidingBuildings == 0) {
            newBuilding = null;
            playerBuilding = false;
            buildConfirmUI.SetActive(false);
        }
    }  
}
