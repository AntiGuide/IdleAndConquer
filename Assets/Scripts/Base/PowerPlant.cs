using UnityEngine;

/// <summary>
/// Script to be attached to the power plant
/// </summary>
public class PowerPlant : MonoBehaviour {
    /// <summary>The energy production for an object</summary>
    public float EnergyProduction;

    private BaseSwitcher baseSwitch;

    /// <summary>The reference to the energy manager to add the energy</summary>
    private EnergyPool energyManagement;

    /// <summary>Adds the energy upon initialization (after build confirmed).</summary>
    public void InitializeBuilt() {
        this.energyManagement.AddEnergy(this.EnergyProduction);
    }

    /// <summary>Runs one time when object is instanciated</summary>
    private void Start() {
        baseSwitch = GameObject.FindObjectOfType<BaseSwitcher>();
        this.energyManagement = baseSwitch.GetEnergyPool();
    }
}