using UnityEngine;

/// <summary>Start a mission on click of a button on the mission map</summary>
public class StartMission : MonoBehaviour {
    public MissionManager MissionMan;

    /// <summary>
    /// Triggered when the player clicks the button on the mission map to start a single mission
    /// </summary>
    public void OnClick() {
        this.MissionMan.StartMission();
    }
}
