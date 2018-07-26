using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour {
    public UIInteraction UIInteractions;
    public MissionDetails MissionDetails;
    public General MissionGeneral;
    public List<Unit> Units;
    private MissionQueue missionQueue;

    public void Initialize(MissionDetails missionDetails, UIInteraction uiInteractions, General general = null, List<Unit> units = null) {
        this.MissionDetails = missionDetails;
        this.MissionGeneral = general;
        this.Units = units ?? new List<Unit>();

        this.UIInteractions = uiInteractions;
        this.missionQueue = GameObject.Find("ReferenceShare").GetComponent<ReferenceShare>().MissionQueue;
    }

    public void StartMission() {
        this.MissionGeneral.IsSentToMission = true;
        this.missionQueue.Add(this);
        this.UIInteractions.MainLoad();
    }
}
