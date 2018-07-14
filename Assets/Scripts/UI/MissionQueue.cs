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
    }

    public void DestroyMissionBar(MissionUI missionUI) {
        UnityEngine.Object.Destroy(missionUI.transform.parent.gameObject);
    }

    public void FinshedMission(Mission attachedMission) {
        attachedMission.MissionGeneral.IsSentToMission = false;
        GameObject go = Instantiate(this.RewardPopUpPrefab, this.TransformCanvas);
        go.GetComponent<RewardPopUp>().Initialize(this.MoneyManager, this.RenownManager, this.VirtualCurrencyManager, this);
        go.GetComponent<RewardPopUp>().ShowRewards(attachedMission.MissionDetails);
    }

    public void OpenLootboxPopUp() {
        UnityEngine.Object.Instantiate(this.LootboxPopUpPrefab, this.TransformCanvas);
    }
}
