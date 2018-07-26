using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the creation of units
/// </summary>
public class CreateAndOrderUnit : MonoBehaviour {
    /// <summary>The name of the unit</summary>
    public string unitName;

    /// <summary>The maximum hitpoints of the unit</summary>
    public int hp;

    /// <summary>The attack of the unit</summary>
    public int attack;

    /// <summary>The chance to do critical damage of the unit</summary>
    public float CritChance;

    /// <summary>The factor by which the damage is multiplied on critical damage</summary>
    public float CritMultiplier;

    /// <summary>The defense of the unit</summary>
    public int Defense;

    /// <summary>The type of the unit</summary>
    public Unit.Type Type;

    /// <summary>The armor type of the unit</summary>
    public Unit.ArmorType ArmorType;

    /// <summary>The cost to build the unit</summary>
    public int cost;

    /// <summary>The buildtime of the unit</summary>
    public float buildtime;

    /// <summary>The reference to the moneypool</summary>
    public MoneyManagement MoneyManager;

    /// <summary>The reference to the powerlevel</summary>
    public PowerlevelManagement PowerlevelManager;

    /// <summary>The reference to the base switcher (used to get the correct production queue)</summary>
    public BaseSwitcher BaseSwitch;

    private static readonly List<CreateAndOrderUnit> allCreateAndOrder = new List<CreateAndOrderUnit>();

    private static readonly int[] costLevel = { 0, 0, 0 };

    /// <summary>The attached units ID</summary>
    private static int unitID;

    /// <summary>The reference to the unit name text object</summary>
    private Text unitNameText;

    /// <summary>The reference to the unit count text object</summary>
    private Text unitCountText;

    /// <summary>The reference to the unit cost text object</summary>
    private Text unitCostText;

    /// <summary>The reference to the unit count building text object</summary>
    private Text unitBuilding;

    /// <summary>The reference to the building vfx image</summary>
    private Image buildingOverlay;

    /// <summary>The units being built at the time</summary>
    private int buildingUnits = 0;

    public int Cost {
        get { return this.cost - (this.cost - Mathf.RoundToInt(Unit.HPBoostLevel[CreateAndOrderUnit.costLevel[(int)this.Type]] * this.cost)); }
    }

    public Unit AttachedUnit { get; private set; }

    public static void LevelUpCost(Unit.Type type) {
        CreateAndOrderUnit.costLevel[(int)type]++;
        foreach (var item in allCreateAndOrder) {
            item.unitCostText.text = item.Cost.ToString();
        }
    }

    /// <summary>Orders unit when a button is clicked</summary>
    public void OrderUnitOnClick() {
        this.AttachedUnit.Order(ref this.MoneyManager);
    }

    /// <summary>
    /// Sets the text unitCountText
    /// </summary>
    /// <param name="text">The text to be set</param>
    public void SetUnitCount(string text) {
        this.unitCountText.text = text;
    }

    /// <summary>
    /// Adds powerlevel.
    /// </summary>
    /// <param name="pl">The amount that should be added</param>
    /// <param name="supressed">True if the floatup should be supressed</param>
    public void AddPowerlevel(int pl, bool supressed) {
        if (pl < 0) {
            this.PowerlevelManager.SubPowerlevel(Mathf.Abs(pl));
        } else {
            this.PowerlevelManager.AddPowerlevel(pl, supressed);
        }
    }

    /// <summary>
    /// Sets the overlay to visualize production
    /// </summary>
    /// <param name="fillPercentage">The percentage to set the fillAmount to (between 0 and 1)</param>
    public void SetProductionOverlayFill(float fillPercentage) {
        this.buildingOverlay.fillAmount = fillPercentage;
    }

    /// <summary>
    /// Adds 1 to the unit building text
    /// </summary>
    public void AddSingleUnitBuilding() {
        this.buildingUnits++;
        this.ShowUnitsBuilding();
    }

    /// <summary>
    /// Subs 1 from the unit building text
    /// </summary>
    public void SubSingleUnitBuilding() {
        this.buildingUnits = --this.buildingUnits;
        this.ShowUnitsBuilding();
    }

    /// <summary>
    /// Subs 1 from the unit building text
    /// </summary>
    public void SetUnitsBuilding(int buildingUnits) {
        this.buildingUnits = buildingUnits;
        this.ShowUnitsBuilding();
    }

    private void ShowUnitsBuilding() {
        this.unitBuilding.text = this.buildingUnits == 0 ? string.Empty : this.buildingUnits.ToString();
    }

    /// <summary>
    /// Loads from PlayerPrefs. Use this for initialization
    /// </summary>
    private void Awake() {
        this.AttachedUnit = gameObject.AddComponent<Unit>().Initialize(this.unitName, this.hp, this.attack, this.CritChance, this.Defense, this.Type, this.ArmorType, this.buildtime, this, this.BaseSwitch);
        PlayerPrefs.SetString("UnitName_" + unitID, this.unitName);
        unitID++;
        PlayerPrefs.SetInt(this.unitName + "_HP", this.hp);
        PlayerPrefs.SetInt(this.unitName + "_ATTACK", this.attack);
        PlayerPrefs.SetFloat(this.unitName + "_CHC", this.CritChance);
        PlayerPrefs.SetFloat(this.unitName + "_CHD", this.CritMultiplier);
        PlayerPrefs.SetInt(this.unitName + "_DEF", this.Defense);
        PlayerPrefs.SetInt(this.unitName + "_TYPE", (int)this.Type);
        PlayerPrefs.SetInt(this.unitName + "_ARMORTYPE", (int)this.ArmorType);
        PlayerPrefs.SetInt(this.unitName + "_COST", this.cost);
        PlayerPrefs.SetFloat(this.unitName + "_BUILDTIME", this.buildtime);
        this.unitNameText = transform.Find("Text").GetComponent<Text>();
        this.unitNameText.text = this.unitName;
        this.unitCountText = transform.Find("CountText").GetComponent<Text>();
        this.unitCostText = transform.Find("CostText").GetComponent<Text>();
        this.unitCostText.text = this.cost.ToString();
        this.buildingOverlay = transform.Find("BuildingOverlay").GetComponent<Image>();
        this.buildingOverlay.fillAmount = 0f;
        this.unitBuilding = transform.Find("BuildingCountText").GetComponent<Text>();
        var count = PlayerPrefs.GetInt(this.unitName + "_COUNT", 0);
        if (count > 0) {
            this.AddPowerlevel(count * Mathf.RoundToInt(this.hp * this.attack * this.Defense / 1000f), true);
            this.SetUnitCount(count.ToString());
            this.AttachedUnit.UnitCount = count;
        }

        allCreateAndOrder.Add(this);
    }
}
