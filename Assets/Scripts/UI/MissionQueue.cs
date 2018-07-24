using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionQueue : MonoBehaviour {
    public GameObject MissionBar;
    public MoneyManagement MoneyManager;
    public RenownManagement RenownManager;
    public VirtualCurrencyManagement VirtualCurrencyManager;
    public GameObject RewardPopUpPrefab;
    public Transform TransformCanvas;
    public GameObject LootboxPopUpPrefab;
    private readonly List<MissionUI> missionUIs = new List<MissionUI>();


    public void Add(Mission mission) {
        var missionUI = Instantiate(this.MissionBar, this.transform).GetComponentInChildren<MissionUI>();
        missionUI.Initialize(mission);
        this.missionUIs.Add(missionUI);
        missionUI.AttachedMissionQueue = this;
    }

    public static void DestroyMissionBar(MissionUI missionUI) {
        UnityEngine.Object.Destroy(missionUI.transform.parent.gameObject);
    }

    public void FinshedMission(Mission attachedMission) {
        attachedMission.MissionGeneral.IsSentToMission = false;
        var go = Instantiate(this.RewardPopUpPrefab, this.TransformCanvas);
        go.GetComponent<RewardPopUp>().Initialize(this.MoneyManager, this.RenownManager, this.VirtualCurrencyManager, this, attachedMission.Units, attachedMission.MissionGeneral);
        go.GetComponent<RewardPopUp>().ShowRewards(attachedMission.MissionDetails);
    }
}
