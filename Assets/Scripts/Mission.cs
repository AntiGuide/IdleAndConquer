using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {
    public UIInteraction UIInteractions;
    public MissionDetails MissionDetails;
    public General MissionGeneral;
    public List<Unit> Units;
    public MissionManager MissionManager;
    private MissionQueue MissionQueue;

    public Mission(MissionDetails missionDetails, UIInteraction UIInteractions, General general = null, List<Unit> units = null) {
        this.MissionDetails = missionDetails;
        this.MissionGeneral = general;
        if (units == null) {
            this.Units = new List<Unit>();
        } else {
            this.Units = units;
        }
        this.UIInteractions = UIInteractions;
        this.MissionQueue = GameObject.Find("ReferenceShare").GetComponent<ReferenceShare>().MissionQueue;
    }

    public void StartMission() {
        this.MissionQueue.Add(this);
        UIInteractions.MainLoad();
    }
}
