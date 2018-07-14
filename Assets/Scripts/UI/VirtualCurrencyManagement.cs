using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manages the virtual currency (V) of the player</summary>
public class VirtualCurrencyManagement : MonoBehaviour {
    /// <summary>The time the V takes to lerp to a specific value</summary>
    public float LerpTimeStart = 1.0f;

    /// <summary>Saves the amout of V the player has at the moment</summary>
    private static long virtualCurrency;

    /// <summary>How much seconds of the lerp are completed</summary>
    private float lerpTimeDone = 0.0f;

    /// <summary>The amount of V that the player lerps from</summary>
    private long virtualCurrencyAmountOld;

    /// <summary>The amount of V that is shown at the moment</summary>
    private long virtualCurrencyAmountShown;

    /// <summary>The amount of V that is goal of the lerp</summary>
    private long virtualCurrencyToLerpTo;

    /// <summary>
    /// Adds V
    /// </summary>
    /// <param name="virtualCurrencyToAdd">The amount to add</param>
    public void AddVirtualCurrency(long virtualCurrencyToAdd) {
        virtualCurrency = virtualCurrency + virtualCurrencyToAdd;
        PlayerPrefs.SetInt("virtualCurrency", (int)VirtualCurrencyManagement.virtualCurrency);
        this.OutputMoney(virtualCurrency, true);
    }

    /// <summary>
    /// Subs V
    /// </summary>
    /// <param name="virtualCurrencyToSub">The amount to sub</param>
    /// <returns>If the player had enough V for the transaction. True is returned and the transaction is performed. (False --> no transaction)</returns>
    public bool SubVirtualCurrency(long virtualCurrencyToSub) {
        if (virtualCurrency >= virtualCurrencyToSub) {
            virtualCurrency = virtualCurrency - virtualCurrencyToSub;
            PlayerPrefs.SetInt("virtualCurrency", (int)VirtualCurrencyManagement.virtualCurrency);
            this.OutputMoney(virtualCurrency, true);
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Checks V without subbing it.
    /// </summary>
    /// <param name="virtualCurrencyToCheck">How much should be checked</param>
    /// <returns>Returns if player has at least this amount of V</returns>
    public bool HasVirtualCurrency(long virtualCurrencyToCheck) {
        return virtualCurrency >= virtualCurrencyToCheck;
    }

    /// <summary>
    /// Sets the V to a specific amount
    /// </summary>
    /// <param name="valueToSet">To how much V should the players account be set to</param>
    public void SetVirtualCurrency(long valueToSet) {
        if (valueToSet >= 0) {
            virtualCurrency = valueToSet;
            PlayerPrefs.SetInt("virtualCurrency", (int)VirtualCurrencyManagement.virtualCurrency);
            this.OutputMoney(virtualCurrency, false);
        } else {
            throw new ArgumentException("Can not set a negative Dollar Value", "valueToSet");
        }
    }

    /// <summary>Sets the V to 0 in the beginning</summary>
    void Start() {
        this.SetVirtualCurrency(PlayerPrefs.GetInt("virtualCurrency", 0));
    }

    /// <summary>Called once per frame</summary>
    private void Update() {
        if (this.virtualCurrencyToLerpTo != this.virtualCurrencyAmountShown) {
            this.lerpTimeDone += Time.deltaTime;
            this.lerpTimeDone = Mathf.Min(this.LerpTimeStart, this.lerpTimeDone);
            this.virtualCurrencyAmountShown = (long)Mathf.Lerp(this.virtualCurrencyAmountOld, this.virtualCurrencyToLerpTo, this.lerpTimeDone / this.LerpTimeStart);
            this.GetComponent<Text>().text = this.virtualCurrencyAmountShown.ToString();
        }
    }

    /// <summary>
    /// Display the V
    /// </summary>
    /// <param name="virtualCurrency">How much V to display</param>
    /// <param name="lerpEffect">Wether the change should be applied instantly or with a lerp effect</param>
    private void OutputMoney(long virtualCurrency, bool lerpEffect) {
        if (lerpEffect) {
            this.virtualCurrencyAmountOld = this.virtualCurrencyAmountShown;
            this.virtualCurrencyToLerpTo = virtualCurrency;
            this.lerpTimeDone = 0.0f;
        } else {
            this.GetComponent<Text>().text = virtualCurrency.ToString();
            this.virtualCurrencyAmountShown = virtualCurrency;
            this.virtualCurrencyToLerpTo = virtualCurrency;
        }
    }
}
