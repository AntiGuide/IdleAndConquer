using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildBuilding : MonoBehaviour {
    public GameObject[] BuiltBuildings;
    public GameObject[] Buildings;
    public GameObject BuildConfirmUI;
    public MenueController[] MenueControll;
    public MenueController BuildingMenueController;
    public MainMenueController MainMenueControll;
    public Vector3 BuildUIOffset;
    public MoneyManagement MoneyManager;
    public float CellSize;
    public Vector2 MinBuildConfirmUIPosition;
    public Vector2 MaxBuildConfirmUIPosition;

    private static bool playerBuilding; 
    private bool[] isBuilt;
    private bool playerBuildingThisBase;
    private int newBuildingID;
    private BuildColorChanger buildColorChanger;
    private GameObject newBuilding;
    private RaycastHit hitInformation;
    private Ray touchRay;
    private Vector3 prevScale;
    private int layerMask;
    private int newBuildingXTiles;
    private int newBuildingZTiles;
    private long costBuilding = 0;

    public static bool PlayerBuilding {
        get { return playerBuilding; }
        set { playerBuilding = value; }
    }

    public Vector3 ToGrid(Vector3 allignToGrid) {
        float x, y, z;
        x = Mathf.Round(allignToGrid.x / this.CellSize) * this.CellSize;
        y = allignToGrid.y;
        z = Mathf.Round(allignToGrid.z / this.CellSize) * this.CellSize;
        return new Vector3(x, y, z);
    }

    public void BuildABuilding(int buildingID, long costBuilding) {
        this.costBuilding = costBuilding;
        buildingID--;
        this.newBuildingID = buildingID;
        if (buildingID == 3 && !this.isBuilt[2]) {
        } else {
            this.newBuilding = UnityEngine.Object.Instantiate(this.Buildings[buildingID], transform.parent);
            this.buildColorChanger = this.newBuilding.GetComponentInChildren<BuildColorChanger>();

            this.buildColorChanger.MenueControll = this.MenueControll[buildingID];
            this.buildColorChanger.IsBuilt = false;
            Vector3 tmpVec3 = new Vector3(-250, 1, 0);
            this.newBuilding.transform.position = this.ToGrid(tmpVec3);
            this.prevScale = this.newBuilding.transform.localScale;
            this.newBuilding.transform.localScale *= 1.001f;

            Bounds bounds = this.newBuilding.GetComponentInChildren<Renderer>().bounds;
            this.newBuildingXTiles = Mathf.RoundToInt(bounds.size.x / this.CellSize);
            this.newBuildingZTiles = Mathf.RoundToInt(bounds.size.z / this.CellSize);

            playerBuilding = true;
            this.playerBuildingThisBase = true;
            InputHandler.BlockCameraMovement = true;
            this.BuildingMenueController.Unexpand(true);
        }
    }

    public void CancelBuildingProcess() {
        if (this.newBuilding != null) {
            UnityEngine.Object.Destroy(this.newBuilding);
            playerBuilding = false;
            this.playerBuildingThisBase = false;
            InputHandler.BlockCameraMovement = false;
            this.BuildConfirmUI.SetActive(false);
        }
    }

    public void ConfirmBuildingProcess() {
        if (this.buildColorChanger.CollidingBuildings == 0) {
            if (this.MoneyManager.SubMoney(this.costBuilding)) {
                this.newBuilding.GetComponentInChildren<BuildingManager>().InitializeAttachedBuilding();
                this.isBuilt[this.newBuildingID] = true;
                this.BuiltBuildings[this.newBuildingID] = this.newBuilding;
                this.newBuilding.transform.localScale = this.prevScale;
                this.newBuilding.transform.position = new Vector3(this.newBuilding.transform.position.x, 0, this.newBuilding.transform.position.z);
                this.newBuilding = null;
                playerBuilding = false;
                this.playerBuildingThisBase = false;
                InputHandler.BlockCameraMovement = false;
                this.BuildConfirmUI.SetActive(false);
            }
        }
    }

    // Use this for initialization
    void Start() {
        this.isBuilt = new bool[this.Buildings.Length];
        for (int i = 0; i < this.isBuilt.Length; i++) {
            this.isBuilt[i] = false;
        }

        if (this.BuiltBuildings[2] != null) {
            this.BuiltBuildings[2].GetComponentInChildren<BuildingManager>().InitializeAttachedBuilding();
            this.isBuilt[2] = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            if (EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject()) {
            } else if (this.playerBuildingThisBase) {
                this.touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                this.layerMask = LayerMask.GetMask("Plane");
                Physics.Raycast(this.touchRay.origin, this.touchRay.direction, out this.hitInformation, 3000.0f, this.layerMask);
                if (this.hitInformation.collider != null) {
                    Bounds bounds = this.newBuilding.GetComponentInChildren<Renderer>().bounds;
                    Vector3 cent = bounds.center;
                    Debug.Log(bounds.size.ToString());
                    this.hitInformation.point = new Vector3(this.hitInformation.point.x, 0, this.hitInformation.point.z);
                    this.newBuilding.transform.position = this.ToGrid(this.hitInformation.point);
                }
            } else if (MainMenueController.IsExpanded) {
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            if (!EventSystem.current.IsPointerOverGameObject(0) && !EventSystem.current.IsPointerOverGameObject() && !playerBuilding && !MainMenueController.IsExpanded) {
                this.touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                this.layerMask = LayerMask.GetMask("Buildings");
                Physics.Raycast(this.touchRay.origin, this.touchRay.direction, out this.hitInformation, 3000.0f, this.layerMask);
                if (this.hitInformation.collider != null) {
                    this.MainMenueControll.ToggleMenue(this.hitInformation.collider.gameObject.GetComponent<BuildColorChanger>().MenueControll);
                }
            }
        }
    }

    private void LateUpdate() {
        if (this.playerBuildingThisBase) {
            this.BuildConfirmUI.SetActive(true);
            Bounds bounds = this.newBuilding.GetComponentInChildren<BoxCollider>().bounds;
            Vector3 onlyXZ = new Vector3(bounds.extents.x, 0, bounds.extents.z);
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(this.newBuilding.transform.position + this.BuildUIOffset + onlyXZ);
            screenPoint = Vector2.Max(this.MinBuildConfirmUIPosition, screenPoint);
            screenPoint = Vector2.Min(this.MaxBuildConfirmUIPosition, screenPoint);
            this.BuildConfirmUI.transform.position = screenPoint;
        }
    }
}
