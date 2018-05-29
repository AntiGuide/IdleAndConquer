using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {

    public MoneyManagement moneyManagement;
    public FloatUpSpawner floatUpSpawner;

    private Image img;
    private float missionTime = -1f;
    private float aktTime = 0f;

    // Use this for initialization
    void Start() {
        img = GetComponent<Image>();
        moneyManagement = GameObject.Find("Canvas/BackgroundTopStripRessources/TextDollar").GetComponent<MoneyManagement>();
        floatUpSpawner = GameObject.Find("Canvas/UXElemente").GetComponent<FloatUpSpawner>();
    }

    // Update is called once per frame
    void Update() {
        if (missionTime > 0f) {
            aktTime += Time.deltaTime;
            if (aktTime / missionTime >= 1f) {
                img.fillAmount = 1f;
                missionTime = -1f;
                moneyManagement.addMoney(10000);
                //TODO
                floatUpSpawner.GenerateFloatUp(10000, FloatUp.ResourceType.DOLLAR,new Vector2(300f, -273f));
                Destroy(gameObject);
                PlayerPrefs.DeleteKey("Mission");
            } else {
                img.fillAmount = aktTime / missionTime;
            }

        }

    }
    internal void SetTime(float time) {
        missionTime = time;
    }
}
