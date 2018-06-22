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
    public __SceneSwitch SceneSwitch;

    /// <summary>Behaviour for button 1</summary>
    public void OpenButton1() {
        this.SceneSwitch.MissionMapLoad();
    }

    /// <summary>Behaviour for button 2</summary>
    public void OpenButton2() {
        this.MainMenueControll.ToggleMenue(2);
    }

    /// <summary>Behaviour for button 3</summary>
    public void OpenButton3() {
        this.SoundControll.StartSound(SoundController.Sounds.BUTTON_CLICK);
        this.MainMenueControll.ToggleMenue(3);
    }

    /// <summary>Behaviour for button 4</summary>
    public void OpenButton4() {
        this.MainMenueControll.ToggleMenue(4);
    }

    /// <summary>Behaviour for button 5</summary>
    public void OpenButton5() {
        this.MainMenueControll.ToggleMenue(5);
    }
}
