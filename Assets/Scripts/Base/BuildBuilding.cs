﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildBuilding : MonoBehaviour {
    
    public GameObject[] buildings;
    public MenueController[] menueController;
    public float cellSize;
    public GameObject buildConfirmUI;
    public MenueController buildingMenueController;
    public Vector3 buildUIOffset;
    public MoneyManagement moneyManager;
    public MainMenueController mainMenueController;

    private Ray touchRay;
    private int layerMask;
    private RaycastHit hitInformation;
    private GameObject newBuilding;
    private int newBuildingXTiles;
    private int newBuildingZTiles;
    private BuildColorChanger buildColorChanger;
    private static bool[] isBuilt;
    private static int newBuildingID;

    public static GameObject[] builtBuildings;

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
        isBuilt = new bool[buildings.Length];
        builtBuildings = new GameObject[buildings.Length];
        for (int i = 0; i < isBuilt.Length; i++) {
            isBuilt[i] = false;
        }
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
                    //Debug.Log(bounds.size.ToString());
                    Vector3 cent = bounds.center;
                    hitInformation.point = new Vector3(hitInformation.point.x, 0, hitInformation.point.z);
                    newBuilding.transform.position = toGrid(hitInformation.point);
                }
            } else if (MainMenueController.IsExpanded){
                //mainMenueController.GetActiveMenueController().Unexpand(true);
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            if(!EventSystem.current.IsPointerOverGameObject(0) && !EventSystem.current.IsPointerOverGameObject() && !playerBuilding && !MainMenueController.IsExpanded) {
                Debug.Log("OpenScreen");
                touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                layerMask = LayerMask.GetMask("Buildings");
                Physics.Raycast(Camera.main.transform.position, touchRay.direction, out hitInformation, 1000.0f, layerMask);
                if (hitInformation.collider != null) {
                    Debug.Log("OpenScreen2");
                    mainMenueController.ToggleMenue(hitInformation.collider.gameObject.GetComponent<BuildColorChanger>().GetMenueController());
                }
            }
        }
    }

    private void LateUpdate() {
        if (playerBuilding) {
            buildConfirmUI.SetActive(true);
            Bounds bounds = newBuilding.GetComponentInChildren<Renderer>().bounds;
            Vector3 onlyXZ = new Vector3(bounds.size.x,0,bounds.size.z);
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(newBuilding.transform.position + buildUIOffset + onlyXZ);
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
        //bool ret = false;
        buildingID--;
        newBuildingID = buildingID;
        if (buildingID == 3 && !isBuilt[2]) {
            //ret = false;
        } else {
            newBuilding = Instantiate(buildings[buildingID]);
            buildColorChanger = newBuilding.GetComponentInChildren<BuildColorChanger>();
            
            buildColorChanger.SetMenueController(menueController[buildingID]);
            
            newBuilding.transform.position = toGrid(new Vector3(-250, 0, 0));
        
            Bounds bounds = newBuilding.GetComponentInChildren<Renderer>().bounds;
            //Debug.Log(bounds.center.ToString() + System.Environment.NewLine + bounds.extents.ToString() + System.Environment.NewLine + bounds.size.ToString());

            newBuildingXTiles = Mathf.RoundToInt(bounds.size.x / cellSize);
            newBuildingZTiles = Mathf.RoundToInt(bounds.size.z / cellSize);
            //Debug.Log(newBuildingXTiles + " " + newBuildingZTiles);

            playerBuilding = true;
            buildingMenueController.Unexpand(true);
            //ret = true;
        }
        //return ret;
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
            if (newBuildingID == 3) {
                newBuilding.GetComponentInChildren<OreRefinery>().Initialize(ref moneyManager);
            }
            isBuilt[newBuildingID] = true;
            builtBuildings[newBuildingID] = newBuilding;
            newBuilding = null;
            playerBuilding = false;
            buildConfirmUI.SetActive(false);
        }
    }  
}
