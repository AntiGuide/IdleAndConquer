using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDetailWindow : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Text missionTitle;

    [SerializeField] private Text rewardText;

    [Header("Images")]
    [SerializeField] private Image[] starImages;

    [SerializeField] private Image[] lootBoxImages;

    [SerializeField] private Sprite[] lootBoxSpritesOpened;

    [SerializeField] private Sprite[] lootBoxSprites;


    private void Start() {
        foreach (var starImage in this.starImages) {
            starImage.color = Color.black;
        }
    }

    public void FillInfo(string missionTitleText, string rewardTextText, ushort starCount) {
        this.missionTitle.text = missionTitleText;
        this.rewardText.text = rewardTextText;

        for (ushort i = 0; i < 3; i++) {
            if (i < starCount) {
                starImages[i].color = Color.white;
                lootBoxImages[i].sprite = lootBoxSpritesOpened[i];
            } else {
                starImages[i].color = Color.black;
                lootBoxImages[i].sprite = lootBoxSprites[i];
            }
        }
    }
}
