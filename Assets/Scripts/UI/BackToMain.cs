using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMain : MonoBehaviour {
    public UIInteraction UIInteractions;

    public void OnClick() {
        OnClickDeploy.DeployedUnits = 0;
        MissionManager.BuildingMission = null;
        MissionManager.MainMenueControll.ActivateDeployUI(false);
        UIInteractions.MainLoad();
    }
}
