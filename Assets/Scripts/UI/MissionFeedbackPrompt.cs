using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionFeedbackPrompt : MonoBehaviour {
    [SerializeField] private Image[] stars;
    [SerializeField] private Text playerCommunicationText;
    [SerializeField] private Text windowTitleText;
    [SerializeField] private VirtualCurrencyManagement virtualCurrencyManager;
    [SerializeField] private MoneyManagement moneyManager;
    [SerializeField] private RenownManagement renownManager;
    [SerializeField] private int[] vCoinReward = { 10, 15, 25 };
    [SerializeField] private LootBoxStackManager.LootboxType[] lootboxReward = new LootBoxStackManager.LootboxType[3];
    [SerializeField] private Transform transformCanvas;
    [SerializeField] private LootBoxStackManager lootBoxStackManager;

    private MissionDetails.Ratings achievedRating;
    private MissionDetails.Ratings prevMaxRating;
    private int renownReward;
    private int moneyReward;

    public void ShowMissionOutcome(int renownReward, int moneyReward,  MissionDetails.Ratings prevMaxRating, MissionDetails.Ratings achievedRating, string missionName) {
        gameObject.SetActive(true);

        this.renownReward = renownReward;
        this.moneyReward = moneyReward;
        this.prevMaxRating = prevMaxRating;
        this.achievedRating = achievedRating;
        this.windowTitleText.text = missionName;

        var tmpRating = achievedRating;
        foreach (var star in stars) {
            if (tmpRating <= 0) {
                break;
            }

            star.color = Color.white;
            tmpRating--;
        }

        switch (achievedRating) {
            case MissionDetails.Ratings.NOT_COMPLETED:
                playerCommunicationText.text = "The mission failed!" + System.Environment.NewLine + System.Environment.NewLine + "All units died in the fight";
                break;
            case MissionDetails.Ratings.ONE_STAR:
                playerCommunicationText.text = "Congratulations!" + System.Environment.NewLine + System.Environment.NewLine + "You Beat the mission and earned one star but lost some units";
                break;
            case MissionDetails.Ratings.TWO_STAR:
                playerCommunicationText.text = "Congratulations!" + System.Environment.NewLine + System.Environment.NewLine + "You Beat the mission and earned two stars and didnt lose anyone";
                break;
            case MissionDetails.Ratings.THREE_STAR:
                playerCommunicationText.text = "Congratulations!" + System.Environment.NewLine + System.Environment.NewLine + "You Beat the mission and earned all stars";
                break;
            default:
                throw new ArgumentOutOfRangeException("achievedRating", achievedRating, null);
        }
    }

    private void Reset() {
        foreach (var star in stars) {
            star.color = Color.black;
        }
    }

    public void OnOKClick() {
        // Give Money
        this.moneyManager.AddMoney(moneyReward);

        // Give Renown
        this.renownManager.AddRenown(renownReward);

        // Give V
        // Give Lootboxes
        while (prevMaxRating < achievedRating) {
            this.virtualCurrencyManager.AddVirtualCurrency(vCoinReward[(int)prevMaxRating]);
            //UnityEngine.Object.Instantiate(this.lootboxRewardPrefabs[(int)prevMaxRating], this.transformCanvas);
            lootBoxStackManager.AddLootbox(lootboxReward[(int)prevMaxRating]);
            prevMaxRating++;
        }

        Reset();
        gameObject.SetActive(false);
    }
}
