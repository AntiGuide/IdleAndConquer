using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUnits : MonoBehaviour {
    private int unitID;
    private string unitName;
    public GameObject unitButtonPrefab;
    // Use this for initialization
    void Start () {
        for (unitID = 0;true; unitID++) {
            unitName = PlayerPrefs.GetString("UnitName_" + unitID,null);
            if (unitName == null) {
                break;
            }

            int count = PlayerPrefs.GetInt(unitName + "_COUNT", 0);
            if (count > 0) {
                //addPowerlevel(count * Mathf.RoundToInt((hp * attack * defense) / 1000), true);
                //setUnitCount(count.ToString());
                //attachedUnit.setUnitCount(count);
                OnClickDeploy ocd = Instantiate(unitButtonPrefab, transform).GetComponent<OnClickDeploy>();
                ocd.Initialize(unitName, count);
            }
            
            //PlayerPrefs.SetInt(unitName + "_HP", hp);
            //PlayerPrefs.SetInt(unitName + "_ATTACK", attack);
            //PlayerPrefs.SetFloat(unitName + "_CHC", critChance);
            //PlayerPrefs.SetFloat(unitName + "_CHD", critMultiplier);
            //PlayerPrefs.SetInt(unitName + "_DEF", defense);
            //PlayerPrefs.SetInt(unitName + "_TYPE", (int)type);
            //PlayerPrefs.SetInt(unitName + "_ARMORTYPE", (int)armorType);
            //PlayerPrefs.SetInt(unitName + "_COST", cost);
            //PlayerPrefs.SetFloat(unitName + "_BUILDTIME", buildtime);
            //attachedUnit = new Unit(unitName, hp, attack, critChance, critMultiplier, defense, type, armorType, cost, buildtime, this, productionQueue);

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
