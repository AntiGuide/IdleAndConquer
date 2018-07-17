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

    public void ShowSelectedGeneral(General gen) {
        this.GeneralName.text = gen.GeneralName;
        this.GeneralCountry.text = gen.Country;
    }

    public GameObject CreateNewUnitImage() {
        var go = Instantiate(this.UnitImagePrefab, this.UnitContainer);
        return go;
    }
}
