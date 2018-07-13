using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    // public List<Mission> Missions = new List<Mission>();
    // public Mission BuildingMission = null;
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

    public void GenerateMission(MissionDetails missionDetails, UIInteraction UIInteractions, MainMenueController MainMenueControll, General general = null) {
        // Mission m = new Mission(missionDetails, UIInteractions, general, units);
        // MissionManager.MainMenueControll = MainMenueControll;
        // BuildingMission = m;
        // Missions.Add(m);
        this.MissionDetails = missionDetails;
        this.UIInteractions = UIInteractions;
        this.MainMenueControll = MainMenueControll;
        this.MissionGeneral = general;

    }

    public void StartMission() {
        this.MissionGeneral.IsSentToMission = true;
        foreach (Unit item in this.Units) {
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
