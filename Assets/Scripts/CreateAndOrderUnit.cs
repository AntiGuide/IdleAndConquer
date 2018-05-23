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

    // Use this for initialization
    void Start () {
        attachedUnit = new Unit(unitName, hp, attack, critChance, critMultiplier, defense, type, armorType, cost, buildtime, this, productionQueue);
        unitNameText = transform.Find("Text").GetComponent<Text>();
        unitNameText.text = unitName;
        unitCountText = transform.Find("CountText").GetComponent<Text>();
        buildingOverlay = transform.Find("BuildingOverlay").GetComponent<Image>();
        buildingOverlay.fillAmount = 0f;
        unitBuilding = transform.Find("BuildingCountText").GetComponent<Text>();
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

    public void addPowerlevel(int pl) {
        powerlevelManager.addPowerlevel(pl);
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
