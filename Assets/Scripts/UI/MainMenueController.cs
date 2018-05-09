using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueController : MonoBehaviour {

    public GameObject[] menue = new GameObject[5];

    private int enabledMenue;
    private bool isExpanded;
    private MenueController[] menueController = new MenueController[5];

    public bool IsExpanded {
        get {
            return isExpanded;
        }

        set {
            isExpanded = value;
        }
    }

    // Use this for initialization
    void Start () {
        int i = 0;
        bool standardSelected = false;

        foreach (GameObject men in menue) {
            if (men != null && !standardSelected) {
                men.SetActive(true);
                enabledMenue = i;
                isExpanded = false;
                standardSelected = true;
            } else if (men != null) {
                men.SetActive(false);
            }
            i++;
        }

        i = 0;
        foreach (GameObject men in menue) {
            if (men != null) {
                menueController[i] = men.GetComponent<MenueController>();
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    /**
     * <summary>This method opens the called type of menue instantly an without an animation.</summary>
     * <param name="menueNumber">Number of menue to open</param>
     * <returns>Nichts</returns>
     * <remarks>This method opens the called type of menue instantly an without an animation.</remarks>
     * <value>The method sets the enabledMenue data member.</value>
     * */
    public void ToggleMenue(int menueNumber) {
        menueNumber--;

        if (enabledMenue != menueNumber) {
            if (enabledMenue != -1) {
                menue[enabledMenue].SetActive(false);
            }
            menue[menueNumber].SetActive(true);
            enabledMenue = menueNumber;
            menueController[enabledMenue].Expand(true);//false wenn nicht animiert
            isExpanded = true;
        } else {
            if (isExpanded) {
                menueController[enabledMenue].Unexpand(true);//false wenn nicht animiert
                isExpanded = false;
                enabledMenue = -1;
            } else {
                menueController[enabledMenue].Expand(true);//false wenn nicht animiert
                isExpanded = true;
            }
        }
    }
}
