using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUnits : MonoBehaviour {
    public GameObject unitButtonPrefab;
    private int unitID;
    private string unitName;

    // Use this for initialization
    void Start() {
        for (this.unitID = 0; true; this.unitID++) {
            this.unitName = PlayerPrefs.GetString("UnitName_" + this.unitID, null);
            if (this.unitName.Equals(string.Empty)) {
                break;
            }

            int count = PlayerPrefs.GetInt(this.unitName + "_COUNT", 0);
            if (count > 0) {
                // addPowerlevel(count * Mathf.RoundToInt((hp * attack * defense) / 1000), true);
                // setUnitCount(count.ToString());
                // attachedUnit.setUnitCount(count);
                OnClickDeploy ocd = Instantiate(this.unitButtonPrefab, transform).GetComponent<OnClickDeploy>();
                ocd.Initialize(new Unit(this.unitName, count));
            }
            
            // PlayerPrefs.SetInt(unitName + "_HP", hp);
            // PlayerPrefs.SetInt(unitName + "_ATTACK", attack);
            // PlayerPrefs.SetFloat(unitName + "_CHC", critChance);
            // PlayerPrefs.SetFloat(unitName + "_CHD", critMultiplier);
            // PlayerPrefs.SetInt(unitName + "_DEF", defense);
            // PlayerPrefs.SetInt(unitName + "_TYPE", (int)type);
            // PlayerPrefs.SetInt(unitName + "_ARMORTYPE", (int)armorType);
            // PlayerPrefs.SetInt(unitName + "_COST", cost);
            // PlayerPrefs.SetFloat(unitName + "_BUILDTIME", buildtime);
            // attachedUnit = new Unit(unitName, hp, attack, critChance, critMultiplier, defense, type, armorType, cost, buildtime, this, productionQueue);
        }
    }
}
