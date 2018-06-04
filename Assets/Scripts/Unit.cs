using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles all tanks, planes and soldiers with all their values</summary>
public class Unit {
    /// <summary>The script that triggeres a production of this unit</summary>
    private CreateAndOrderUnit createAndOrderButton;

    /// <summary>The production queue in which a unit in production is put into</summary>
    private ProductionQueue productionQueue;

    /// <summary>The type of the unit. (Tank, Soldier or Plane)</summary>
    private Type type;

    /// <summary>The armor type of the unit. Important for passives</summary>
    private ArmorType armorType;

    /// <summary>Healthpoints of the unit e.g. 100</summary>
    private int hp;

    /// <summary>Attackvalue of the unit e.g. 20</summary>
    private int attack;

    /// <summary>The defense value of a unit</summary>
    private int defense;

    /// <summary>The cost to build a unit of this kind</summary>
    private int cost;

    /// <summary>How many of this unit are available</summary>
    private int unitCount;

    /// <summary>Chance to hit a critical hit on another unit e.g. 0,1 = 10%</summary>
    private float critChance;

    /// <summary>How much percent of normal hit damage a critical hit does e.g. 1,1 = 10% more damage</summary>
    private float critMultiplier;

    /// <summary>The time it takes to build this unit in seconds</summary>
    private float buildtime;

    /// <summary>Name of the unit e.g. Tank 1</summary>
    private string unitName;

    /// <summary>
    /// Creates a unit with all parameters
    /// </summary>
    /// <param name="unitName">Name of the unit e.g. Tank 1</param>
    /// <param name="hp">Healthpoints of the unit e.g. 100</param>
    /// <param name="attack">Attackvalue of the unit e.g. 20</param>
    /// <param name="critChance">Chance to hit a critical hit on another unit e.g. 0,1 = 10%</param>
    /// <param name="critMultiplier">How much percent of normal hit damage a critical hit does e.g. 1,1 = 10% more damage</param>
    /// <param name="defense">The defense value of a unit</param>
    /// <param name="type">The type of the unit. (Tank, Soldier or Plane)</param>
    /// <param name="armorType">The armor type of the unit. Important for passives</param>
    /// <param name="cost">The cost to build a unit of this kind</param>
    /// <param name="buildtime">The time it takes to build this unit in seconds</param>
    /// <param name="createAndOrderButton">The script that triggeres a production of this unit</param>
    /// <param name="productionQueue">The production queue in which a unit in production is put into</param>
    public Unit(string unitName, int hp, int attack, float critChance, float critMultiplier, int defense, Type type, ArmorType armorType, int cost, float buildtime, CreateAndOrderUnit createAndOrderButton, ProductionQueue productionQueue) {
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
        this.createAndOrderButton = createAndOrderButton;
        this.productionQueue = productionQueue;
    }

    /// <summary>
    /// Creates a unit with minimal information. Used for loading units in the mission map screen
    /// </summary>
    /// <param name="unitName">Name of the unit e.g. Tank 1</param>
    /// <param name="unitCount">How many of this unit are available</param>
    public Unit(string unitName, int unitCount) {
        this.unitName = unitName;
        this.unitCount = unitCount;
    }

    /// <summary>The type of the unit. (Tank, Soldier or Plane)</summary>
    public enum Type : int {
        TANK = 0,
        SOLDIER,
        PLANE
    }

    /// <summary>The armor type of the unit. Important for passives</summary>
    public enum ArmorType : int {
        NONE = 0,
        LIGHT,
        MEDIUM,
        HEAVY
    }

    /// <summary>Getter and setter for buildtime</summary>
    public float Buildtime {
        get {
            return this.buildtime;
        }

        set {
            this.buildtime = value;
        }
    }

    /// <summary>Getter and setter for unitCount</summary>
    public int UnitCount {
        get {
            return this.unitCount;
        }

        set {
            this.unitCount = value;
        }
    }

    /// <summary>Getter and setter for unitName</summary>
    public string UnitName {
        get {
            return this.unitName;
        }

        set {
            this.unitName = value;
        }
    }

    /// <summary>Getter and setter for armorType</summary>
    public ArmorType ArmorTypeUnit {
        get {
            return this.armorType;
        }

        set {
            this.armorType = value;
        }
    }

    /// <summary>Getter and setter for type (tank, plane...)</summary>
    public Type UnitType {
        get {
            return this.type;
        }

        set {
            this.type = value;
        }
    }

    /// <summary>
    /// This method tries to order a specific unit. If the player has enough money the queue is started.
    /// </summary>
    /// <param name="moneyManager">The reference to the players money pool</param>
    public void Order(ref MoneyManagement moneyManager) {
        if (moneyManager.subMoney(this.cost)) {
            this.productionQueue.addToQueue(this, this.createAndOrderButton);
            this.createAndOrderButton.AddSingleUnitBuilding();
        }
    }

    /// <summary>
    /// Calculates the attack value against an enemy unit type
    /// </summary>
    /// <param name="enemyUnit">The enemy unit type which the attack value should be calculated for.</param>
    /// <returns>Returns the attack value/the damage calculated</returns>
    public int GetAttack(Unit enemyUnit) {
        int returnDamage = this.attack;
        returnDamage += Passives.GetAbsolutPassive(this.type, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassive(this.type, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveArmortype(this.armorType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveArmortype(this.armorType, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveAgainstType(enemyUnit.UnitType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveAgainstType(enemyUnit.UnitType, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveAgainstArmortype(enemyUnit.ArmorTypeUnit, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveAgainstArmortype(enemyUnit.ArmorTypeUnit, Passives.Value.ATTACK));

        return returnDamage;
    }

    /// <summary>
    /// Calculates the attack value without the eventual passives against the enemy
    /// </summary>
    public int GetAttack() {
        int returnDamage = this.attack;
        returnDamage += Passives.GetAbsolutPassive(this.type, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassive(this.type, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveArmortype(this.armorType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveArmortype(this.armorType, Passives.Value.ATTACK));

        return returnDamage;
    }

    /// <summary>
    /// Adds a single unit of this type to the unit count (visually, save game and variable). Also sets power level.
    /// </summary>
    public void AddSingleBuiltUnit() {
        this.createAndOrderButton.SetUnitCount((++this.unitCount).ToString());
        PlayerPrefs.SetInt(this.unitName + "_COUNT", this.unitCount);
        this.createAndOrderButton.AddPowerlevel(Mathf.RoundToInt((this.hp * this.attack * this.defense) / 1000), false);
    }
}
