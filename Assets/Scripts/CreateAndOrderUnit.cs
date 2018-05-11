using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndOrderUnit : MonoBehaviour {

    public string unitName;
    public int cost;
    public int damage;
    public int defense;
    public float buildtime;
    public GameObject textDollar;

    private Unit attachedUnit;
    private Text unitCountText;
    private int availableUnits = 0;
    private MoneyManagement moneyManager;

    // Use this for initialization
    void Start () {
        attachedUnit = new Unit(unitName, cost, damage, defense, buildtime);
        unitCountText = gameObject.GetComponentInChildren<Text>();
        moneyManager = textDollar.GetComponent<MoneyManagement>();
    }
	
	// Update is called once per frame
	void Update () {
        if (unitCountText != null) {
            unitCountText.text = "" + availableUnits;
        }
	}

    public void OrderUnitOnClick() {
        attachedUnit.Order(ref availableUnits, ref moneyManager);
    }
}
