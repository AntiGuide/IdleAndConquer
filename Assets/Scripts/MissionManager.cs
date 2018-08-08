using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    public MissionDetails MissionDetails;
    public UIInteraction UIInteractions;
    public MainMenueController MainMenueControll;
    public General MissionGeneral;
    public List<Unit> Units = new List<Unit>();
    public MissionQueue MissionQueue;

    public void Reset() {
        this.MissionDetails = null;
        this.UIInteractions = null;
        this.MainMenueControll = null;
        this.MissionGeneral = null;
        this.Units = new List<Unit>();
    }

    public void GenerateMission(MissionDetails missionDetails, UIInteraction uiInteractions, MainMenueController mainMenueControll, General general = null) {
        this.MissionDetails = missionDetails;
        this.UIInteractions = uiInteractions;
        this.MainMenueControll = mainMenueControll;
        this.MissionGeneral = general;
    }

    public void StartMission() {
        if (MissionDetails == null) {
            return;
        }
        this.MissionGeneral.IsSentToMission = true;
        foreach (var item in this.Units) {
            item.SentToMission++;
        }

        var m = gameObject.AddComponent<Mission>();
        m.Initialize(this.MissionDetails, this.UIInteractions, this.MissionGeneral, this.Units);
        this.MissionDetails.currentlyRunning = true;
        this.MissionQueue.Add(m);
        this.UIInteractions.MainLoad();
        OnClickDeploy.DeployedUnits = 0;
        this.MainMenueControll.ActivateDeployUI(false);
        this.Reset();
    }

    public void AddUnitToBuildingMission(ref Unit unit) {
        this.Units.Add(unit);
    }

    public void RemoveUnitFromBuildingMission(ref Unit unit) {
        this.Units.Remove(unit);
    }
}
