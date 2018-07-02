using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralButtonMissionMap : MonoBehaviour {
    public static MissionDetails MissionDetail;
    private Image image;
    private Text country;
    private Text generalName;
    private Text winLoseHistory;
    private MainMenueController mainMenueController;

    public void SetTexts(string country, string generalName, string winLoseHistory) {
        this.country.text = country;
        this.generalName.text = generalName;
        this.winLoseHistory.text = winLoseHistory;
    }

    public void OnClick() {
        MissionManager.BuildingMission.MissionGeneral = gameObject.GetComponent<General>();
        this.mainMenueController.ActivateDeployUI(true);
        this.mainMenueController.ToggleMenue(2);
    }

    // Use this for initialization
    void Start() {
        this.mainMenueController = GameObject.Find("/MissionMap/Canvas/MainMenue/").GetComponent<MainMenueController>();
    }
}
