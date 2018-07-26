using System;
using UnityEngine;
using UnityEngine.UI;

public class TimedResearchReward : MonoBehaviour {
    public static bool BeginNewTimerNow;
    public int SecondsComplete;
    public Text RemainingTimeText;
    public AppPauseHandler AppPauseHandle;
    private DateTime timeBeginning;
    private Image fillImage;
    private DateTime tmpNow;
    private bool timerRunning;

    // Use this for initialization
    private void Start() {
        BeginNewTimerNow = true;
        this.fillImage = gameObject.GetComponent<Image>();
        this.AppPauseHandle = GameObject.Find("/Main/Canvas/UXElemente").GetComponent<AppPauseHandler>();
    }
    
    // Update is called once per frame
    private void Update() {
        tmpNow = System.DateTime.Now;
        if (BeginNewTimerNow) {
            this.timeBeginning = tmpNow;
            BeginNewTimerNow = false;
            timerRunning = true;
        }

        if (!timerRunning) return;

        this.fillImage.fillAmount = Mathf.Min((this.SecondsComplete - ((float)tmpNow.Subtract(this.timeBeginning).TotalSeconds)) / this.SecondsComplete, 1f);
        this.RemainingTimeText.text = (int)(this.SecondsComplete - ((float)tmpNow.Subtract(this.timeBeginning).TotalSeconds)) / 60 + ":" + Mathf.RoundToInt((this.SecondsComplete - ((float)tmpNow.Subtract(this.timeBeginning).TotalSeconds)) % 60).ToString("00");
        if ((float)tmpNow.Subtract(this.timeBeginning).TotalSeconds < this.SecondsComplete) return;
        
        timerRunning = false;
        this.AppPauseHandle.DailyLootBoxPopUp();

    }
}
