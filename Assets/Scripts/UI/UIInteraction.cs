using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the 5 main buttons on the bottom of the screen
/// </summary>
public class UIInteraction : MonoBehaviour {
    /// <summary>Used to toggle menue</summary>
    public MainMenueController MainMenueControll;

    /// <summary>Used to trigger sound</summary>
    public SoundController SoundControll;

    /// <summary>Used to switch scenes</summary>
    public GameObject MissionMapContainer;

    /// <summary>Used to switch scenes</summary>
    public GameObject MainContainer;

    public BuildConfirmDialogHandler BuildConfirmDialogHandle;

    /// <summary>Behaviour for button 1</summary>
    public void OpenButton1() {
        this.CheckPlayerBuilding();
        this.MissionMapLoad();
    }

    /// <summary>Behaviour for button 2</summary>
    public void OpenButton2() {
        this.CheckPlayerBuilding();
        this.SoundControll.StartSound(SoundController.Sounds.MENUE_TAPS);
        this.MainMenueControll.ToggleMenue(2);
    }

    /// <summary>Behaviour for button 3</summary>
    public void OpenButton3() {
        this.CheckPlayerBuilding();
        this.SoundControll.StartSound(SoundController.Sounds.MENUE_TAPS);
        this.MainMenueControll.ToggleMenue(3);
    }

    /// <summary>Behaviour for button 4</summary>
    public void OpenButton4() {
        this.CheckPlayerBuilding();
        this.SoundControll.StartSound(SoundController.Sounds.MENUE_TAPS);
        this.MainMenueControll.ToggleMenue(4);
    }

    /// <summary>Behaviour for button 5</summary>
    public void OpenButton5() {
        this.CheckPlayerBuilding();
        this.SoundControll.StartSound(SoundController.Sounds.MENUE_TAPS);
        this.MainMenueControll.ToggleMenue(5);
    }

    public void MissionMapLoad() {
        this.SoundControll.StartSound(SoundController.Sounds.SWITCHBASE_TO_MISSION);
        this.MainContainer.SetActive(false);
        this.MissionMapContainer.SetActive(true);
    }

    public void MainLoad() {
        this.SoundControll.StartSound(SoundController.Sounds.SWITCHBASE_TO_MISSION);
        this.MissionMapContainer.SetActive(false);
        this.MainContainer.SetActive(true);
    }

    private void CheckPlayerBuilding() {
        if (this.BuildConfirmDialogHandle.isActiveAndEnabled) {
            this.BuildConfirmDialogHandle.DenyClick();
        }
    }
}
