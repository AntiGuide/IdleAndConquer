using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionQueue : MonoBehaviour {
    public GameObject MissionBar;
    public MoneyManagement MoneyManager;
    public RenownManagement RenownManager;
    public VirtualCurrencyManagement VirtualCurrencyManager;
    public FloatUpSpawner FloatUpSpawner;
    public GameObject RewardPopUpPrefab;
    public Transform TransformCanvas;
    public GameObject LootboxPopUpPrefab;
    private List<MissionUI> missionUIs = new List<MissionUI>();

    public void Add(Mission mission) {
        MissionUI missionUI = Instantiate(this.MissionBar, this.transform).GetComponentInChildren<MissionUI>();
        missionUI.Initialize(mission);
        this.missionUIs.Add(missionUI);
        missionUI.MissionQueue = this;
        // missionUI.moneyManagement = this.MoneyManagement;
        // missionUI.floatUpSpawner = this.FloatUpSpawner;
    }

    public void DestroyMissionBar(MissionUI missionUI) {
        UnityEngine.Object.Destroy(missionUI.transform.parent.gameObject);
    }

    public void FinshedMission(Mission attachedMission) {
        attachedMission.MissionGeneral.IsSentToMission = false;
        // Instantiate RewardPopUp
        GameObject go = Instantiate(this.RewardPopUpPrefab, this.TransformCanvas);
        // Initialize RewardPopUp
        go.GetComponent<RewardPopUp>().Initialize(this.MoneyManager, this.RenownManager, this.VirtualCurrencyManager, this);
        go.GetComponent<RewardPopUp>().ShowRewards(attachedMission.MissionDetails);
    }

    public void OpenLootboxPopUp() {
        Instantiate(this.LootboxPopUpPrefab, this.TransformCanvas);
    }
}
