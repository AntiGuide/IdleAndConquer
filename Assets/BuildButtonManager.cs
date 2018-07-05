using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for handling button inputs for creation of buildings.
/// </summary>
public class BuildButtonManager : MonoBehaviour {
    /// <summary>Attached BuildBuilding object (One per base)</summary>
    public BuildBuilding Builder;

    /// <summary>UI text element to display the cost of a building</summary>
    public Text Cost;

    /// <summary>Attached building prefab</summary>
    public GameObject AttachedBuilding;

    /// <summary>Reference to MoneyManagement object (Only one per scene existent)</summary>
    public MoneyManagement MoneyManager;

    /// <summary>Reference to BaseSwitcher to get the correct BuildBuilding object</summary>
    public BaseSwitcher BaseSwitch;

    /// <summary>Variable to store the cost of the attached building after the Start method</summary>
    private long costBuilding;

    /// <summary>Variable to store the energy cost of the attached building after the Start method</summary>
    private int costEnergy;

    /// <summary>
    /// Triggered by a button click. Builds the wanted building if the money in the attached MoneyManager is enough to cover the costs.
    /// </summary>
    /// <param name="i">The index of the building that should be built (Indexes defined in BuildBuilding class)</param>
    public void ClickBuildBuilding(int i) {
        if (this.MoneyManager.HasMoney(this.costBuilding)) {
            this.BaseSwitch.GetBuilder().BuildABuilding(i, this.costBuilding, this.costEnergy);
        }
    }

    /// <summary>Use this for initialization</summary>
    void Start() {
        BuildingManager buildingManager = this.AttachedBuilding.GetComponentInChildren<BuildingManager>();
        this.costBuilding = buildingManager.BuildCost;
        this.costEnergy = buildingManager.CostEnergy;
        this.Cost.text = MoneyManagement.FormatMoney(this.costBuilding);
    }
}
