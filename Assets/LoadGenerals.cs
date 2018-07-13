using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGenerals : MonoBehaviour {
    public GameObject generalButtonMissionMap;
    private float chanceDeath;
    private string country;
    private string generalName;
    private int wins;
    private int loses;

    // Use this for initialization
    void Start() {
    }

    private void OnEnable() {
        GeneralButton[] GeneralButtons = transform.GetComponentsInChildren<GeneralButton>();
        foreach (GeneralButton item in GeneralButtons) {
            UnityEngine.Object.Destroy(item.gameObject);
        }
        foreach (General item in GeneralManager.AllGenerals) {
            if (!item.IsSentToMission) {
                GeneralButtonMissionMap attachedButton = Instantiate(this.generalButtonMissionMap, transform).GetComponent<GeneralButtonMissionMap>();
                attachedButton.SetTexts(item.Country, item.GeneralName, item.Wins + Environment.NewLine + "-" + Environment.NewLine + item.Loses);
                attachedButton.General = item;
            }
        }
    }
}
