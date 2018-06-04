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

    /// <summary>
    /// Triggers one time on application pause or unpause
    /// </summary>
    /// <param name="pauseStatus">True if game is paused</param>
    void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            loginTimer = 0f;
            testText.text = "Willkommen";
            //Player is back
        }
    }

    private void Update() {
        loginTimer += Time.deltaTime;
        if (loginTimer > 5f) {
            testText.text = "";
        }
    }
}
