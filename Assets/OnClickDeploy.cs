using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickDeploy : MonoBehaviour {

    public Text remainingUnitsText;
    public Text unitNameText;
    private ShowChosenGeneral showChosenGeneral;

    private Unit attachedUnit;
    private int unitCount;

    // Use this for initialization
    void Start () {
        showChosenGeneral = GameObject.Find("/MissionMap/Canvas/DeployUI/BG").GetComponent<ShowChosenGeneral>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnClickDeployEvent() {
        if (unitCount > 0) {
            unitCount--;
            remainingUnitsText.text = unitCount.ToString();
            showChosenGeneral.createNewUnitImage(attachedUnit);

        }
    }

    public void Initialize(Unit unit) {
        attachedUnit = unit;
        remainingUnitsText.text = attachedUnit.UnitCount.ToString();
        unitCount = attachedUnit.UnitCount;
        unitNameText.text = attachedUnit.UnitName;
    }
}
