using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    public static List<Mission> Missions = new List<Mission>();
    public static Mission BuildingMission = null;
    public static MainMenueController MainMenueControll;

    public static Mission GenerateMission(MissionDetails missionDetails, UIInteraction UIInteractions, MainMenueController MainMenueControll, General general = null, List<Unit> units = null) {
        if (BuildingMission != null) {
            return null;
        }
        Mission m = new Mission(missionDetails, UIInteractions, general, units);
        MissionManager.MainMenueControll = MainMenueControll;
        BuildingMission = m;
        Missions.Add(m);
        return m;
    }

    public static void StartMission() {
        BuildingMission.StartMission();
        OnClickDeploy.DeployedUnits = 0;
        BuildingMission = null;
        MissionManager.MainMenueControll.ActivateDeployUI(false);
    }

    public static void AddUnitToBuildingMission(Unit unit) {
        BuildingMission.Units.Add(unit);
    }
}
