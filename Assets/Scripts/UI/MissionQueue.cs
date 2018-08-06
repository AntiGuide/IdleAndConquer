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
    [SerializeField] private MissionFeedbackPrompt missionFeedbackPrompt;
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
        attachedMission.MissionDetails.currentlyRunning = false;
        attachedMission.MissionGeneral.IsSentToMission = false;
        var tmpMissionDetails = attachedMission.MissionDetails;

        var achievedRating = attachedMission.MissionDetails.CalculateBattle(attachedMission.Units, attachedMission.MissionGeneral);
        if (!Enum.IsDefined(typeof(MissionDetails.Ratings), achievedRating)) {
            return;
        }
        
        missionFeedbackPrompt.ShowMissionOutcome(tmpMissionDetails.MissionRenownReward, tmpMissionDetails.MissionMoneyReward,tmpMissionDetails.AktRating, achievedRating,attachedMission.MissionDetails.name);

        tmpMissionDetails.AktRating = achievedRating;
        tmpMissionDetails.SaveRating();
        // var go = Instantiate(this.RewardPopUpPrefab, this.TransformCanvas);
        // go.GetComponent<RewardPopUp>().Initialize(this.MoneyManager, this.RenownManager, this.VirtualCurrencyManager, this, attachedMission.Units, attachedMission.MissionGeneral);
        // go.GetComponent<RewardPopUp>().ShowRewards(attachedMission.MissionDetails);
    }
}
