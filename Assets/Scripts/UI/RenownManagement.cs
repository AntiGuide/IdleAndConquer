using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenownManagement : MonoBehaviour {
    /// <summary>The time the renown takes to lerp to a specific value</summary>
    public float LerpTimeStart = 1.0f;

    /// <summary>Saves the amout of renown the player has at the moment</summary>
    private static long renown;

    /// <summary>How much seconds of the lerp are completed</summary>
    private float lerpTimeDone = 0.0f;

    /// <summary>The amount of renown that the player lerps from</summary>
    private long renownAmountOld;

    /// <summary>The amount of renown that is shown at the moment</summary>
    private long renownAmountShown;

    /// <summary>The amount of renown that is goal of the lerp</summary>
    private long renownToLerpTo;

    /// <summary>
    /// Formats the renown to german format
    /// </summary>
    /// <param name="renown">The amount of renown</param>
    /// <returns>Formatted renown as a string</returns>
    public static string FormatRenown(long renown) {
        return renown.ToString() + " REN";
    }

    /// <summary>
    /// Adds renown
    /// </summary>
    /// <param name="renownToAdd">The amount to add</param>
    public void AddRenown(long renownToAdd) {
        renown = renown + renownToAdd;
        this.OutputRenown(renown, true);
    }

    /// <summary>
    /// Subs renown
    /// </summary>
    /// <param name="renownToSub">The amount to sub</param>
    /// <returns>If the player had enough renown for the transaction. True is returned and the transaction is performed. (False --> no transaction)</returns>
    public bool SubRenown(long renownToSub) {
        if (renown >= renownToSub) {
            renown = renown - renownToSub;
            this.OutputRenown(renown, true);
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Checks renown without subbing it.
    /// </summary>
    /// <param name="renownToCheck">How much should be checked</param>
    /// <returns>Returns if player has at least this amount of renown</returns>
    public bool HasRenown(long renownToCheck) {
        return renown >= renownToCheck;
    }

    /// <summary>
    /// Sets the renown to a specific amount
    /// </summary>
    /// <param name="valueToSet">To how much renown should the players account be set to</param>
    public void SetRenown(long valueToSet) {
        if (valueToSet >= 0) {
            renown = valueToSet;
            this.OutputRenown(renown, false);
        } else {
            throw new ArgumentException("Can not set a negative Dollar Value", "valueToSet");
        }
    }

    /// <summary>Sets the renown to 0 in the beginning</summary>
    void Start() {
        this.SetRenown(0);
    }

    /// <summary>Called once per frame</summary>
    private void Update() {
        if (UnityEngine.Random.Range(0f, 1f) < 0.005f) {
            this.AddRenown(100);
        }

        if (this.renownToLerpTo != this.renownAmountShown) {
            this.lerpTimeDone += Time.deltaTime;
            this.lerpTimeDone = Mathf.Min(this.LerpTimeStart, this.lerpTimeDone);
            this.renownAmountShown = (long)Mathf.Lerp(this.renownAmountOld, this.renownToLerpTo, this.lerpTimeDone / this.LerpTimeStart);
            this.GetComponent<Text>().text = FormatRenown(this.renownAmountShown);
        }
    }

    /// <summary>
    /// Display the renown
    /// </summary>
    /// <param name="renown">How much renown to display</param>
    private void OutputRenown(long renown, bool lerpEffect) {
        if (lerpEffect) {
            this.renownAmountOld = this.renownAmountShown;
            this.renownToLerpTo = renown;
            this.lerpTimeDone = 0.0f;
        } else {
            this.GetComponent<Text>().text = FormatRenown(renown);
            this.renownAmountShown = renown;
            this.renownToLerpTo = renown;
        }
    }
}
