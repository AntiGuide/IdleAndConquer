using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manages the money of the player</summary>
public class MoneyManagement : MonoBehaviour {
    /// <summary>Saves the amout of money the player has at the moment</summary>
    private static long money;

    /// <summary>
    /// Formats the money to german format
    /// </summary>
    /// <param name="money">The amount of money</param>
    /// <returns>Formatted money as a string</returns>
    public static string FormatMoney(long money) {
        CultureInfo cultureInfo = new CultureInfo("de-DE", false);
        cultureInfo.NumberFormat.CurrencySymbol = "$";
        return money.ToString("C0", cultureInfo);
    }

    /// <summary>
    /// Adds money
    /// </summary>
    /// <param name="moneyToAdd">The amount to add</param>
    public void AddMoney(long moneyToAdd) {
        money = money + moneyToAdd;
        this.OutputMoney(money);
    }

    /// <summary>
    /// Subs money
    /// </summary>
    /// <param name="moneyToSub">The amount to sub</param>
    /// <returns>If the player had enough money for the transaction. True is returned and the transaction is performed. (False --> no transaction)</returns>
    public bool SubMoney(long moneyToSub) {
        if (money >= moneyToSub) {
            money = money - moneyToSub;
            this.OutputMoney(money);
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Checks money without subbing it.
    /// </summary>
    /// <param name="moneyToCheck">How much should be checked</param>
    /// <returns>Returns if player has at least this amount of money</returns>
    public bool HasMoney(long moneyToCheck) {
        return money >= moneyToCheck;
    }

    /// <summary>
    /// Sets the money to a specific amount
    /// </summary>
    /// <param name="valueToSet">To how much money should the players account be set to</param>
    public void SetMoney(long valueToSet) {
        if (valueToSet >= 0) {
            money = valueToSet;
            this.OutputMoney(money);
        } else {
            throw new ArgumentException("Can not set a negative Dollar Value", "valueToSet");
        }
    }

    /// <summary>Sets the money to 20000 in the beginning</summary>
    void Start() {
        this.SetMoney(20000);
    }

    /// <summary>
    /// Display the money
    /// </summary>
    /// <param name="money">How much money to display</param>
    private void OutputMoney(long money) {
        GetComponent<Text>().text = FormatMoney(money);
    }
}
