using System;
using UnityEngine;

public class LoadGenerals : MonoBehaviour {
    public GameObject generalButtonMissionMap;
    // private float chanceDeath;
    // private string country;
    // private string generalName;
    // private int wins;
    // private int loses;

    private void OnEnable() {
        var generalButtons = transform.GetComponentsInChildren<GeneralButton>();
        foreach (var item in generalButtons) {
            UnityEngine.Object.Destroy(item.gameObject);
        }

        foreach (var item in GeneralManager.AllGenerals) {
            if (item.IsSentToMission) continue;
            var attachedButton = Instantiate(this.generalButtonMissionMap, transform).GetComponent<GeneralButtonMissionMap>();
            attachedButton.SetTexts(item.Country, item.GeneralName, item.Wins + Environment.NewLine + "-" + Environment.NewLine + item.Loses);
            attachedButton.General = item;
        }
    }
}
