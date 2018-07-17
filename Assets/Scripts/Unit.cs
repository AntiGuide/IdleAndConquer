using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles all tanks, planes and soldiers with all their values</summary>
public class Unit {
    public static readonly float[] HPBoostLevel = { 1f, 1.05f, 1.075f, 1.1f, 1.125f, 1.15f, 1.175f, 1.2f, 1.225f, 1.25f };

    private static readonly int[] OtherBoostLevel = { 0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12 };

    private static readonly int[] ArmorTypeLevel = { 0, 0, 0, 0 };

    private static readonly int[] HPGroupLevel = { 0, 0, 0 };

    private static readonly int[] ATKGroupLevel = { 0, 0, 0 };

    private static readonly int[] CritGroupLevel = { 0, 0, 0 };

    public static int[] CostGroupLevel = { 0, 0, 0 };

    private static readonly int[] BuildtimeGroupLevel = { 0, 0, 0 };

    public static readonly List<Unit> AllUnits = new List<Unit>();

    public int SentToMission = 0;

    private int Level = 0;

    // public static int[] LevelGroup = { 0, 0, 0 };

    /// <summary>The script that triggeres a production of this unit</summary>
    private readonly CreateAndOrderUnit CreateAndOrderButton;

    /// <summary>The used to get the correct production queue</summary>
    private readonly BaseSwitcher baseSwitch;

    /// <summary>The type of the unit. (Tank, Soldier or Plane)</summary>
    private Type type;

    /// <summary>The armor type of the unit. Important for passives</summary>
    private ArmorType armorType;

    /// <summary>Healthpoints of the unit e.g. 100</summary>
    private readonly int hp;

    /// <summary>Attackvalue of the unit e.g. 20</summary>
    private readonly int attack;

    /// <summary>The defense value of a unit</summary>
    private readonly int defense;

    /// <summary>How many of this unit are available</summary>
    private int unitCount;

    /// <summary>Chance to hit a critical hit on another unit e.g. 0,1 = 10%</summary>
    private readonly float critChance;

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
    /// <param name="defense">The defense value of a unit</param>
    /// <param name="type">The type of the unit. (Tank, Soldier or Plane)</param>
    /// <param name="armorType">The armor type of the unit. Important for passives</param>
    /// <param name="buildtime">The time it takes to build this unit in seconds</param>
    /// <param name="createAndOrderButton">The script that triggeres a production of this unit</param>
    /// <param name="baseSwitch">The script that handles a base switch</param>
    public Unit(string unitName, int hp, int attack, float critChance, int defense, Type type, ArmorType armorType,
        float buildtime, CreateAndOrderUnit createAndOrderButton, BaseSwitcher baseSwitch) {
        this.unitName = unitName;
        this.hp = hp;
        this.attack = attack;
        this.critChance = critChance;
        // this.critMultiplier = critMultiplier;
        this.defense = defense;
        this.type = type;
        this.armorType = armorType;
        // this.cost = cost;
        this.buildtime = buildtime;
        this.CreateAndOrderButton = createAndOrderButton;
        this.baseSwitch = baseSwitch;
        AllUnits.Add(this);
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
        get { return this.buildtime - ((this.buildtime * Unit.HPBoostLevel[Unit.BuildtimeGroupLevel[(int)this.type]]) - this.buildtime); }
        set { this.buildtime = value; }
    }

    /// <summary>Getter and setter for unitCount</summary>
    public int UnitCount {
        get { return this.unitCount; }
        set { this.unitCount = value; }
    }

    /// <summary>Getter and setter for unitName</summary>
    public string UnitName {
        get { return this.unitName; }
        set { this.unitName = value; }
    }

    /// <summary>Getter and setter for armorType</summary>
    private ArmorType ArmorTypeUnit {
        get { return this.armorType; }
        set { this.armorType = value; }
    }

    /// <summary>Getter and setter for type (tank, plane...)</summary>
    private Type UnitType {
        get { return this.type; }
        set { this.type = value; }
    }

    /// <summary>
    /// This method tries to order a specific unit. If the player has enough money the queue is started.
    /// </summary>
    /// <param name="moneyManager">The reference to the players money pool</param>
    public void Order(ref MoneyManagement moneyManager) {
        if (moneyManager.SubMoney(this.CreateAndOrderButton.Cost)) {
            this.baseSwitch.GetProductionQueue().AddToQueue(this, this.CreateAndOrderButton);
            this.CreateAndOrderButton.AddSingleUnitBuilding();
        }
    }

    /// <summary>
    /// Calculates the attack value against an enemy unit type
    /// </summary>
    /// <param name="enemyUnit">The enemy unit type which the attack value should be calculated for.</param>
    /// <returns>Returns the attack value/the damage calculated</returns>
    public int GetAttack(Unit enemyUnit) {
        int returnDamage = this.attack + Unit.OtherBoostLevel[this.Level] + Unit.OtherBoostLevel[Unit.ATKGroupLevel[(int)this.type]];
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
    private int GetAttack() {
        int returnDamage = this.attack + Unit.OtherBoostLevel[this.Level] + Unit.OtherBoostLevel[Unit.ATKGroupLevel[(int)this.type]];
        returnDamage += Passives.GetAbsolutPassive(this.type, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassive(this.type, Passives.Value.ATTACK));

        returnDamage += Passives.GetAbsolutPassiveArmortype(this.armorType, Passives.Value.ATTACK);
        returnDamage = (int)(returnDamage * Passives.GetPassiveArmortype(this.armorType, Passives.Value.ATTACK));

        return returnDamage;
    }

    private int GetHP() {
        return Mathf.RoundToInt(this.hp * Unit.HPBoostLevel[this.Level] * Unit.HPBoostLevel[Unit.HPGroupLevel[(int)this.type]]);
    }

    private int GetDef() {
        return this.defense + Unit.OtherBoostLevel[this.Level] + Unit.OtherBoostLevel[Unit.ArmorTypeLevel[(int)this.armorType]];
    }

    public float GetCritChance() {
        return this.critChance + (Unit.OtherBoostLevel[this.Level] / 100f) + (Unit.OtherBoostLevel[Unit.CritGroupLevel[(int)this.type]] / 100f);
    }

    /// <summary>
    /// Adds a single unit of this type to the unit count (visually, save game and variable). Also sets power level.
    /// </summary>
    public void AddSingleBuiltUnit() {
        this.CreateAndOrderButton.SetUnitCount((++this.unitCount).ToString());
        PlayerPrefs.SetInt(this.unitName + "_COUNT", this.unitCount);
        this.CreateAndOrderButton.AddPowerlevel(Mathf.RoundToInt((this.hp * this.attack * this.defense) / 1000f), false);
    }

    public void LevelUp() {
        int powerLevelBeforeLevelUp = Mathf.RoundToInt((this.GetHP() * this.GetAttack() * this.GetDef()) / 1000f);
        this.Level++;
        int powerLevelAfterLevelUp = Mathf.RoundToInt((this.GetHP() * this.GetAttack() * this.GetDef()) / 1000f);
        if (this.unitCount * (powerLevelAfterLevelUp - powerLevelBeforeLevelUp) > 0) {
            this.CreateAndOrderButton.AddPowerlevel(this.unitCount * (powerLevelAfterLevelUp - powerLevelBeforeLevelUp), false);
        }
    }
}
