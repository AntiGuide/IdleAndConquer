﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to call base switching on player input
/// </summary>
public class BaseSwitchTrigger : MonoBehaviour {
    /// <summary>The base switcher manager to call the switch in</summary>
    public BaseSwitcher BaseSwitch;

    /// <summary>Wether the object is the button to the left (true) or right (false)</summary>
    public bool IsLeft;

    /// <summary>Used to tint both buttons after switch</summary>
    public BaseSwitchTrigger OtherButton;

    /// <summary>Used to trigger sound</summary>
    public SoundController SoundControll;

    public BuildConfirmDialogHandler BuildConfirmDialogHandle;

    public BuildAvailabiltyManager BuildAvailabiltyMan;

    public UnitAvailabilityManager UnitAvailabilityMan;

    public MainMenueController MainMenueCont;

    public Image UnavailableImage;

    private bool available = false;

    /// <summary>Reference to the image component to regulate the tint color</summary>
    private Image image;

    /// <summary>
    /// Switches the base after a button click.
    /// If isLeft is true the base should be switched to the one on the left. If false its to the right.
    /// </summary>
    public void OnClickBaseSwitch() {
        if (!available) {
            return;
        }

        this.CheckPlayerBuilding();
        this.BaseSwitch.OnClickBaseSwitch(this.IsLeft);
        this.UpdateButtons();
        this.OtherButton.UpdateButtons();
        this.SoundControll.StartSound(SoundController.Sounds.SWITCHBASE_TO_MISSION, 0.5f);
        BuildAvailabiltyMan.Refresh();
        UnitAvailabilityMan.Refresh();
        MainMenueCont.Unexpand(false);
    }

    /// <summary>Sets the color for the base switch buttons</summary>
    private void UpdateButtons() {
        bool leftPossible;
        bool rightPossible;
        this.BaseSwitch.CheckPossibilities(out leftPossible, out rightPossible);
        if (this.IsLeft) {
            this.image.color = leftPossible ? Color.white : Color.grey;
        } else {
            this.image.color = rightPossible ? Color.white : Color.grey;
        }
    }

    private void Start() {
        this.image = this.GetComponent<Image>();
        this.UpdateButtons();
    }

    private void CheckPlayerBuilding() {
        if (this.BuildConfirmDialogHandle.isActiveAndEnabled) {
            this.BuildConfirmDialogHandle.DenyClick();
        }
    }

    public void Activate() {
        UnavailableImage.color = new Color(0f, 0f, 0f, 0f);
        available = true;
    }

    public void Deactivate() {
        UnavailableImage.color = new Color(0f, 0f, 0f, 0.75f);
        available = false;
    }
}
