using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPool : MonoBehaviour {
    public float maxEnergy = 100;
    public float minEnergy = 0;
    public EnergyManagement EnergyManager;
    public float CurEnergy = 50;
    public BaseSwitcher BaseSwitch;

    public void AddEnergy(float energyToAdd) {
        if (this.IsInBounds(this.CurEnergy + energyToAdd)) {
            this.CurEnergy += energyToAdd;
            EnergyManager.OutputEnergy(this.CurEnergy, this.maxEnergy, this.minEnergy);
        }
    }

    // TODO Negative Energy + Blinking Energy
    public bool SubEnergy(float energyToSub) {
        if (this.IsInBounds(this.CurEnergy - energyToSub)) {
            this.CurEnergy -= energyToSub;
            EnergyManager.OutputEnergy(this.CurEnergy, this.maxEnergy, this.minEnergy);
            return true;
        } else {
            return false;
        }
    }

    public void SetEnergy(float valueToSet) {
        if (this.IsInBounds(valueToSet)) {
            this.CurEnergy = valueToSet;
            EnergyManager.OutputEnergy(this.CurEnergy, this.maxEnergy, this.minEnergy);
        } else {
            throw new System.ArgumentException("Can not set an Energy Value that is not in the range of min and max Energy Value", "valueToSet");
        }
    }

    public void SetActive() {
        SetEnergy(CurEnergy);
    }

    private bool IsInBounds(float valueToCheck) {
        if (valueToCheck >= this.minEnergy && valueToCheck <= this.maxEnergy) {
            return true;
        } else {
            return false;
        }
    }
}
