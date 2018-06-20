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

    public void AddEnergy(float energyToAdd) {
        if (this.IsInBounds(this.curEnergy + energyToAdd)) {
            this.curEnergy += energyToAdd;
            this.OutputEnergy(this.curEnergy);
        }
    }

    // TODO Negative Energy + Blinking Energy
    public bool SubEnergy(float energyToSub) {
        if (this.IsInBounds(this.curEnergy - energyToSub)) {
            this.curEnergy -= energyToSub;
            this.curEnergy = this.curEnergy <= this.redStep - 1 ? this.redStep - 1 : this.curEnergy;
            this.OutputEnergy(this.curEnergy);
            return true;
        } else {
            return false;
        }
    }

    public void SetEnergy(float valueToSet) {
        if (this.IsInBounds(valueToSet)) {
            this.curEnergy = valueToSet;
            this.OutputEnergy(this.curEnergy);
        } else {
            throw new System.ArgumentException("Can not set an Energy Value that is not in the range of min and max Energy Value", "valueToSet");
        }
    }

    // Use this for initialization
    void Start() {
        this.SetEnergy(50);
        this.prevColor = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update() {
        gameObject.GetComponent<Image>().color = this.curEnergy < this.redStep ? Color.red : this.prevColor;
    }

    private void OutputEnergy(float curEnergy) {
        this.Bar.fillAmount = this.curEnergy / (this.maxEnergy - this.minEnergy);
    }

    private bool IsInBounds(float valueToCheck) {
        if (valueToCheck >= this.minEnergy && valueToCheck <= this.maxEnergy) {
            return true;
        } else {
            return false;
        }
    }
}
