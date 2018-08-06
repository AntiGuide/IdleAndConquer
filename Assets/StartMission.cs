using UnityEngine;

/// <summary>Start a mission on click of a button on the mission map</summary>
public class StartMission : MonoBehaviour {
    [SerializeField] private ScreenStateMachine screenStateMachine;

    /// <summary>
    /// Triggered when the player clicks the button on the mission map to start a single mission
    /// </summary>
    public void OnClick() {
        if (OnClickDeploy.DeployedUnits <= 0) {
            return;
        }
        screenStateMachine.NextState();
    }
}
