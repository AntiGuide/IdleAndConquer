using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopUp : MonoBehaviour {
    public Text PlayerCommunication;
    public Transform RewardsParent;
    public GameObject RewardPrefab;
    private MissionDetails missionDetails;

	public void GiveRewards() {

    }

    public void ShowRewards(MissionDetails missionDetails) {
        this.missionDetails = missionDetails;
        PlayerCommunication.text = "Congratulations! You beat " + missionDetails.EnemyGeneral + " and get:";
        GameObject go = Instantiate(RewardPrefab, transform);
        // go.GetComponent<MissionReward>();
    }
}
