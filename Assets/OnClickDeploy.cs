using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickDeploy : MonoBehaviour {

    private Text remainingUnitsText;
    private Text unitNameText;
    private int remainingUnits;

	// Use this for initialization
	void Start () {
        unitNameText = transform.Find("NameText").GetComponent<Text>();
        remainingUnitsText = transform.Find("RemainigUnitsText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnClickDeployEvent() {
        remainingUnits--;
        remainingUnitsText.text = remainingUnits.ToString();
    }

    public void Initialize(string name, int count) {
        remainingUnits = count;
        remainingUnitsText.text = name;
        unitNameText.text = name;
    }
}
