using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueController : MonoBehaviour {

    public enum MenueCategory
    {
        NONE = 0,
        MENUE_ONE,
        MENUE_TWO,
        MENUE_THREE,
        MENUE_FOUR,
        MENUE_FIVE
    };
    MenueCategory enabledScreen;

    public GameObject menue1;
    public GameObject menue2;
    public GameObject menue3;
    public GameObject menue4;
    public GameObject menue5;

    private GameObject lastEnabledMenue;
    private MenueController menueController1;

    // Use this for initialization
    void Start () {
        menueController1 = menue1.GetComponent<MenueController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * <summary>This method opens the called type of menue instantly an without an animation.</summary>
     * <param name="screenType">Der Typ des zu öffnenden Menüs</param>
     * <returns>Nichts</returns>
     * <remarks>This method opens the called type of menue instantly an without an animation.</remarks>
     * <value>The method sets the enabledScreen data member.</value>
     * */
    public void ToggleMenue(MenueCategory screenType) {
        switch (screenType) {
            case MenueCategory.MENUE_ONE:
                if (enabledScreen != MenueCategory.MENUE_ONE) {
                    if (enabledScreen != MenueCategory.NONE) {
                        lastEnabledMenue.SetActive(false);
                    }
                    menue1.SetActive(true);
                    lastEnabledMenue = menue1;
                    menueController1.Expand(false);
                    enabledScreen = MenueCategory.MENUE_ONE;
                } else {
                    enabledScreen = MenueCategory.NONE;
                    menueController1.Unexpand(false);
                }
                break;
            default:
                break;
        }
        
    }

    public void closeMenue() {
        ToggleMenue(enabledScreen);
    }
}
