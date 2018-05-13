using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndOrderUnit : MonoBehaviour {

    public string unitName;
    public int hp;
    public int attack;
    public int defense;
    public int type;
    public int cost;
    public float buildtime;
    public MoneyManagement moneyManager;
    public PowerlevelManagement powerlevelManager;

    private Unit attachedUnit;
    private Text unitCountText;
    private int availableUnits = 0;

    // Use this for initialization
    void Start () {
        attachedUnit = new Unit(unitName, hp, attack, defense, type, cost, buildtime);
        unitCountText = gameObject.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (unitCountText != null) {
        //    unitCountText.text = "" + availableUnits;
        //}
	}

    public void OrderUnitOnClick() {
        attachedUnit.Order(ref availableUnits, ref moneyManager, ref powerlevelManager);
    }
}
