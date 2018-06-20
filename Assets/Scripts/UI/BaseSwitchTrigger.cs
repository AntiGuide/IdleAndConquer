using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to call base switching on player input
/// </summary>
public class BaseSwitchTrigger : MonoBehaviour {
    /// <summary>The base switcher manager to call the switch in</summary>
    public BaseSwitcher BaseSwitch;

    /// <summary>
    /// Switches the base after a button click
    /// </summary>
    /// <param name="isLeft">If true the base should be switched to the one on the left. If false its to the right</param>
    public void OnClickBaseSwitch(bool isLeft) {
        bool leftPossible = false;
        bool rightPossible = false;

        this.BaseSwitch.OnClickBaseSwitch(isLeft, out leftPossible, out rightPossible);

        // WIP
    }
}
