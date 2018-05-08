using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInteraction : MonoBehaviour {

    private MenueController menueController;

    // Use this for initialization
    void Start () {

        menueController = GameObject.Find("BackgroundBuyMenue").GetComponent<MenueController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenWorldMap() {
        menueController.OpenMenue(MenueController.MenueCategory.MENUE_ONE);
        Debug.Log("OpenWorldMap");
    }

    public void OpenResearch() {
        Debug.Log("OpenResearch");
    }

    public void OpenProductionBoost() {
        Debug.Log("OpenProductionBoost");
    }

    public void OpenBlackMarket() {
        Debug.Log("OpenBlackMarket");
    }

    public void OpenBuildings(GameObject BuildMenue) {
        Debug.Log("OpenBuildings");
    }

    public void OpenOptions() {
        Debug.Log("OpenOptions");
    }


}
