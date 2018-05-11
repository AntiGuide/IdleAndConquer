using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagement : MonoBehaviour {

    private static long money;

    // Use this for initialization
    void Start () {
        setMoney(0);
    }
	
	// Update is called once per frame
	void Update () {
        addMoney(1);
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

    public void setMoney(long valueToSet) {
        if (valueToSet >= 0) {
            money = valueToSet;
            outputMoney(money);
        } else {
            throw new ArgumentException("Can not set a negative Dollar Value","valueToSet");
        }
    }

    private void outputMoney(long money) {
        GetComponent<Text>().text = money.ToString() + " $";
    }
}
