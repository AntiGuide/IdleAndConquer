using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    public static List<Mission> Missions = new List<Mission>();
    public static Mission BuildingMission = null;

    public static Mission GenerateMission(MissionDetails missionDetails, UIInteraction UIInteractions, General general = null, List<Unit> units = null) {
        if (BuildingMission != null) {
            return null;
        }
        Mission m = new Mission(missionDetails, UIInteractions, general, units);
        BuildingMission = m;
        Missions.Add(m);
        return m;
    }

    public static void StartMission() {
        BuildingMission.StartMission();
        BuildingMission = null;
    }

    public static void AddUnitToBuildingMission(Unit unit) {
        BuildingMission.Units.Add(unit);
    }
}
