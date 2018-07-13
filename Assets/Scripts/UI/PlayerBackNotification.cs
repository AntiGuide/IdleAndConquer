using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the notifications the player gets when he comes back to the game
/// </summary>
public class PlayerBackNotification : MonoBehaviour {
    /// <summary>The text on the pop up</summary>
    public Text NotificationText;

    private GameObject dailyRewardLootBoxPopUp;

    private Transform canvasTransform;

    private SoundController soundController;

    /// <summary>Reference to the FloatUpSpawner</summary>
    private FloatUpSpawner floatUpSpawner;

    /// <summary>The type of resource that the player gets</summary>
    private FloatUp.ResourceType type;

    /// <summary>The seconds the player paused the game</summary>
    private long secondsSincePause;

    /// <summary>The money the player gets additional to the secondsSincePause</summary>
    private long additionalMoney;

    /// <summary>List of all harvesters</summary>
    private List<Harvester> harvesters;

    /// <summary>
    /// Called to give a PlayerBackNotification all important variables
    /// </summary>
    /// <param name="notificationText">The text that is displayed on the notification</param>
    /// <param name="floatUpSpawner">Reference to the FloatUpSpawner</param>
    /// <param name="type">The type of resource that the player gets</param>
    /// <param name="secondsSincePause">The seconds the player paused the game</param>
    /// <param name="additionalMoney">The money the player gets additional to the secondsSincePause</param>
    /// <param name="harvesters">List of all harvesters</param>
    public void Initialize(string notificationText, FloatUpSpawner floatUpSpawner, FloatUp.ResourceType type, long secondsSincePause, long additionalMoney, SoundController soundController, ref List<Harvester> harvesters) {
        this.NotificationText.text = notificationText;
        this.floatUpSpawner = floatUpSpawner;
        this.type = type;
        this.secondsSincePause = secondsSincePause;
        this.additionalMoney = additionalMoney;
        this.harvesters = harvesters;
        this.soundController = soundController;
    }

    public void InitializeDaily(GameObject dailyRewardLootBoxPopUp, Transform canvasTransform) {
        this.dailyRewardLootBoxPopUp = dailyRewardLootBoxPopUp;
        this.canvasTransform = canvasTransform;
    }

    /// <summary>Called when the player clicks the notification. Grants earned money.</summary>
    public void OnClick() {
        long addedMoney = this.additionalMoney;
        foreach (Harvester h in this.harvesters) {
            addedMoney += h.AddAppPauseTime(this.secondsSincePause);
        }

        this.floatUpSpawner.GenerateFloatUp(addedMoney, this.type, transform.position);
        this.soundController.StartSound(SoundController.Sounds.REPORT_TAPS);
        MonoBehaviour.Destroy(this.gameObject);
    }

    public void OnDailyClick() {
        UnityEngine.Object.Instantiate(this.dailyRewardLootBoxPopUp, this.canvasTransform);
        MonoBehaviour.Destroy(this.gameObject);
    }
}
