using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    public BuildingType buildingType;
    public long BuildCost = 1000;
    public int CostEnergy = 5;
    // private EnergyManagement energyManager;
    public int BuildingID;
    public bool CanBeDestroyed = true;
    public UnitAvailabilityManager UnitAvailabilityMan;


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

    private void Start() {
        var go = GameObject.Find("Main/Canvas/MainMenue/MenueArmyBasesInfoStats");
        if (go == null) {
            Debug.Log("No GameObject found in BuildingManager");
        }

        UnitAvailabilityMan = go.GetComponent<UnitAvailabilityManager>();
        if (UnitAvailabilityMan == null) {
            Debug.Log("No UnitAvailabilityManager component found in BuildingManager");
        }
    }

    public void InitializeAttachedBuilding(int buildingID) {
        this.BuildingID = buildingID;
        switch (this.buildingType) {
            case BuildingType.AIRFIELD:
                UnitAvailabilityMan.AirFieldCount[BaseSwitcher.CurrentBase]++;
                // gameObject.GetComponent<AirField>().InitializeBuilt();
                break;
            case BuildingType.COMMAND_CENTER:
                // gameObject.GetComponent<CommandCenter>().InitializeBuilt();
                break;
            case BuildingType.ORE_MINE:
                // gameObject.GetComponent<Mine>().InitializeBuilt();
                break;
            case BuildingType.ORE_REFINERY:
                gameObject.GetComponent<OreRefinery>().InitializeBuilt();
                break;
            case BuildingType.RESEARCH_LAB:
                UnitAvailabilityMan.ResearchLabCount++;
                // gameObject.GetComponent<ResearchLab>().InitializeBuilt();
                break;
            case BuildingType.BARRACKS:
                UnitAvailabilityMan.BarracksCount[BaseSwitcher.CurrentBase]++;
                // gameObject.GetComponent<Barracks>().InitializeBuilt();
                break;
            case BuildingType.POWERPLANT:
                gameObject.GetComponent<PowerPlant>().InitializeBuilt();
                break;
            case BuildingType.TANK_FACTORY:
                UnitAvailabilityMan.TankFactoryCount[BaseSwitcher.CurrentBase]++;
                // gameObject.GetComponent<TankFactory>().InitializeBuilt();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void DeinitializeAttachedBuilding(int buildingID) {
        this.BuildingID = buildingID;
        switch (this.buildingType) {
            case BuildingType.AIRFIELD:
                UnitAvailabilityMan.AirFieldCount[BaseSwitcher.CurrentBase]--;
                // gameObject.GetComponent<AirField>().DeinitializeBuilt();
                break;
            case BuildingType.COMMAND_CENTER:
                // gameObject.GetComponent<CommandCenter>().DeinitializeBuilt();
                break;
            case BuildingType.ORE_MINE:
                // gameObject.GetComponent<Mine>().DeinitializeBuilt();
                break;
            case BuildingType.ORE_REFINERY:
                gameObject.GetComponent<OreRefinery>().DeinitializeBuilt();
                break;
            case BuildingType.RESEARCH_LAB:
                UnitAvailabilityMan.ResearchLabCount--;
                // gameObject.GetComponent<ResearchLab>().DeinitializeBuilt();
                break;
            case BuildingType.BARRACKS:
                UnitAvailabilityMan.BarracksCount[BaseSwitcher.CurrentBase]--;
                // gameObject.GetComponent<Barracks>().DeinitializeBuilt();
                break;
            case BuildingType.POWERPLANT:
                //gameObject.GetComponent<PowerPlant>().DeinitializeBuilt();
                break;
            case BuildingType.TANK_FACTORY:
                UnitAvailabilityMan.TankFactoryCount[BaseSwitcher.CurrentBase]--;
                // gameObject.GetComponent<TankFactory>().DeinitializeBuilt();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
