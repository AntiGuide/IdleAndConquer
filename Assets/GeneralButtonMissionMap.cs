using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralButtonMissionMap : MonoBehaviour {
    public static MissionDetails MissionDetail;
    public General General;
    // private Image image;
    public Text country;
    public Text generalName;
    public Text winLoseHistory;
    private MainMenueController mainMenueController;
    private MissionManager MissionMan;

    public void SetTexts(string country, string generalName, string winLoseHistory) {
        this.country.text = country;
        this.generalName.text = generalName;
        this.winLoseHistory.text = winLoseHistory;
    }

    public void OnClick() {
        MissionMan.MissionGeneral = this.General;
        this.mainMenueController.ActivateDeployUI(true);
        this.mainMenueController.ToggleMenue(2);
    }

    // Use this for initialization
    void Start() {
        this.mainMenueController = GameObject.Find("/MissionMap/Canvas/MainMenue/").GetComponent<MainMenueController>();
        this.MissionMan = GameObject.Find("/ReferenceShare").GetComponent<MissionManager>();
    }
}
