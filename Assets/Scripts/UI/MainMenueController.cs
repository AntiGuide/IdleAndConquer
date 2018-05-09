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
    private MenueController enabledMenueController;

    private MenueController menueController1;
    private MenueController menueController2;
    private MenueController menueController3;
    private MenueController menueController4;
    private MenueController menueController5;


    // Use this for initialization
    void Start () {
        //menueController1 = menue1.GetComponent<MenueController>();
        menueController2 = menue2.GetComponent<MenueController>();
        menueController3 = menue3.GetComponent<MenueController>();
        menueController4 = menue4.GetComponent<MenueController>();
        menueController5 = menue5.GetComponent<MenueController>();

        //menue1.SetActive(false);
        menue2.SetActive(true);
        lastEnabledMenue = menue2;
        menue3.SetActive(false);
        menue4.SetActive(false);
        menue5.SetActive(false);

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

        if (enabledScreen != screenType) {
            lastEnabledMenue.SetActive(false);
            switch (screenType) {
                case MenueCategory.MENUE_ONE:
                    menue1.SetActive(true);
                    lastEnabledMenue = menue1;
                    enabledMenueController = menueController1;
                    break;
                case MenueCategory.MENUE_TWO:
                    menue2.SetActive(true);
                    lastEnabledMenue = menue2;
                    enabledMenueController = menueController2;
                    break;
                case MenueCategory.MENUE_THREE:
                    menue3.SetActive(true);
                    lastEnabledMenue = menue3;
                    enabledMenueController = menueController3;
                    break;
                case MenueCategory.MENUE_FOUR:
                    menue4.SetActive(true);
                    lastEnabledMenue = menue4;
                    enabledMenueController = menueController4;
                    break;
                case MenueCategory.MENUE_FIVE:
                    menue5.SetActive(true);
                    lastEnabledMenue = menue5;
                    enabledMenueController = menueController5;
                    break;
            }
            enabledMenueController.Expand(false);
            enabledScreen = screenType;
        } else {
            enabledScreen = MenueCategory.NONE;
            enabledMenueController.Unexpand(false);
        }


        

    }
}
