using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles all tanks, planes and soldiers with all their values</summary>
public class Unit : MonoBehaviour{
    public static readonly float[] HPBoostLevel = { 1f, 1.05f, 1.075f, 1.1f, 1.125f, 1.15f, 1.175f, 1.2f, 1.225f, 1.25f };

    public static readonly List<Unit> AllUnits = new List<Unit>();

    public int SentToMission = 0;

    private static readonly int[] OtherBoostLevel = { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12 };

    private static readonly int[] ArmorTypeLevel = { 0, 0, 0, 0 };

    private static readonly int[] HPGroupLevel = { 0, 0, 0 };

    private static readonly int[] ATKGroupLevel = { 0, 0, 0 };

    private static readonly int[] CritGroupLevel = { 0, 0, 0 };

    private static readonly int[] BuildtimeGroupLevel = { 0, 0, 0 };

    private int Level = 0;

    // public static int[] LevelGroup = { 0, 0, 0 };

    /// <summary>The script that triggeres a production of this unit</summary>
    private CreateAndOrderUnit CreateAndOrderButton;

    /// <summary>The used to get the correct production queue</summary>
    private BaseSwitcher baseSwitch;

    /// <summary>Healthpoints of the unit e.g. 100</summary>
    private int hp;

    /// <summary>Attackvalue of the unit e.g. 20</summary> 
    private int attack; 

    /// <summary>The defense value of a unit</summary>
    private int defense;

    /// <summary>Chance to hit a critical hit on another unit e.g. 0,1 = 10%</summary>
    private float critChance;

    /// <summary>The time it takes to build this unit in seconds</summary>
    private float buildtime;

    public int Powerlevel {
        get { return Mathf.RoundToInt(this.hp * this.attack * this.defense / 1000f); }
    }

    /// <summary>
    /// Creates a unit with all parameters
    /// </summary>
    /// <param name="unitName">Name of the unit e.g. Tank 1</param>
    /// <param name="hp">Healthpoints of the unit e.g. 100</param>
    /// <param name="attack">Attackvalue of the unit e.g. 20</param>
    /// <param name="critChance">Chance to hit a critical hit on another unit e.g. 0,1 = 10%</param>
    /// <param name="defense">The defense value of a unit</param>
    /// <param name="type">The type of the unit. (Tank, Soldier or Plane)</param>
    /// <param name="armorType">The armor type of the unit. Important for passives</param>
    /// <param name="buildtime">The time it takes to build this unit in seconds</param>
    /// <param name="createAndOrderButton">The script that triggeres a production of this unit</param>
    /// <param name="baseSwitch">The script that handles a base switch</param>
    public Unit Initialize(string unitName, int hp, int attack, float critChance, int defense, Type type, ArmorType armorType, float buildtime, CreateAndOrderUnit createAndOrderButton, BaseSwitcher baseSwitch) {
        this.UnitName = unitName;
        this.hp = hp;
        this.attack = attack;
        this.critChance = critChance;
        this.defense = defense;
        this.UnitType = type;
        this.ArmorTypeUnit = armorType;
        this.buildtime = buildtime;
        this.CreateAndOrderButton = createAndOrderButton;
        this.baseSwitch = baseSwitch;
        AllUnits.Add(this);
        return this;
    }

    /// <summary>The type of the unit. (Tank, Soldier or Plane)</summary>
    public enum Type {
        TANK = 0,
        SOLDIER,
        PLANE
    }

    /// <summary>The armor type of the unit. Important for passives</summary>
    public enum ArmorType {
        NONE = 0,
        LIGHT,
        MEDIUM,
        HEAVY
    }

    /// <summary>Getter and setter for buildtime</summary>
    public float Buildtime {
        get { return this.buildtime - (this.buildtime * Unit.HPBoostLevel[Unit.BuildtimeGroupLevel[(int)this.UnitType]] - this.buildtime); }
    }

    /// <summary>Getter and setter for unitCount</summary>
    public int UnitCount { get; set; }

    /// <summary>Getter and setter for unitName</summary>
    public string UnitName { get; private set; }

    /// <summary>Getter and setter for armorType</summary>
    private ArmorType ArmorTypeUnit { get; set; }

    /// <summary>Getter and setter for type (tank, plane...)</summary>
    public Type UnitType { get; set; }

    public int AttackRaw { get; private set; }

    /// <summary>
    /// This method tries to order a specific unit. If the player has enough money the queue is started.
    /// </summary>
    /// <param name="moneyManager">The reference to the players money pool</param>
    public void Order(ref MoneyManagement moneyManager) {
        if (!moneyManager.SubMoney(this.CreateAndOrderButton.Cost)) return;
        this.baseSwitch.GetProductionQueue().AddToQueue(this, this.CreateAndOrderButton);
        this.CreateAndOrderButton.AddSingleUnitBuilding();
    }

