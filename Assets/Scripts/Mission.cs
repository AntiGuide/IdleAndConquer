using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {
    public UIInteraction UIInteractions;
    public MissionDetails MissionDetails;
    public General MissionGeneral;
    public List<Unit> Units;
    public MissionManager MissionManager;
    private readonly MissionQueue missionQueue;

    public Mission(MissionDetails missionDetails, UIInteraction uiInteractions, General general = null, List<Unit> units = null) {
        this.MissionDetails = missionDetails;
        this.MissionGeneral = general;
        if (units == null) {
            this.Units = new List<Unit>();
        } else {
            this.Units = units;
        }

        this.UIInteractions = uiInteractions;
        this.missionQueue = GameObject.Find("ReferenceShare").GetComponent<ReferenceShare>().MissionQueue;
    }

    public void StartMission() {
        this.MissionGeneral.IsSentToMission = true;
        this.missionQueue.Add(this);
        this.UIInteractions.MainLoad();
    }
}
