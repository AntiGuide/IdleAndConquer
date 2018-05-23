using UnityEngine;

public class PowerPlant : MonoBehaviour
{
    private EnergyManagement energyManagement;
    public float energyProduction;

    private void Start() {
        energyManagement = GameObject.Find("/Canvas/BackgroundSideStrip").GetComponent<EnergyManagement>();
    }

    public void InitializeBuilt() {
        energyManagement.addEnergy(energyProduction);
    }
}