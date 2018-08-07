using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildBuilding : MonoBehaviour {
    public GameObject[] BuiltBuildings;
    public GameObject[] Buildings;
    public GameObject BuildConfirmUI;
    public MenueController[] MenueControll;
    public MenueController BuildingMenueController;
    public MainMenueController MainMenueControll;
    public Vector3 BuildUIOffset;
    public MoneyManagement MoneyManager;
    public BaseSwitcher BaseSwitch;
    public float CellSize;
    public Vector2 MinBuildConfirmUIPosition;
    public Vector2 MaxBuildConfirmUIPosition;

    private bool[] isBuilt;
    private bool playerBuildingThisBase;
    private int newBuildingID;
    private BuildColorChanger buildColorChanger;
    private GameObject newBuilding;
    private RaycastHit hitInformation;
    private Ray touchRay;
    private Vector3 prevScale;
    private int layerMask;
    // private int newBuildingXTiles;
    // private int newBuildingZTiles;
    private long costBuilding = 0;
    private int costEnergy = 0;

    public static bool PlayerBuilding { get; private set; }

    public void BuildABuilding(int buildingID, long costBuilding, int costEnergy) {
        InputHandler.MoveCamForBuilding = true;
        this.costBuilding = costBuilding;
        this.costEnergy = costEnergy;
        buildingID--;
        this.newBuildingID = buildingID;
        if (buildingID == 3 && !this.isBuilt[2]) {
        } else {
            this.newBuilding = UnityEngine.Object.Instantiate(this.Buildings[buildingID], transform.parent);
            this.buildColorChanger = this.newBuilding.GetComponentInChildren<BuildColorChanger>();

            this.buildColorChanger.MenueControll = this.MenueControll[buildingID];
            this.buildColorChanger.IsBuilt = false;
            var tmpVec3 = new Vector3(-250, 1, 0);
            this.newBuilding.transform.position = this.ToGrid(tmpVec3);
            this.prevScale = this.newBuilding.transform.localScale;
            this.newBuilding.transform.localScale *= 1.001f;

            // Bounds bounds = this.newBuilding.GetComponentInChildren<Renderer>().bounds;
            // this.newBuildingXTiles = Mathf.RoundToInt(bounds.size.x / this.CellSize);
            // this.newBuildingZTiles = Mathf.RoundToInt(bounds.size.z / this.CellSize);

            PlayerBuilding = true;
            this.playerBuildingThisBase = true;
            InputHandler.BlockCameraMovement = true;
            this.BuildingMenueController.Unexpand(true);
        }
    }

    public void CancelBuildingProcess() {
        if (this.newBuilding == null) return;
        InputHandler.MoveCamForBuilding = false;
        UnityEngine.Object.Destroy(this.newBuilding);
        PlayerBuilding = false;
        this.playerBuildingThisBase = false;
        InputHandler.BlockCameraMovement = false;
        this.BuildConfirmUI.SetActive(false);
    }

    public void ConfirmBuildingProcess() {
        if (this.buildColorChanger.CollidingBuildings != 0) return;
        if (!this.MoneyManager.SubMoney(this.costBuilding)) return;
        InputHandler.MoveCamForBuilding = false;
        this.BaseSwitch.GetEnergyPool().SubEnergy(this.costEnergy);
        this.newBuilding.GetComponentInChildren<BuildingManager>().InitializeAttachedBuilding();
        this.isBuilt[this.newBuildingID] = true;
        this.BuiltBuildings[this.newBuildingID] = this.newBuilding;
        this.newBuilding.transform.localScale = this.prevScale;
        this.newBuilding.transform.position = new Vector3(this.newBuilding.transform.position.x, 0, this.newBuilding.transform.position.z);
        this.newBuilding = null;
        PlayerBuilding = false;
        this.playerBuildingThisBase = false;
        InputHandler.BlockCameraMovement = false;
        this.BuildConfirmUI.SetActive(false);
    }

    private Vector3 ToGrid(Vector3 allignToGrid) {
        var x = Mathf.Round(allignToGrid.x / this.CellSize) * this.CellSize;
        var y = allignToGrid.y;
        var z = Mathf.Round(allignToGrid.z / this.CellSize) * this.CellSize;
        return new Vector3(x, y, z);
    }

    // Use this for initialization
    private void Start() {
        this.isBuilt = new bool[this.Buildings.Length];
        for (var i = 0; i < this.isBuilt.Length; i++) {
            this.isBuilt[i] = false;
        }

        if (this.BuiltBuildings[2] == null) return;
        this.BuiltBuildings[2].GetComponentInChildren<BuildingManager>().InitializeAttachedBuilding();
        this.isBuilt[2] = true;
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetMouseButton(0)) {
            if (EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject()) {
            } else if (this.playerBuildingThisBase) {
                this.touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                this.layerMask = LayerMask.GetMask("Plane");
                Physics.Raycast(this.touchRay.origin, this.touchRay.direction, out this.hitInformation, 3000.0f, this.layerMask);
                if (this.hitInformation.collider != null) {
                    // Bounds bounds = this.newBuilding.GetComponentInChildren<Renderer>().bounds;
                    // Vector3 cent = bounds.center;
                    this.hitInformation.point = new Vector3(this.hitInformation.point.x, 0, this.hitInformation.point.z);
                    this.newBuilding.transform.position = this.ToGrid(this.hitInformation.point);
                }
            }
        }

        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject() ||
            PlayerBuilding || this.MainMenueControll.IsExpanded) return;
        this.touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        this.layerMask = LayerMask.GetMask("Buildings");
        Physics.Raycast(this.touchRay.origin, this.touchRay.direction, out this.hitInformation, 3000.0f, this.layerMask);
        if (this.hitInformation.collider != null) {
            this.MainMenueControll.ToggleMenue(this.hitInformation.collider.gameObject.GetComponent<BuildColorChanger>().MenueControll);
        }
    }

    private void LateUpdate() {
        if (!this.playerBuildingThisBase) return;
        this.BuildConfirmUI.SetActive(true);
        var bounds = this.newBuilding.GetComponentInChildren<BoxCollider>().bounds;
        var onlyXZ = new Vector3(bounds.extents.x, 0, bounds.extents.z);
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(this.newBuilding.transform.position + this.BuildUIOffset + onlyXZ);
        screenPoint = Vector2.Max(new Vector2(this.MinBuildConfirmUIPosition.x * (Screen.width / 1080f), this.MinBuildConfirmUIPosition.y * (Screen.height / 1920f)), screenPoint);
        screenPoint = Vector2.Min(new Vector2(this.MaxBuildConfirmUIPosition.x * (Screen.width / 1080f), this.MaxBuildConfirmUIPosition.y * (Screen.height / 1920f)), screenPoint);
        //screenPoint = Vector2.Min(this.MaxBuildConfirmUIPosition, screenPoint);
        this.BuildConfirmUI.transform.position = screenPoint;
    }
}
