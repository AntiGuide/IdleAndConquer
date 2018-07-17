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
        this.MissionGeneral.IsSentToMission = true;
        foreach (var item in this.Units) {
            item.SentToMission++;
        }

        this.MissionQueue.Add(new Mission(this.MissionDetails, this.UIInteractions, this.MissionGeneral, this.Units));
        this.UIInteractions.MainLoad();
        OnClickDeploy.DeployedUnits = 0;
        this.MainMenueControll.ActivateDeployUI(false);
    }

    public void AddUnitToBuildingMission(ref Unit unit) {
        this.Units.Add(unit);
    }
}
