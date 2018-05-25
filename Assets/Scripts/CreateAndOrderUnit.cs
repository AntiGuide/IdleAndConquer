using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndOrderUnit : MonoBehaviour {

    public string unitName;
    public int hp;
    public int attack;
    public float critChance;
    public float critMultiplier;
    public int defense;
    public Unit.Type type;
    public Unit.ArmorType armorType;
    public int cost;
    public float buildtime;
    public MoneyManagement moneyManager;
    public PowerlevelManagement powerlevelManager;
    public ProductionQueue productionQueue;

    private Unit attachedUnit;
    private Text unitNameText;
    private Text unitCountText;
    private Text unitBuilding;
    private Image buildingOverlay;
    private int availableUnits = 0;
    private int buildingUnits = 0;
    private static int unitID;

    // Use this for initialization
    void Start () {
        attachedUnit = new Unit(unitName, hp, attack, critChance, critMultiplier, defense, type, armorType, cost, buildtime, this, productionQueue);
        PlayerPrefs.SetString("UnitName_" + unitID, unitName);
        unitID++;
        PlayerPrefs.SetInt(unitName + "_HP", hp);
        PlayerPrefs.SetInt(unitName + "_ATTACK", attack);
        PlayerPrefs.SetFloat(unitName + "_CHC", critChance);
        PlayerPrefs.SetFloat(unitName + "_CHD", critMultiplier);
        PlayerPrefs.SetInt(unitName + "_DEF", defense);
        PlayerPrefs.SetInt(unitName + "_TYPE", (int)type);
        PlayerPrefs.SetInt(unitName + "_ARMORTYPE", (int)armorType);
        PlayerPrefs.SetInt(unitName + "_COST", cost);
        PlayerPrefs.SetFloat(unitName + "_BUILDTIME", buildtime);


        unitNameText = transform.Find("Text").GetComponent<Text>();
        unitNameText.text = unitName;
        unitCountText = transform.Find("CountText").GetComponent<Text>();
        buildingOverlay = transform.Find("BuildingOverlay").GetComponent<Image>();
        buildingOverlay.fillAmount = 0f;
        unitBuilding = transform.Find("BuildingCountText").GetComponent<Text>();
        int count = PlayerPrefs.GetInt(unitName + "_COUNT", 0);
        if (count > 0) {
            addPowerlevel(count * Mathf.RoundToInt((hp * attack * defense) / 1000), true);
            setUnitCount(count.ToString());
            attachedUnit.setUnitCount(count);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OrderUnitOnClick() {
        attachedUnit.Order(ref availableUnits, ref moneyManager, ref powerlevelManager);
    }

    public void setUnitCount(string text) {
        unitCountText.text = text;
    }

    public void addPowerlevel(int pl, bool supressed) {
        powerlevelManager.addPowerlevel(pl, supressed);
    }

    public void setProductionOverlayFill(float fillPercentage) {
        buildingOverlay.fillAmount = fillPercentage;
    }

    public void addSingleUnitBuilding() {
        unitBuilding.text = (++buildingUnits).ToString();
    }

    public void subSingleUnitBuilding() {
        unitBuilding.text = (--buildingUnits).ToString();
    }
}
