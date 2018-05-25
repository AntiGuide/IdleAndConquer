using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowChosenGeneral : MonoBehaviour {

    public Text generalName;
    public Text generalCountry;
    public GameObject unitImagePrefab;
    public Transform unitContainer;

    private static List<string> unitsToShow = new List<string>();


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        showSelectedGeneral(SelectedGeneral.General);
    }

    public void showSelectedGeneral(General gen) {
        generalName.text = gen.GeneralName;
        generalCountry.text = gen.Country;

    }

    public GameObject createNewUnitImage(Unit unit) {
        GameObject go = Instantiate(unitImagePrefab, unitContainer);
        DeployingUnit du = go.GetComponent<DeployingUnit>();
        du.Initialize(unit);
        return go;
    }

    public static void setUnitsToShow(List<string> unitsToShowVar) {
        unitsToShow = unitsToShowVar;
    }
}
