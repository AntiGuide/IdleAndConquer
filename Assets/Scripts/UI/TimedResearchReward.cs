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
    void Start () {
        timeBeginning = Time.time;
        fillImage = gameObject.GetComponent<Image>();
        AppPauseHandle = GameObject.Find("/Main/Canvas/UXElemente").GetComponent<AppPauseHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        fillImage.fillAmount = Mathf.Min((SecondsComplete - (Time.time - timeBeginning)) / (float)SecondsComplete, 1f);
        RemainingTimeText.text = (int)(((SecondsComplete - ((int)Time.time - (int)timeBeginning)) / 60)) + ":" + Mathf.RoundToInt(((SecondsComplete - ((int)Time.time - (int)timeBeginning)) % 60)).ToString("00");
        if ((int)Time.time - (int)timeBeginning >= SecondsComplete) {
            // Complete
            AppPauseHandle.DailyLootBoxPopUp();
            timeBeginning = Time.time;
        }
    }
}
