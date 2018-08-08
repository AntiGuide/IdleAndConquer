using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the units selected in the mission preparation screen
/// </summary>
public class ShowChosenGeneral : MonoBehaviour {
    /// <summary>Reference to the text to display the general name</summary>
    public Text GeneralName;

    /// <summary>Reference to the text to display the general country</summary>
    public Text GeneralCountry;

    public GameObject UnitImagePrefab;

    public Transform UnitContainer;

    public List<DeployedUnit> unitStacks = new List<DeployedUnit>();

    public void ShowSelectedGeneral(General gen) {
        this.GeneralName.text = gen.GeneralName;
        this.GeneralCountry.text = gen.Country;
    }

    public DeployedUnit CreateNewUnitImage(Unit unit,  OnClickDeploy ocd) {
        foreach (var stack in unitStacks) {
            if (stack.AttachedUnit == unit) {
                stack.IncreaseCount();
                return stack;
            }
        }

        var go = Instantiate(this.UnitImagePrefab, this.UnitContainer).GetComponent<DeployedUnit>();
        go.Initialize(unit, ocd,  this);
        unitStacks.Add(go);
        return go;
    }

    public void ResetUnits() {
        var children = this.UnitContainer.GetComponentsInChildren<Image>();
        foreach (var child in children) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
