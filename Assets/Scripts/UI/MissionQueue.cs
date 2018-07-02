using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionQueue : MonoBehaviour {
    public GameObject MissionBar;
    public MoneyManagement MoneyManagement;
    public FloatUpSpawner FloatUpSpawner;
    private List<MissionUI> missionUIs = new List<MissionUI>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Add(Mission mission) {
        MissionUI missionUI = Instantiate(MissionBar, transform).GetComponentInChildren<MissionUI>();
        missionUI.Initialize(mission);
        missionUIs.Add(missionUI);
        missionUI.MissionQueue = this;
        missionUI.moneyManagement = this.MoneyManagement;
        missionUI.floatUpSpawner = this.FloatUpSpawner;
    }

    public void DestroyMissionBar(MissionUI missionUI) {
        Destroy(missionUI.transform.parent.gameObject);
    }
}
