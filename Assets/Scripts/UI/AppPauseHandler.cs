using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to handle the routine of the player coming back and leaving to the game
/// </summary>
public class AppPauseHandler : MonoBehaviour {

    public Text testText;

    /// <summary>Time since login</summary>
    private float loginTimer = 0f;

    /// <summary>The date and time the user paused the app</summary>
    private DateTime exitTime;

    /// <summary>
    /// Triggers one time on application pause or unpause
    /// </summary>
    /// <param name="pauseStatus">True if game is paused</param>
    void OnApplicationPause(bool pauseStatus) {
        Debug.Log("OnApplicationPause(" + pauseStatus.ToString() + ")");
        if (!pauseStatus) {
            loginTimer = 0f;
            if (exitTime.CompareTo(DateTime.MinValue) == 0) {
                // First start/New start
                testText.text = "Willkommen";
            } else {
                // Player back from pause
                testText.text = "Willkommen" + Environment.NewLine + Math.Round((System.DateTime.Now - exitTime).TotalSeconds) + " Sekunden Abwesenheit";
            }
            // Compate Times
            // Player is back
        } else {
            exitTime = System.DateTime.Now;
        }
    }

    private void Start() {
        // Set exitTime to PlayerPref Application Quit?
    }

    private void Update() {
        if (loginTimer > 5f) {
            testText.text = "";
        } else {
            loginTimer += Time.deltaTime;
        }
    }
}
