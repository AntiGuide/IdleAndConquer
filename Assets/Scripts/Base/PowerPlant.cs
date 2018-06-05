using UnityEngine;

/// <summary>
/// Script to be attached to the power plant
/// </summary>
public class PowerPlant : MonoBehaviour {
    /// <summary>The energy production for an object</summary>
    public float EnergyProduction;

    /// <summary>The reference to the energy manager to add the energy</summary>
    private EnergyManagement energyManagement;

    /// <summary>Adds the energy upon initialization (after build confirmed).</summary>
    public void InitializeBuilt() {
        this.energyManagement.addEnergy(this.EnergyProduction);
    }

    /// <summary>Runs one time when object is instanciated</summary>
    private void Start() {
        this.energyManagement = GameObject.Find("/Canvas/BackgroundSideStrip").GetComponent<EnergyManagement>();
    }
}