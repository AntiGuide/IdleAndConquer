using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowChosenGeneral : MonoBehaviour {
    /// <summary>Reference to the text to display the general name</summary>
    public Text GeneralName;

    /// <summary>Reference to the text to display the general country</summary>
    public Text GeneralCountry;

    public GameObject UnitImagePrefab;

    public Transform UnitContainer;

    private static List<string> unitsToShow = new List<string>();

    public static void SetUnitsToShow(List<string> unitsToShowVar) {
        unitsToShow = unitsToShowVar;
    }

    public void ShowSelectedGeneral(General gen) {
        this.GeneralName.text = gen.GeneralName;
        this.GeneralCountry.text = gen.Country;
    }

    public GameObject CreateNewUnitImage(Unit unit) {
        GameObject go = Instantiate(this.UnitImagePrefab, this.UnitContainer);
        DeployingUnit du = go.GetComponent<DeployingUnit>();
        du.Initialize(unit);
        return go;
    }

    void Update() {
        this.ShowSelectedGeneral(SelectedGeneral.General);
    }
}
