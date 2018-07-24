using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {
    // public MoneyManagement moneyManagement;
    // public FloatUpSpawner floatUpSpawner;
    public MissionQueue AttachedMissionQueue;
    private Image img;
    private float missionTime = -1f;
    // private long missionMoneyReward;
    private float aktTime = 0f;
    private List<Unit> unitsInMission;
    private Mission attachedMission;

    public void SetTime(float time) {
        this.missionTime = time;
    }

    public void Initialize(Mission mission) {
        this.missionTime = mission.MissionDetails.MissionTime;
        // this.missionMoneyReward = mission.MissionDetails.MissionMoneyReward;
        this.unitsInMission = mission.Units;
        this.attachedMission = mission;
    }

    // Use this for initialization
    private void Start() {
        this.img = this.GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update() {
        if (!(this.missionTime > 0f)) return;
        this.aktTime += Time.deltaTime;
        if (this.aktTime / this.missionTime >= 1f) {
            this.img.fillAmount = 1f;
            this.missionTime = -1f;
                
            // this.floatUpSpawner.GenerateFloatUp(missionMoneyReward, FloatUp.ResourceType.DOLLAR, transform.position);
            this.AttachedMissionQueue.FinshedMission(this.attachedMission);
            MissionQueue.DestroyMissionBar(this);
        } else {
            this.img.fillAmount = this.aktTime / this.missionTime;
        }
    }
}
