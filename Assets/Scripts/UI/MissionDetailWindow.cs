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

    [Header("Animation")]
    [SerializeField] private float animationTime = 1f;

    public bool IsOpen;

    private float timeSpawnFinished;

    private bool animating = false;

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

        if (!this.IsOpen) {
            timeSpawnFinished = Time.time + animationTime;
            animating = true;
        }
    }

    private void Update() {
        if (!animating) {
            return;
        }
        var timeUntilFinished = Mathf.Max(timeSpawnFinished - Time.time, 0f);
        if (timeUntilFinished <= float.Epsilon) {
            IsOpen = true;
            animating = false;
        }
        var tmpScale = 0.1f + (0.9f * (1f - timeUntilFinished * (1f / animationTime)));
        transform.localScale = new Vector3(tmpScale, tmpScale, tmpScale);
    }
}
