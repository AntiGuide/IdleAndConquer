using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManagement : MonoBehaviour {

    public Image Bar;
    public float maxEnergy = 100;
    public float minEnergy = 0;
    public float redStep = 24f;

    private float curEnergy = 0;
    private Color prevColor;

    // Use this for initialization
    void Start() {
        setEnergy(50);
        prevColor = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update() {
        gameObject.GetComponent<Image>().color = curEnergy < redStep ? Color.red : prevColor;
    }

    public void addEnergy(float energyToAdd) {
        if (isInBounds(curEnergy + energyToAdd)) {
            curEnergy = curEnergy + energyToAdd;
            outputEnergy(curEnergy);

        } else {
            //throw new System.ArgumentException("Can not set an Energy Value that is not in the range of min and max Energy Value", "energyToAdd");
        }
    }
    //TODO Negative Energy + Blinking Energy
    public bool subEnergy(float energyToSub) {
        if (isInBounds(curEnergy - energyToSub)) {
            curEnergy = curEnergy - energyToSub;
            curEnergy = curEnergy <= redStep - 1 ? redStep - 1 : curEnergy;
            outputEnergy(curEnergy);
            return true;
        } else {
            return false;
        }
    }

    public void setEnergy(float valueToSet) {
        if (isInBounds(valueToSet)) {
            curEnergy = valueToSet;
            outputEnergy(curEnergy);
        } else {
            throw new System.ArgumentException("Can not set an Energy Value that is not in the range of min and max Energy Value", "valueToSet");
        }
    }

    private void outputEnergy(float curEnergy) {
        Bar.fillAmount = curEnergy / (maxEnergy - minEnergy);
    }

    private bool isInBounds(float valueToCheck) {
        if (valueToCheck >= minEnergy && valueToCheck <= maxEnergy) {
            return true;
        } else {
            return false;
        }
    }

}