    /// <summary>
    /// Calculates the attack value against an enemy unit type
    /// </summary>
    /// <param name="enemyUnit">The enemy unit type which the attack value should be calculated for.</param>
    /// <returns>Returns the attack value/the damage calculated</returns>
    public int GetAttack(Unit enemyUnit) {
        var returnDamage = this.attack + Unit.OtherBoostLevel[this.Level] + Unit.OtherBoostLevel[Unit.ATKGroupLevel[(int)this.UnitType]];
        returnDamage += Passives.GetAbsolutPassive(this.UnitType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassive(this.UnitType, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveArmortype(this.ArmorTypeUnit, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveArmortype(this.ArmorTypeUnit, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveAgainstType(enemyUnit.UnitType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveAgainstType(enemyUnit.UnitType, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveAgainstArmortype(enemyUnit.ArmorTypeUnit, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveAgainstArmortype(enemyUnit.ArmorTypeUnit, Passives.Value.ATTACK));

        return returnDamage;
    }

    /// <summary>
    /// Calculates the attack value against an enemy unit type
    /// </summary>
    /// <param name="enemyUnitType">The enemy unit type which the attack value should be calculated for.</param>
    /// <param name="armorUnitType">The enemy unit armor type which the attack value should be calculated for.</param>
    /// <returns>Returns the attack value/the damage calculated</returns>
    public int GetAttack(Unit.Type enemyUnitType, Unit.ArmorType armorUnitType) {
        var returnDamage = this.attack + Unit.OtherBoostLevel[this.Level] + Unit.OtherBoostLevel[Unit.ATKGroupLevel[(int)this.UnitType]];
        returnDamage += Passives.GetAbsolutPassive(this.UnitType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassive(this.UnitType, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveArmortype(this.ArmorTypeUnit, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveArmortype(this.ArmorTypeUnit, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveAgainstType(enemyUnitType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveAgainstType(enemyUnitType, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveAgainstArmortype(armorUnitType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveAgainstArmortype(armorUnitType, Passives.Value.ATTACK));

        return returnDamage;
    }

    public int GetAverageDamage(List<Unit> enemys) {
        var returnAttack = 0;
        foreach (var enemy in enemys) {
            returnAttack += GetAttack(enemy);
        }

        return returnAttack / enemys.Count;
    }

    public float GetCritChance() {
        return this.critChance + Unit.OtherBoostLevel[this.Level] / 100f + Unit.OtherBoostLevel[Unit.CritGroupLevel[(int)this.UnitType]] / 100f;
    }

    /// <summary>
    /// Adds a single unit of this type to the unit count (visually, save game and variable). Also sets power level.
    /// </summary>
    public void AddSingleBuiltUnit() {
        this.CreateAndOrderButton.SetUnitCount((++this.UnitCount).ToString());
        PlayerPrefs.SetInt(this.UnitName + "_COUNT", this.UnitCount);
        this.CreateAndOrderButton.AddPowerlevel(Mathf.RoundToInt(this.hp * this.attack * this.defense / 1000f), false);
    }

    public void KillSingleUnit() {
        if (UnitCount > 0) {
            this.CreateAndOrderButton.SetUnitCount((--this.UnitCount).ToString());
            PlayerPrefs.SetInt(this.UnitName + "_COUNT", this.UnitCount);
            this.CreateAndOrderButton.AddPowerlevel(-Mathf.RoundToInt(this.hp * this.attack * this.defense / 1000f), false);
        } else {
            throw new Exception("Not enough Units to kill one!");
        }
    }

    public void LevelUp() {
        var powerLevelBeforeLevelUp = Mathf.RoundToInt(this.GetHP() * this.GetAttack() * this.GetDef() / 1000f);
        this.Level++;
        var powerLevelAfterLevelUp = Mathf.RoundToInt(this.GetHP() * this.GetAttack() * this.GetDef() / 1000f);
        if (this.UnitCount * (powerLevelAfterLevelUp - powerLevelBeforeLevelUp) > 0) {
            this.CreateAndOrderButton.AddPowerlevel(this.UnitCount * (powerLevelAfterLevelUp - powerLevelBeforeLevelUp), false);
        }
    }

    /// <summary>
    /// Calculates the attack value without the eventual passives against the enemy
    /// </summary>
    private int GetAttack() {
        var returnDamage = this.attack + Unit.OtherBoostLevel[this.Level] + Unit.OtherBoostLevel[Unit.ATKGroupLevel[(int)this.UnitType]];
        returnDamage += Passives.GetAbsolutPassive(this.UnitType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassive(this.UnitType, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveArmortype(this.ArmorTypeUnit, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveArmortype(this.ArmorTypeUnit, Passives.Value.ATTACK));

        return returnDamage;
    }

    public int GetHP() {
        return Mathf.RoundToInt(this.hp * Unit.HPBoostLevel[this.Level] * Unit.HPBoostLevel[Unit.HPGroupLevel[(int)this.UnitType]]);
    }

    public int GetDef() {
        return this.defense + Unit.OtherBoostLevel[this.Level] + Unit.OtherBoostLevel[Unit.ArmorTypeLevel[(int)this.ArmorTypeUnit]];
    }
}
