using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __TimeScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setTimeScale(float scale) {
        Time.timeScale = scale;
    }
}
