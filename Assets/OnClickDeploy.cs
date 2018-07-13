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
    public static int DeployedUnits = 0;
    private MissionManager MissionMan;

    public void OnClickDeployEvent() {
        if (this.unitCount > 0 && DeployedUnits < MaxSlots) {
            this.unitCount--;
            DeployedUnits++;
            this.RemainingUnitsText.text = this.unitCount.ToString();
            this.showChosenGeneral.CreateNewUnitImage();
            this.MissionMan.AddUnitToBuildingMission(ref this.attachedUnit);
        }
    }

    public void Initialize(Unit unit) {
        this.attachedUnit = unit;
        this.RemainingUnitsText.text = this.attachedUnit.UnitCount.ToString();
        this.unitCount = this.attachedUnit.UnitCount - this.attachedUnit.SentToMission;
        this.UnitNameText.text = this.attachedUnit.UnitName;
    }

    /// <summary>Use this for initialization</summary>
    void Start() {
        this.showChosenGeneral = GameObject.Find("/MissionMap/Canvas/DeployUI/BG").GetComponent<ShowChosenGeneral>();
        this.MissionMan = GameObject.Find("/ReferenceShare").GetComponent<MissionManager>();
    }
}
