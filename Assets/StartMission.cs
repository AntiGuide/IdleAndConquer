using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Start a mission on click of a button on the mission map</summary>
public class StartMission : MonoBehaviour {
    /// <summary>
    /// Triggered when the player clicks the button on the mission map to start a single mission
    /// </summary>
    public void OnClick() {
        MissionManager.StartMission();
    }
}
