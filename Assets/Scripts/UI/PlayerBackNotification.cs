using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBackNotification : MonoBehaviour {
    public Text text;

    private FloatUpSpawner floatUpSpawner;
    private FloatUp.ResourceType type;
    private long secondsSincePause;
    private long additionalMoney;
    private List<Harvester> Harvesters;

    public void Initialize(string text, FloatUpSpawner floatUpSpawner, FloatUp.ResourceType type, long secondsSincePause, long additionalMoney, ref List<Harvester> Harvesters) {
        this.text.text = text;
        this.floatUpSpawner = floatUpSpawner;
        this.type = type;
        this.secondsSincePause = secondsSincePause;
        this.additionalMoney = additionalMoney;
        this.Harvesters = Harvesters;
    }

    public void OnClick() {
        long addedMoney = additionalMoney;
        foreach (Harvester h in Harvesters) {
            addedMoney += h.AddAppPauseTime(secondsSincePause);
        }
        this.floatUpSpawner.GenerateFloatUp(addedMoney, type, transform.position);
        Destroy(gameObject);
    }
}
