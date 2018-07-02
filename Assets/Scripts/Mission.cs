using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {

    public MissionDetails MissionDetails;
    public General MissionGeneral;
    public List<Unit> Units;

    public Mission(MissionDetails missionDetails, General general = null, List<Unit> units = null) {
        this.MissionDetails = missionDetails;
        this.MissionGeneral = general;
        this.Units = units;
    }

    public void StartMission() {
        throw new NotImplementedException();
    }
}
