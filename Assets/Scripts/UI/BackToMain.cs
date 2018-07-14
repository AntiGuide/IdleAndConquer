using UnityEngine;

public class BackToMain : MonoBehaviour {
    public UIInteraction UIInteractions;
    public MissionManager MissionMan;
    public MainMenueController MainMenueControll;

    public void OnClick() {
        OnClickDeploy.DeployedUnits = 0;
        this.MissionMan.Reset();
        this.MainMenueControll.ActivateDeployUI(false);
        this.UIInteractions.MainLoad();
    }
}
