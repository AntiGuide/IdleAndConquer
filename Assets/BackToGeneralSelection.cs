using UnityEngine;

/// <summary>
/// Used for navigating back to the generals tab from the deployment tab on the mission map.
/// </summary>
public class BackToGeneralSelection : MonoBehaviour {
    /// <summary>Reference to the used MainMenueController. Used for expanding menues</summary>
    public MainMenueController MainMenueControll;

    /// <summary>
    /// Triggered on click on the back button
    /// </summary>
    public void OnClick() {
        this.MainMenueControll.ToggleMenue(1);
        this.MainMenueControll.ActivateDeployUI(false);
        SelectedGeneral.General = null;
    }
}
