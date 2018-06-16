
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralButtonMissionMap : MonoBehaviour {

    private Image image;
    private Text country;
    private Text generalName;
    private Text winLoseHistory;

    private MainMenueController mainMenueController;

    // Use this for initialization
    void Start() {
        mainMenueController = GameObject.Find("/MissionMap/Canvas/MainMenue/").GetComponent<MainMenueController>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetTexts(string country, string generalName, string winLoseHistory) {
        this.country.text = country;
        this.generalName.text = generalName;
        this.winLoseHistory.text = winLoseHistory;
    }

    public void OnClick() {

        SelectedGeneral.General = gameObject.GetComponent<General>();
        mainMenueController.ActivateDeployUI(true);
        mainMenueController.ToggleMenue(2);
    }
}
