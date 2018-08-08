using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFlow : MonoBehaviour {

    public ButtonAvailability[] buttonsBot;

    public UIInteraction UIInter;

    public EnergyManagement Management;

    public BaseSwitchTrigger[] BaseSwitchTrigg;

    // Use this for initialization
    void Start () {
        //Deactivate all buttons beside Building
	    buttonsBot[0].SetUnavailable();
	    buttonsBot[1].SetUnavailable();
	    buttonsBot[2].SetAvailable();
	    buttonsBot[3].SetUnavailable();
	    buttonsBot[4].SetUnavailable();

        UIInter.blockButtons[0] = true;
        UIInter.blockButtons[1] = true;
        UIInter.blockButtons[2] = false;
        UIInter.blockButtons[3] = true;
        UIInter.blockButtons[4] = true;

        BaseSwitchTrigg[0].Deactivate();
        BaseSwitchTrigg[1].Deactivate();


    }
    //barracks, tankfactory, airfield built
    public void BTABuilt() {
        //Unit menue + unit tabs are unlocked
        buttonsBot[3].SetAvailable();
        UIInter.blockButtons[3] = false;
        Management.IsWaitingOnEBackUp = true;
    }

    public void PowerBackUp() {
        //PowerDrops and Goes back up
        //MissionButton + GeneralButton unlocks
        //5x Tank1 + 1 General prebuilt
        buttonsBot[0].SetAvailable();
        buttonsBot[1].SetAvailable();
        buttonsBot[4].SetAvailable();

        UIInter.blockButtons[0] = false;
        UIInter.blockButtons[1] = false;
        UIInter.blockButtons[4] = false;

        BaseSwitchTrigg[0].Activate();
        BaseSwitchTrigg[1].Activate();
    }

}
