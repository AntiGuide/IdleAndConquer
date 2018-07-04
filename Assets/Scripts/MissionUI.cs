using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {
    public MoneyManagement moneyManagement;
    public FloatUpSpawner floatUpSpawner;
    public MissionQueue MissionQueue;
    private Image img;
    private float missionTime = -1f;
    private long missionMoneyReward;
    private float aktTime = 0f;
    private List<Unit> unitsInMission;
    private Mission attachedMission;

    public void SetTime(float time) {
        this.missionTime = time;
    }

    // Use this for initialization
    void Start() {
        this.img = this.GetComponent<Image>();
    }

    public void Initialize(Mission mission) {
        this.missionTime = mission.MissionDetails.MissionTime;
        this.missionMoneyReward = mission.MissionDetails.MissionMoneyReward;
        unitsInMission = mission.Units;
        this.attachedMission = mission;
    }

    // Update is called once per frame
    void Update() {
        if (this.missionTime > 0f) {
            this.aktTime += Time.deltaTime;
            if (this.aktTime / this.missionTime >= 1f) {
                this.img.fillAmount = 1f;
                this.missionTime = -1f;
                foreach (Unit item in unitsInMission) {
                    item.SentToMission--;
                }
                // Instantiate RewardPopUp
                // Initialize RewardPopUp
                this.moneyManagement.AddMoney(missionMoneyReward);
                this.floatUpSpawner.GenerateFloatUp(missionMoneyReward, FloatUp.ResourceType.DOLLAR, transform.position);
                this.MissionQueue.DestroyMissionBar(this);
            } else {
                this.img.fillAmount = this.aktTime / this.missionTime;
            }
        }
    }
}
