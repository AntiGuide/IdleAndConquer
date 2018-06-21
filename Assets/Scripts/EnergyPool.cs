using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Contains energy data for each base</summary>
public class EnergyPool : MonoBehaviour {
    /// <summary>The maximum containable energy</summary>
    public float MaxEnergy = 100;

    /// <summary>The minimum containable energy</summary>
    public float MinEnergy = 0;

    /// <summary>The reference to the EnergyManager used to output the energy level</summary>
    public EnergyManagement EnergyManager;

    /// <summary>The momentarily contained energy</summary>
    public float CurEnergy = 50;

    /// <summary>
    /// Adds energy to the pool
    /// </summary>
    /// <param name="energyToAdd">How much energy to add</param>
    /// <returns>Returns wether the change is possible</returns>
    public bool AddEnergy(float energyToAdd) {
        if (this.IsInBounds(this.CurEnergy + energyToAdd)) {
            this.CurEnergy += energyToAdd;
            this.EnergyManager.OutputEnergy(this.CurEnergy, this.MaxEnergy, this.MinEnergy);
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Subs energy from the pool
    /// </summary>
    /// <param name="energyToSub">How much energy to sub</param>
    /// <returns>Returns wether the change is possible</returns>
    public bool SubEnergy(float energyToSub) {
        if (this.IsInBounds(this.CurEnergy - energyToSub)) {
            this.CurEnergy -= energyToSub;
            this.EnergyManager.OutputEnergy(this.CurEnergy, this.MaxEnergy, this.MinEnergy);
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Sets the energy to a specific value
    /// </summary>
    /// <param name="valueToSet">To which level do we set the energy</param>
    public void SetEnergy(float valueToSet) {
        if (this.IsInBounds(valueToSet)) {
            this.CurEnergy = valueToSet;
            this.EnergyManager.OutputEnergy(this.CurEnergy, this.MaxEnergy, this.MinEnergy);
        } else {
            throw new System.ArgumentException("Can not set an Energy Value that is not in the range of min and max Energy Value", "valueToSet");
        }
    }

    /// <summary>
    /// Loads and displays the curent energy of a base
    /// </summary>
    public void SetActive() {
        this.SetEnergy(this.CurEnergy);
    }

    /// <summary>
    /// Checks if a value is in the given bounds
    /// </summary>
    /// <param name="valueToCheck">The value to check the bounds for</param>
    /// <returns>Returns wether a value is in the given bounds</returns>
    private bool IsInBounds(float valueToCheck) {
        if (valueToCheck >= this.MinEnergy && valueToCheck <= this.MaxEnergy) {
            return true;
        } else {
            return false;
        }
    }
}
