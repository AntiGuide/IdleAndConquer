using UnityEngine;

/// <summary>Handles click event of dialog buttons</summary>
public class BuildConfirmDialogHandler : MonoBehaviour {
    /// <summary>Refernce to BaseSwitcher to get the correct builder</summary>
    public BaseSwitcher BaseSwitch;

    public SoundController soundController;

    public void AcceptClick() {
        this.BaseSwitch.GetBuilder().ConfirmBuildingProcess();
        this.soundController.StartSound(SoundController.Sounds.BUILDING);
    }

    public void DenyClick() {
        this.BaseSwitch.GetBuilder().CancelBuildingProcess();
        this.soundController.StartSound(SoundController.Sounds.CANCEL_SELL);
    }
}
