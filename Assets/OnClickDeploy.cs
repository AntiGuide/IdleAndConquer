using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickDeploy : MonoBehaviour {
    public static int MaxSlots = 5;
    public Text RemainingUnitsText;
    public Text UnitNameText;
    private ShowChosenGeneral showChosenGeneral;
    private Unit attachedUnit;
    private int unitCount;
    private static int deployedUnits = 0;

    public void OnClickDeployEvent() {
        if (this.unitCount > 0 && deployedUnits < MaxSlots) {
            this.unitCount--;
            deployedUnits++;
            this.RemainingUnitsText.text = this.unitCount.ToString();
            this.showChosenGeneral.CreateNewUnitImage();
            MissionManager.AddUnitToBuildingMission(attachedUnit);
        }
    }

    public void Initialize(Unit unit) {
        this.attachedUnit = unit;
        this.RemainingUnitsText.text = this.attachedUnit.UnitCount.ToString();
        this.unitCount = this.attachedUnit.UnitCount;
        this.UnitNameText.text = this.attachedUnit.UnitName;
    }

    /// <summary>Use this for initialization</summary>
    void Start() {
        this.showChosenGeneral = GameObject.Find("/MissionMap/Canvas/DeployUI/BG").GetComponent<ShowChosenGeneral>();
    }
}
