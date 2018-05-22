using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUIInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickButtonClose() {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
    }
}
