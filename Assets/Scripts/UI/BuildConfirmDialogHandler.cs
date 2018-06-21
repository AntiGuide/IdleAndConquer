using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles click event of dialog buttons</summary>
public class BuildConfirmDialogHandler : MonoBehaviour {
    /// <summary>Refernce to BaseSwitcher to get the correct builder</summary>
    public BaseSwitcher BaseSwitch;

    public void AcceptClick() {
        this.BaseSwitch.GetBuilder().ConfirmBuildingProcess();
    }

    public void DenyClick() {
        this.BaseSwitch.GetBuilder().CancelBuildingProcess();
    }
}
