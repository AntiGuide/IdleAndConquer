using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagement : MonoBehaviour {

    private static long money;

    // Use this for initialization
    void Start () {
        setMoney(20000);
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void addMoney(long moneyToAdd) {
        money = money + moneyToAdd;
        outputMoney(money);
    }

    public bool subMoney(long moneyToSub) {
        if (money >= moneyToSub) {
            money = money - moneyToSub;
            outputMoney(money);
            return true;
        } else {
            return false;
        }
    }
    public bool hasMoney(long moneyToCheck) {
        return money >= moneyToCheck;
    }

    public void setMoney(long valueToSet) {
        if (valueToSet >= 0) {
            money = valueToSet;
            outputMoney(money);
        } else {
            throw new ArgumentException("Can not set a negative Dollar Value","valueToSet");
        }
    }

    private void outputMoney(long money) {
        GetComponent<Text>().text = formatMoney(money);
    }

    public static string formatMoney(long money) {
        CultureInfo cultureInfo = new CultureInfo("de-DE", false);
        cultureInfo.NumberFormat.CurrencySymbol = "$";
        return money.ToString("C0", cultureInfo);
    }
}
