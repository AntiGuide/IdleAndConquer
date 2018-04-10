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
    private float canvasHeight;

    // Use this for initialization
    void Start () {
        enabledScreen = Screen.NONE;
        canvasHeight = GameObject.Find("/Canvas").GetComponent<RectTransform>().rect.height;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * <summary>This method opens the called type of menue instantly an without an animation.</summary>
     * <param name="screenType">Der Typ des zu öffnenden Menüs</param>
     * <returns>Nichts</returns>
     * <remarks>This method opens the called type of menue instantly an without an animation. The types to be called are: MAP, RESEARCH, PRODUCTION_BOOST, BLACK_MARKET and OPTIONS</remarks>
     * <value>The method sets the _enabledScreen data member.</value>
     * */
    public void OpenMenue(Screen screenType) {
        switch (screenType) {
            case Screen.MAP:
                if (enabledScreen == Screen.MAP) {
                    float y = transform.position.y;
                    if (y > canvasHeight * 0.8f) {
                        y = canvasHeight * 0.8f;
                    }
                    transform.position = new Vector3(transform.position.x, y, 0);
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
