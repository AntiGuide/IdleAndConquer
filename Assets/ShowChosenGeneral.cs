using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowChosenGeneral : MonoBehaviour {

    public Text generalName;
    public Text generalCountry;


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
}
