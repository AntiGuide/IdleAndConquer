using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Controlls expansion and unexpansion of the MainMenue (Slider from bottom)
/// </summary>
public class MainMenueController : MonoBehaviour {
    /// <summary>Contains all menues as GameObjects to activate, deactivate and get all needed components</summary>
    public GameObject[] Menue;

    /// <summary>Contains menue for deploying units to a mission on the mission map</summary>
    public GameObject DeployUI;

    public GameObject CloseMainMenueGameObject;

    /// <summary>Contains the index of the activated menue</summary>
    public int EnabledMenue;

    /// <summary>Holds all MenueController components of the menues specified in "GameObject[] Menue"</summary>
    private MenueController[] menueController;

    /// <summary>Getter/Setter for the isExpanded variable</summary>
    public bool IsExpanded { get; set; }

    /// <summary>
    /// This method opens the called type of menue instantly an without an animation.
    /// </summary>
    /// <param name="menueNumber">Number of menue to open</param>
    public void ToggleMenue(int menueNumber) {
        Profiler.BeginSample("MainMenueController.ToggleMenue()");
        menueNumber--;

        if (this.EnabledMenue != menueNumber) {
            if (this.Menue[menueNumber] == null) {
                return;
            }

            if (this.EnabledMenue != -1) {
                this.Menue[this.EnabledMenue].GetComponent<Canvas>().enabled = false;
            }

            this.Menue[menueNumber].GetComponent<Canvas>().enabled = true;
            this.EnabledMenue = menueNumber;
            CloseMainMenueGameObject.SetActive(true);
            this.menueController[this.EnabledMenue].Expand(!this.IsExpanded);
        } else {
            if (this.IsExpanded) {
                CloseMainMenueGameObject.SetActive(false);
                this.menueController[this.EnabledMenue].Unexpand(true);
            } else {
                CloseMainMenueGameObject.SetActive(true);
                this.menueController[this.EnabledMenue].Expand(true);
            }
        }
        Profiler.EndSample();
    }

    /// <summary>
    /// Toggles the specified menueController. Function searches matching index and calls ToggleMenue if it found something.
    /// </summary>
    /// <param name="menueCon">The MenueController to be toggled</param>
    public void ToggleMenue(MenueController menueCon) {
        for (var i = 0; i < this.menueController.Length; i++) {
            if (menueCon == this.menueController[i]) {
                this.ToggleMenue(i + 1);
            }
        }
    }

    public void Unexpand() {
        if (!this.IsExpanded) { return; }

        ToggleMenue(EnabledMenue + 1);
    }

    /// <summary>
    /// Get the MenueController of the enabled Menue
    /// </summary>
    /// <returns>Returns the MenueController of the enabled Menue</returns>
    public MenueController GetActiveMenueController() {
        return this.menueController[this.EnabledMenue];
    }

    /// <summary>Sets the DeployUI active or inactive. The DeployUI is the UI used to send Units to missions.</summary>
    /// <param name="val">The value contains info wether the UI should be activated (true) or deactivated (false)</param>
    public void ActivateDeployUI(bool val) {
        //if (!val) {
        //    var unitContainer = this.DeployUI.transform.Find("BG").Find("UnitContainer");
        //    for (var i = 0; i < unitContainer.childCount; i++) {
        //        UnityEngine.Object.Destroy(unitContainer.GetChild(i).gameObject);
        //    }
        //}

        this.DeployUI.SetActive(val);
    }

    /// <summary>Use this for initialization</summary>
    private void Start() {
        this.menueController = new MenueController[this.Menue.Length];
        var i = 0;
        var standardSelected = false;
        foreach (var men in Menue) {
            if (men != null) {
                men.SetActive(true);
            }
        }

        foreach (var men in this.Menue) {
            if (men != null && !standardSelected) {
                //men.SetActive(true);
                this.EnabledMenue = i;
                this.IsExpanded = false;
                standardSelected = true;
            } else if (men != null) {
                men.GetComponent<Canvas>().enabled = false;
                //men.SetActive(false);
            }

            i++;
        }

        i = 0;
        foreach (var men in this.Menue) {
            if (men != null) {
                this.menueController[i] = men.GetComponent<MenueController>();
            }

            i++;
        }
    }
}
