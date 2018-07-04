using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMain : MonoBehaviour {
    public UIInteraction UIInteractions;
    public MissionManager MissionMan;
    public MainMenueController MainMenueControll;

    public void OnClick() {
        OnClickDeploy.DeployedUnits = 0;
        MissionMan.Reset();
        MainMenueControll.ActivateDeployUI(false);
        UIInteractions.MainLoad();
    }
}
