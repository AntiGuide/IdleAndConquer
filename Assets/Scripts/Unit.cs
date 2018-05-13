using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit {

#pragma warning disable 0414
    private string unitName;
    private int hp;
    private int attack;
    private int defense;
    private int type;
    private int cost;
    private float buildtime;
#pragma warning restore 0414

    public Unit(string unitName, int hp, int attack, int defense, int type, int cost, float buildtime) {
        this.unitName = unitName;
        this.hp = hp;
        this.attack = attack;
        this.defense = defense;
        this.type = type;
        this.cost = cost;
        this.buildtime = buildtime;
    }   

    internal void Order(ref int availableUnits, ref MoneyManagement moneyManager, ref PowerlevelManagement powerlevelManager) {
        if (moneyManager.subMoney(cost)) {
            availableUnits++;
            powerlevelManager.addPowerlevel(Mathf.RoundToInt((hp * attack * defense) / 1000));
        }
    }
}
