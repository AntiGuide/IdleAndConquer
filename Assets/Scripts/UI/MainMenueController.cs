using UnityEngine;

/// <summary>
/// Controlls expansion and unexpansion of the MainMenue (Slider from bottom)
/// </summary>
public class MainMenueController : MonoBehaviour {
    /// <summary>Contains all menues as GameObjects to activate, deactivate and get all needed components</summary>
    public GameObject[] Menue;

    /// <summary>Contains menue for deploying units to a mission on the mission map</summary>
    public GameObject DeployUI;

    /// <summary>Contains information if any menue is expanded at the moment</summary>
    private bool isExpanded;

    /// <summary>Contains the index of the activated menue</summary>
    private int enabledMenue;

    /// <summary>Holds all MenueController components of the menues specified in "GameObject[] Menue"</summary>
    private MenueController[] menueController;

    /// <summary>Getter/Setter for the isExpanded variable</summary>
    public bool IsExpanded {
        get { return isExpanded; }
        set { isExpanded = value; }
    }

    /// <summary>
    /// This method opens the called type of menue instantly an without an animation.
    /// </summary>
    /// <param name="menueNumber">Number of menue to open</param>
    public void ToggleMenue(int menueNumber) {
        menueNumber--;

        if (this.enabledMenue != menueNumber) {
            if (this.Menue[menueNumber] == null) {
                return;
            }

            if (this.enabledMenue != -1) {
                this.Menue[this.enabledMenue].SetActive(false);
            }

            this.Menue[menueNumber].SetActive(true);
            this.enabledMenue = menueNumber;
            this.menueController[this.enabledMenue].Expand(!isExpanded);
        } else {
            if (isExpanded) {
                this.menueController[this.enabledMenue].Unexpand(true);
            } else {
                this.menueController[this.enabledMenue].Expand(true);
            }
        }
    }

    /// <summary>
    /// Toggles the specified menueController. Function searches matching index and calls ToggleMenue if it found something.
    /// </summary>
    /// <param name="menueCon">The MenueController to be toggled</param>
    public void ToggleMenue(MenueController menueCon) {
        for (int i = 0; i < this.menueController.Length; i++) {
            if (menueCon == this.menueController[i]) {
                this.ToggleMenue(i + 1);
            }
        }
    }

    /// <summary>
    /// Get the MenueController of the enabled Menue
    /// </summary>
    /// <returns>Returns the MenueController of the enabled Menue</returns>
    public MenueController GetActiveMenueController() {
        return this.menueController[this.enabledMenue];
    }

    /// <summary>Sets the DeployUI active or inactive. The DeployUI is the UI used to send Units to missions.</summary>
    /// <param name="val">The value contains info wether the UI should be activated (true) or deactivated (false)</param>
    public void ActivateDeployUI(bool val) {
        if (!val) {
            Transform unitContainer = this.DeployUI.transform.Find("BG").Find("UnitContainer");
            for (int i = 0; i < unitContainer.childCount; i++) {
                Destroy(unitContainer.GetChild(i).gameObject);
            }
        }
        this.DeployUI.SetActive(val);
    }

    /// <summary>Use this for initialization</summary>
    void Start() {
        this.menueController = new MenueController[this.Menue.Length];
        int i = 0;
        bool standardSelected = false;

        foreach (GameObject men in this.Menue) {
            if (men != null && !standardSelected) {
                men.SetActive(true);
                this.enabledMenue = i;
                isExpanded = false;
                standardSelected = true;
            } else if (men != null) {
                men.SetActive(false);
            }

            i++;
        }

        i = 0;
        foreach (GameObject men in this.Menue) {
            if (men != null) {
                this.menueController[i] = men.GetComponent<MenueController>();
            }

            i++;
        }
    }
}
