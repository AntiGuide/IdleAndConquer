using UnityEngine;

public class BuildingManager : MonoBehaviour {

    private EnergyManagement energyManager;

    private long buildCost = 1000;
    public long BuildCost {
        get {
            return buildCost;
        }

        set {
            buildCost = value;
        }
    }

    private float costEnergy = 5;
    public float CostEnergy {
        get {
            return costEnergy;
        }

        set {
            costEnergy = value;
        }
    }

    public BuildingType buildingType;
    public enum BuildingType {
        AIRFIELD = 0,
        COMMAND_CENTER,
        ORE_MINE,
        ORE_REFINERY,
        RESEARCH_LAB,
        BARRACKS,
        POWERPLANT,
        TANK_FACTORY
    }

    // Use this for initialization
    void Start () {
        energyManager = GameObject.Find("/Main/Canvas/BackgroundSideStrip").GetComponent<EnergyManagement>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeAttachedBuilding() {
        switch (buildingType) {
            case BuildingType.AIRFIELD:
                //gameObject.GetComponent<AirField>().InitializeBuilt();
                break;
            case BuildingType.COMMAND_CENTER:
                //gameObject.GetComponent<CommandCenter>().InitializeBuilt();
                break;
            case BuildingType.ORE_MINE:
                //gameObject.GetComponent<Mine>().InitializeBuilt();
                break;
            case BuildingType.ORE_REFINERY:
                gameObject.GetComponent<OreRefinery>().InitializeBuilt();
                break;
            case BuildingType.RESEARCH_LAB:
                //gameObject.GetComponent<ResearchLab>().InitializeBuilt();
                break;
            case BuildingType.BARRACKS:
                //gameObject.GetComponent<Barracks>().InitializeBuilt();
                break;
            case BuildingType.POWERPLANT:
                gameObject.GetComponent<PowerPlant>().InitializeBuilt();
                break;
            case BuildingType.TANK_FACTORY:
                //gameObject.GetComponent<TankFactory>().InitializeBuilt();
                break;
            default:
                break;
        }
        //if (energyManager.subEnergy(costEnergy)){
            
        //}
    }
}
