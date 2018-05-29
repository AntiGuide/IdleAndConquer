﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for handling button inputs for creation of buildings.
/// </summary>
public class BuildButtonManager : MonoBehaviour {
    /// <summary>Attached BuildBuilding object (Only one per scene existent)</summary>
    public BuildBuilding Builder;

    /// <summary>UI text element to display the cost of a building</summary>
    public Text Cost;

    /// <summary>Attached building prefab</summary>
    public GameObject AttachedBuilding;

    /// <summary>Reference to MoneyManagement object (Only one per scene existent)</summary>
    public MoneyManagement MoneyManager;

    /// <summary>Variable to store the cost of the attached building after the Start method</summary>
    private long costBuilding;

    /// <summary>
    /// Triggered by a button click. Builds the wanted building if the money in the attached MoneyManager is enough to cover the costs.
    /// </summary>
    /// <param name="i">The index of the building that should be built (Indexes defined in BuildBuilding class)</param>
    public void ClickBuildBuilding(int i) {
        if (this.MoneyManager.hasMoney(this.costBuilding)) {
            this.Builder.BuildABuilding(i, this.costBuilding);
        }
    }

    /// <summary>Use this for initialization</summary>
    void Start() {
        this.costBuilding = this.AttachedBuilding.GetComponentInChildren<BuildingManager>().BuildCost;
        this.Cost.text = MoneyManagement.formatMoney(this.costBuilding);
    }
}