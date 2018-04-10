using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenueControll : MonoBehaviour {

    public enum Screen {
        NONE = 0,
        MAP,
        RESEARCH,
        PRODUCTION_BOOST,
        BLACK_MARKET,
        OPTIONS
    };

    Screen enabledScreen;

    // Use this for initialization
    void Start () {
        enabledScreen = Screen.NONE;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenMenue(Screen screenType) {
        switch (screenType) {
            case Screen.MAP:
                if (enabledScreen == Screen.MAP) {
                    transform.localScale = new Vector3(1, 1, 1);
                    enabledScreen = Screen.NONE;
                } else {
                    transform.localScale = new Vector3(1, 7, 1);
                    enabledScreen = Screen.MAP;
                }
                break;
            default:
                break;
        }
        
        //transform.localPosition = Vector3.zero;
    }
}
