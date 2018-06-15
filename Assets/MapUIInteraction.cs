using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUIInteraction : MonoBehaviour {

    public __SceneSwitch SceneSwitch;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickButtonClose() {
        SceneSwitch.MainLoad();
    }
}
