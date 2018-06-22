using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the players powerlevel
/// </summary>
public class PowerlevelManagement : MonoBehaviour {
    /// <summary>Reference to the FloatUpSpawner to spawn FloatUps on PowerLevel gain</summary>
    public FloatUpSpawner floatUpSpawner;

    /// <summary>The players powerlevel</summary>
    private static long powerlevel = 0;

    /// <summary>
    /// Adds to the players powerlevel
    /// </summary>
    /// <param name="powerlevelToAdd">How much PowerLevel to add</param>
    /// <param name="supressed">Should the Floatup be supressed</param>
    public void AddPowerlevel(long powerlevelToAdd, bool supressed) {
        powerlevel = powerlevel + powerlevelToAdd;
        if (!supressed) {
            this.floatUpSpawner.GenerateFloatUp(powerlevelToAdd, FloatUp.ResourceType.POWERLEVEL, transform.position);
        }

        this.OutputPowerlevel(ref powerlevel);
    }

    /// <summary>
    /// Subs from the players powerlevel
    /// </summary>
    /// <param name="powerlevelToSub">How much PowerLevel to sub</param>
    /// <returns>Returns if the player had enough PowerLevel to sub the amount</returns>
    public bool SubPowerlevel(long powerlevelToSub) {
        if (powerlevel >= powerlevelToSub) {
            powerlevel = powerlevel - powerlevelToSub;
            this.OutputPowerlevel(ref powerlevel);
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Sets the PowerLevel to a value
    /// </summary>
    /// <param name="valueToSet">Which value to set the PowerLevel to</param>
    public void SetPowerlevel(long valueToSet) {
        if (valueToSet >= 0) {
            powerlevel = valueToSet;
            this.OutputPowerlevel(ref powerlevel);
        } else {
            throw new ArgumentException("Can not set a negative Dollar Value", "valueToSet");
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {
        this.OutputPowerlevel(ref powerlevel);
    }

    /// <summary>
    /// Outputs the Powerlevel
    /// </summary>
    /// <param name="powerlevel">Which amount of Powerlevel to output</param>
    private void OutputPowerlevel(ref long powerlevel) {
        GetComponent<Text>().text = powerlevel.ToString() + " PL";
    }
}