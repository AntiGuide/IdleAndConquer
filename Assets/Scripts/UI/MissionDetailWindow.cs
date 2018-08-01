using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDetailWindow : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Text missionTitle;

    [SerializeField] private Text rewardText;

    [SerializeField] private Image[] starImages;

    private void Start() {
        foreach (var starImage in this.starImages) {
            starImage.color = Color.black;
        }
    }

    public void FillInfo(string missionTitleText, string rewardTextText, ushort starCount) {
        this.missionTitle.text = missionTitleText;
        this.rewardText.text = rewardTextText;

        for (ushort i = 0; i < starCount; i++) {
            starImages[i].color = Color.white;
        }
    }
}
