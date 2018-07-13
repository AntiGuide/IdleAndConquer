using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedResearchReward : MonoBehaviour {

    public int SecondsComplete;
    public Text RemainingTimeText;
    public AppPauseHandler AppPauseHandle;
    private float timeBeginning;
    private Image fillImage;

    // Use this for initialization
    void Start() {
        this.timeBeginning = Time.time;
        this.fillImage = gameObject.GetComponent<Image>();
        this.AppPauseHandle = GameObject.Find("/Main/Canvas/UXElemente").GetComponent<AppPauseHandler>();
    }
    
    // Update is called once per frame
    void Update() {
        this.fillImage.fillAmount = Mathf.Min((SecondsComplete - (Time.time - this.timeBeginning)) / (float)SecondsComplete, 1f);
        RemainingTimeText.text = (int)((SecondsComplete - ((int)Time.time - (int)this.timeBeginning)) / 60) + ":" + Mathf.RoundToInt((SecondsComplete - ((int)Time.time - (int)this.timeBeginning)) % 60).ToString("00");
        if ((int)Time.time - (int)this.timeBeginning >= SecondsComplete) {
            // Complete
            this.AppPauseHandle.DailyLootBoxPopUp();
            this.timeBeginning = Time.time;
        }
    }
}
