using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopUp : MonoBehaviour {
    public Text PlayerCommunication;
    public Text PlayerRewards;
    private MissionDetails missionDetails;
    private MoneyManagement moneyManager;
    private RenownManagement renownManager;
    private VirtualCurrencyManagement virtualCurrencyManager;
    private MissionQueue missionQueue;

    public void GiveRewards() {
        this.moneyManager.AddMoney(this.missionDetails.MissionMoneyReward);
        this.renownManager.AddRenown(this.missionDetails.MissionRenownReward);
        this.virtualCurrencyManager.AddVirtualCurrency(this.missionDetails.MissionVirtualReward);
        this.missionQueue.OpenLootboxPopUp();
        UnityEngine.Object.Destroy(this.gameObject);
    }

    public void Initialize(MoneyManagement moneyManager, RenownManagement renownManager, VirtualCurrencyManagement virtualCurrencyManager, MissionQueue missionQueue) {
        this.moneyManager = moneyManager;
        this.renownManager = renownManager;
        this.virtualCurrencyManager = virtualCurrencyManager;
        this.missionQueue = missionQueue;
    }

    public void ShowRewards(MissionDetails missionDetails) {
        this.missionDetails = missionDetails;
        this.PlayerCommunication.text = "Congratulations on beating " + this.missionDetails.EnemyGeneral + "!" + System.Environment.NewLine + System.Environment.NewLine + "You get:";
        this.PlayerRewards.text = this.missionDetails.MissionMoneyReward.ToString() + " Dollar" + System.Environment.NewLine +
                             this.missionDetails.MissionRenownReward.ToString() + " Renown" + System.Environment.NewLine +
                             this.missionDetails.MissionVirtualReward.ToString() + " Virtual" + System.Environment.NewLine +
                             this.missionDetails.MissionBlueprintReward.ToString() + " Blueprint";
        
        // GameObject go = Instantiate(RewardPrefab, transform);
        // go.GetComponent<MissionReward>();
    }
}
