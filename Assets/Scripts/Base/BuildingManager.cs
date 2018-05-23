using UnityEngine;

public class BuildingManager : MonoBehaviour {
    
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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitializeAttachedBuilding() {
        switch (buildingType) {
            case BuildingType.AIRFIELD:
                break;
            case BuildingType.COMMAND_CENTER:
                break;
            case BuildingType.ORE_MINE:
                break;
            case BuildingType.ORE_REFINERY:
                gameObject.GetComponent<OreRefinery>().InitializeBuilt();
                break;
            case BuildingType.RESEARCH_LAB:
                break;
            case BuildingType.BARRACKS:
                break;
            case BuildingType.POWERPLANT:
                gameObject.GetComponent<PowerPlant>().InitializeBuilt();
                break;
            case BuildingType.TANK_FACTORY:
                break;
            default:
                break;
        }
    }
}
