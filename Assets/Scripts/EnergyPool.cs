using UnityEngine;

/// <summary>Contains energy data for each base</summary>
public class EnergyPool : MonoBehaviour {
    /// <summary>The maximum containable energy</summary>
    public int MaxEnergy = 96;

    /// <summary>The minimum containable energy</summary>
    public int MinEnergy = 0;

    /// <summary>The reference to the EnergyManager used to output the energy level</summary>
    public EnergyManagement EnergyManager;

    /// <summary>The momentarily contained energy</summary>
    public int CurEnergy = 50;

    /// <summary>
    /// Adds energy to the pool
    /// </summary>
    /// <param name="energyToAdd">How much energy to add</param>
    /// <returns>Returns wether the change is possible</returns>
    public void AddEnergy(int energyToAdd) {
        if (!this.IsInBounds(this.CurEnergy + energyToAdd)) return;
        this.CurEnergy += energyToAdd;
        this.EnergyManager.OutputEnergy(this.CurEnergy, this.MaxEnergy, this.MinEnergy, true);
    }

    /// <summary>
    /// Subs energy from the pool
    /// </summary>
    /// <param name="energyToSub">How much energy to sub</param>
    /// <returns>Returns wether the change is possible</returns>
    public void SubEnergy(int energyToSub) {
        if (!this.IsInBounds(this.CurEnergy - energyToSub)) return;
        this.CurEnergy -= energyToSub;
        this.EnergyManager.OutputEnergy(this.CurEnergy, this.MaxEnergy, this.MinEnergy, true);
    }

    /// <summary>
    /// Loads and displays the curent energy of a base
    /// </summary>
    public void SetActive() {
        this.SetEnergy(this.CurEnergy);
    }

    /// <summary>
    /// Sets the energy to a specific value
    /// </summary>
    /// <param name="valueToSet">To which level do we set the energy</param>
    private void SetEnergy(int valueToSet) {
        if (this.IsInBounds(valueToSet)) {
            this.CurEnergy = valueToSet;
            this.EnergyManager.OutputEnergy(this.CurEnergy, this.MaxEnergy, this.MinEnergy, false);
        } else {
            throw new System.ArgumentException("Can not set an Energy Value that is not in the range of min and max Energy Value", "valueToSet");
        }
    }

    /// <summary>
    /// Checks if a value is in the given bounds
    /// </summary>
    /// <param name="valueToCheck">The value to check the bounds for</param>
    /// <returns>Returns wether a value is in the given bounds</returns>
    private bool IsInBounds(float valueToCheck) {
        return valueToCheck >= this.MinEnergy && valueToCheck <= this.MaxEnergy;
    }
}
