using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit {

#pragma warning disable 0414
    private string unitName;
    private Type type;
    private ArmorType armorType;
    private int hp;
    private int attack;
    private float critChance;
    private float critMultiplier;
    private int defense;
    private int cost;
    private CreateAndOrderUnit cAOButton;
    private int unitCount;
    private ProductionQueue productionQueue;

    private float buildtime;
    public float Buildtime {
        get {
            return buildtime;
        }

        set {
            buildtime = value;
        }
    }
#pragma warning restore 0414

    public enum Type:int
    {
        TANK = 0,
        SOLDIER,
        PLANE
    }

    public enum ArmorType : int
    {
        NONE = 0,
        LIGHT,
        MEDIUM,
        HEAVY
    }

    public Unit(string unitName, int hp, int attack, float critChance, float critMultiplier, int defense, Type type, ArmorType armorType, int cost, float buildtime, CreateAndOrderUnit cAOButton, ProductionQueue productionQueue) {
        this.unitName = unitName;
        this.hp = hp;
        this.attack = attack;
        this.critChance = critChance;
        this.critMultiplier = critMultiplier;
        this.defense = defense;
        this.type = type;
        this.armorType = armorType;
        this.cost = cost;
        this.buildtime = buildtime;
        this.cAOButton = cAOButton;
        this.productionQueue = productionQueue;
    }   

    internal void Order(ref int availableUnits, ref MoneyManagement moneyManager, ref PowerlevelManagement powerlevelManager) {
        if (moneyManager.subMoney(cost)) {
            productionQueue.addToQueue(this, cAOButton);
            cAOButton.addSingleUnitBuilding();
        }
    }

    public int getAttack(Unit enemyUnit) {
        int returnDamage = attack;
        returnDamage += Passives.GetAbsolutPassive(type, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassive(type, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveArmortype(armorType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveArmortype(armorType, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveAgainstType(enemyUnit.GetUnitType(), Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveAgainstType(enemyUnit.GetUnitType(), Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveAgainstArmortype(enemyUnit.GetUnitArmortype(), Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveAgainstArmortype(enemyUnit.GetUnitArmortype(), Passives.Value.ATTACK));

        return returnDamage;
    }

    private ArmorType GetUnitArmortype() {
        return armorType;
    }

    public int getAttack() {
        int returnDamage = attack;
        returnDamage += Passives.GetAbsolutPassive(type, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassive(type, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveArmortype(armorType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveArmortype(armorType, Passives.Value.ATTACK));

        return returnDamage;
    }

    public Type GetUnitType() {
        return type;
    }

    public void addSingleBuiltUnit() {
        cAOButton.setUnitCount((++unitCount).ToString());
        cAOButton.addPowerlevel(Mathf.RoundToInt((hp * attack * defense) / 1000));
    }
}
