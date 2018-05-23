using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIInteraction : MonoBehaviour {

    public MainMenueController mainMenueController;
    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OpenButton1() {
        __SceneSwitch.MissionMapLoad();
    }

    public void OpenButton2() {
        mainMenueController.ToggleMenue(2);
    }

    public void OpenButton3() {
        mainMenueController.ToggleMenue(3);
    }

    public void OpenButton4() {
        mainMenueController.ToggleMenue(4);
    }

    public void OpenButton5() {
        mainMenueController.ToggleMenue(5);
    }


}
