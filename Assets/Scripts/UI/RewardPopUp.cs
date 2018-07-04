using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopUp : MonoBehaviour {
    public Text PlayerCommunication;
    public Text PlayerRewards;
    // public Transform RewardsParent;
    // public GameObject RewardPrefab;
    private MissionDetails missionDetails;
    private MoneyManagement MoneyManager;
    private RenownManagement RenownManager;
    private VirtualCurrencyManagement VirtualCurrencyManager;

	public void GiveRewards() {
        MoneyManager.AddMoney(missionDetails.MissionMoneyReward);
        RenownManager.AddRenown(missionDetails.MissionRenownReward);
        VirtualCurrencyManager.AddVirtualCurrency(missionDetails.MissionVirtualReward);
        // TODO Blueprint
        Destroy(gameObject);
    }

    public void Initialize(MoneyManagement moneyManager, RenownManagement renownManager, VirtualCurrencyManagement virtualCurrencyManager) {
        this.MoneyManager = moneyManager;
        this.RenownManager = renownManager;
        this.VirtualCurrencyManager = virtualCurrencyManager;
    }

    public void ShowRewards(MissionDetails missionDetails) {
        this.missionDetails = missionDetails;
        PlayerCommunication.text = "Congratulations on beating " + missionDetails.EnemyGeneral + "!" + System.Environment.NewLine + System.Environment.NewLine + "You get:";
        PlayerRewards.text =    missionDetails.MissionMoneyReward.ToString() + " Dollar" + System.Environment.NewLine +
                                missionDetails.MissionRenownReward.ToString() + " Renown" + System.Environment.NewLine +
                                missionDetails.MissionVirtualReward.ToString() + " Virtual" + System.Environment.NewLine +
                                missionDetails.MissionBlueprintReward.ToString() + " Blueprint";

        // GameObject go = Instantiate(RewardPrefab, transform);
        // go.GetComponent<MissionReward>();
    }
}
