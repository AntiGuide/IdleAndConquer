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

    public void SetTime(float time) {
        this.missionTime = time;
    }

    // Use this for initialization
    void Start() {
        this.img = this.GetComponent<Image>();
        this.moneyManagement = GameObject.Find("/Main/Canvas/BackgroundTopStripRessources/TextDollar").GetComponent<MoneyManagement>();
        this.floatUpSpawner = GameObject.Find("/Main/Canvas/UXElemente").GetComponent<FloatUpSpawner>();
    }

    // Update is called once per frame
    void Update() {
        if (this.missionTime > 0f) {
            this.aktTime += Time.deltaTime;
            if (this.aktTime / this.missionTime >= 1f) {
                this.img.fillAmount = 1f;
                this.missionTime = -1f;
                this.moneyManagement.AddMoney(10000);
                this.floatUpSpawner.GenerateFloatUp(10000, FloatUp.ResourceType.DOLLAR, new Vector2(300f, -273f));
                UnityEngine.Object.Destroy(this.gameObject);
                PlayerPrefs.DeleteKey("Mission");
            } else {
                this.img.fillAmount = this.aktTime / this.missionTime;
            }
        }
    }
}
