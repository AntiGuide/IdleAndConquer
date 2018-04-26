using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit {

#pragma warning disable 0414
    private string unitName;
    private int cost;
    private int damage;
    private int defense;
    private float buildtime;
#pragma warning restore 0414

    public Unit(string unitName, int cost, int damage, int defense, float buildtime) {
        this.unitName = unitName;
        this.cost = cost;
        this.damage = damage;
        this.defense = defense;
        this.buildtime = buildtime;
    }

    internal void Order(ref int availableUnits, ref MoneyManagement moneyManager) {
        if (moneyManager.subMoney(cost)) {
            availableUnits++;
        }
    }
}
