using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployingUnit : MonoBehaviour {

    //public Text countText;

    private Unit unit;
    private int deployinCount = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //countText.text = deployinCount.ToString();
	}

    public void Initialize(Unit unit) {
        this.unit = unit;
    }

    public void AddDeployingCount(int addVal) {
        deployinCount += addVal;
    }
}
