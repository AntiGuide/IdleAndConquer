using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to handle the routine of the player coming back and leaving to the game
/// </summary>
public class AppPauseHandler : MonoBehaviour {

    public FloatUpSpawner floatUpSpawner;

    public Text TestText;

    public GameObject PlayerBackNotification;

    public Transform ParentPlayerBackNotification;

    public static List<Harvester> Harvesters = new List<Harvester>();

    /// <summary>Time since login</summary>
    private float loginTimer = 0f;

    /// <summary>The date and time the user paused the app</summary>
    private DateTime exitTime;

    /// <summary>
    /// Triggers one time on application pause or unpause
    /// </summary>
    /// <param name="pauseStatus">True if game is paused</param>
    void OnApplicationPause(bool pauseStatus) {
        if (!pauseStatus) {
            loginTimer = 0f;
            if (exitTime.CompareTo(DateTime.MinValue) == 0) {
                // First start/New start
                TestText.text = "Willkommen";
            } else {
                // Player back from pause
                TestText.text = "Willkommen" + Environment.NewLine + Math.Round((DateTime.Now - exitTime).TotalSeconds) + " Sek. Abwesenheit";
                long secondsSincePause = (long)Math.Round((DateTime.Now - exitTime).TotalSeconds);
                long additionalMoney = 0;
                foreach (Harvester h in Harvesters) {
                    additionalMoney += h.AddAppPauseProgressTime(secondsSincePause % (long)h.miningSpeed);
                }
                if (secondsSincePause - (secondsSincePause % (long)Harvesters[0].miningSpeed) > 0 || additionalMoney > 0) {
                    GameObject go = Instantiate(PlayerBackNotification, ParentPlayerBackNotification);
                    PlayerBackNotification pbn = go.GetComponent<PlayerBackNotification>();
                    pbn.Initialize("You earned money while you were gone", floatUpSpawner, FloatUp.ResourceType.DOLLAR, secondsSincePause, additionalMoney, ref Harvesters);
                }
            }
        } else {
            exitTime = System.DateTime.Now;
        }
    }

    private void Start() {
        // Set exitTime to PlayerPref Application Quit?
    }

    private void Update() {
        if (loginTimer > 5f) {
            TestText.text = "";
        } else {
            loginTimer += Time.deltaTime;
        }
    }
}
