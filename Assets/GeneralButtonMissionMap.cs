using UnityEngine;
using UnityEngine.UI;

public class GeneralButtonMissionMap : MonoBehaviour {
    public General General;
    public Text country;
    public Text generalName;
    public Text winLoseHistory;
    //private GameObject missionDetailsWindow;
    //private MainMenueController mainMenueController;
    private MissionManager missionMan;
    private ScreenStateMachine screenStateMach;

    public void SetTexts(string country, string generalName, string winLoseHistory) {
        this.country.text = country;
        this.generalName.text = generalName;
        this.winLoseHistory.text = winLoseHistory;
    }

    public void OnClick() {
        this.missionMan.MissionGeneral = this.General;
        //this.missionDetailsWindow.SetActive(false);
        //this.mainMenueController.ActivateDeployUI(true);
        //this.mainMenueController.ToggleMenue(2);
        screenStateMach.SetToState(ScreenStateMachine.WindowStates.SQUAD_SELECT);
    }

    // Use this for initialization
    private void Start() {
        //this.mainMenueController = GameObject.Find("/MissionMap/Canvas/MainMenue/").GetComponent<MainMenueController>();
        this.missionMan = GameObject.Find("/ReferenceShare").GetComponent<MissionManager>();
        //this.missionDetailsWindow = GameObject.Find("/MissionMap/Canvas/MissionWindow");
        this.screenStateMach = GameObject.Find("/MissionMap").GetComponent<ScreenStateMachine>();
    }
}
