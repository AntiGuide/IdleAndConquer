using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManagement : MonoBehaviour {

    public Image Bar;
    public long maxEnergy = 100;
    public long minEnergy = 0;

    private long curEnergy = 0;
    private float EnergyTimer = 0;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        EnergyTimer += Time.deltaTime;
        if (EnergyTimer > 0.2f) {
            addEnergy(1);
            EnergyTimer = EnergyTimer - 1;
        }
    }

    public void addEnergy(long energyToAdd) {
        if (isInBounds(curEnergy + energyToAdd)) {
            curEnergy = curEnergy + energyToAdd;
            outputEnergy(curEnergy);

        } else {
            throw new System.ArgumentException("Can not set an Energy Value that is not in the range of min and max Energy Value", "energyToAdd");
        }
    }

    public bool subEnergy(long energyToSub) {
        if (isInBounds(curEnergy - energyToSub)) {
            curEnergy = curEnergy - energyToSub;
            outputEnergy(curEnergy);
            return true;
        } else {
            return false;
        }
    }

    public void setEnergy(long valueToSet) {
        if (isInBounds(valueToSet)) {
            curEnergy = valueToSet;
            outputEnergy(curEnergy);
        } else {
            throw new System.ArgumentException("Can not set an Energy Value that is not in the range of min and max Energy Value", "valueToSet");
        }
    }

    private void outputEnergy(long curEnergy) {
        Bar.fillAmount = (float)curEnergy / (float)(maxEnergy - minEnergy);
    }

    private bool isInBounds(long valueToCheck) {
        if (valueToCheck >= minEnergy && valueToCheck <= maxEnergy) {
            return true;
        } else {
            return false;
        }
    }

}
