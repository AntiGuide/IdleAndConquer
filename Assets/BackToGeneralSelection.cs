using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGeneralSelection : MonoBehaviour {

    MainMenueController mainMenueController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick() {
        mainMenueController.ToggleMenue(1);
        SelectedGeneral.General = null;
    }
}
