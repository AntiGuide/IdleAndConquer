using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickDeploy : MonoBehaviour {
    public static int DeployedUnits = 0;
    public Text RemainingUnitsText;
    public Text UnitNameText;
    private const int MaxSlots = 5;
    private ShowChosenGeneral showChosenGeneral;
    private Unit attachedUnit;
    private int unitCount;
    private int startUnitCount;
    private MissionManager missionMan;
    private List<DeployedUnit> deplUnits = new List<DeployedUnit>();

    public void OnClickDeployEvent() {
        if (this.unitCount <= 0 || DeployedUnits >= MaxSlots) return;
        this.unitCount--;
        DeployedUnits++;
        this.RemainingUnitsText.text = this.unitCount.ToString();
        deplUnits.Add(this.showChosenGeneral.CreateNewUnitImage(attachedUnit, this));
        this.missionMan.AddUnitToBuildingMission(ref this.attachedUnit);
    }

    public void GiveUnitBack() {
        this.unitCount++;
        DeployedUnits--;
        this.RemainingUnitsText.text = this.unitCount.ToString();
        this.missionMan.RemoveUnitFromBuildingMission(ref this.attachedUnit);
    }

    public void Initialize(Unit unit) {
        this.attachedUnit = unit;
        this.RemainingUnitsText.text = this.attachedUnit.UnitCount.ToString();
        this.unitCount = this.attachedUnit.UnitCount - this.attachedUnit.SentToMission;
        this.startUnitCount = this.unitCount;
        this.UnitNameText.text = this.attachedUnit.UnitName;
        ScreenStateMachine.OCDs.Add(this);
    }

    public void ResetOCD() {
        this.unitCount = this.startUnitCount;
        this.RemainingUnitsText.text = this.unitCount.ToString();
        foreach (var deplUnit in deplUnits) {
            Destroy(deplUnit);
        }

        deplUnits = new List<DeployedUnit>();
    }

    /// <summary>Use this for initialization</summary>
    private void Start() {
        this.showChosenGeneral = GameObject.Find("/MissionMap/Canvas/DeployUI/BG").GetComponent<ShowChosenGeneral>();
        this.missionMan = GameObject.Find("/ReferenceShare").GetComponent<MissionManager>();
    }
}
