using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    public static List<Mission> Missions = new List<Mission>();
    public static Mission BuildingMission = null;

    public static Mission GenerateMission(MissionDetails missionDetails, General general = null, List<Unit> units = null) {
        if (BuildingMission != null) {
            return null;
        }
        Mission m = new Mission(missionDetails, general, units);
        BuildingMission = m;
        Missions.Add(m);
        return m;
    }

    public void StartMission() {
        BuildingMission.StartMission();
        BuildingMission = null;
    }
}
