using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteraction : MonoBehaviour {

    private MenueControll menueController;

    // Use this for initialization
    void Start () {

        menueController = GameObject.Find("BackgroundBuyMenue").GetComponent<MenueControll>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenWorldMap() {
        menueController.OpenMenue(MenueControll.Screen.MAP);
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

    public void OpenOptions() {
        Debug.Log("OpenOptions");
    }


}
