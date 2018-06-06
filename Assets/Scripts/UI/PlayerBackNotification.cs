using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBackNotification : MonoBehaviour {
    public Text text;

    private FloatUpSpawner floatUpSpawner;
    private FloatUp.ResourceType type;
    private long value;
    private long secondsSincePause;
    private List<Harvester> Harvesters;

    public void Initialize(string text, FloatUpSpawner floatUpSpawner, FloatUp.ResourceType type, long value, long secondsSincePause, ref List<Harvester> Harvesters) {
        this.text.text = text;
        this.floatUpSpawner = floatUpSpawner;
        this.type = type;
        this.value = value;
        this.secondsSincePause = secondsSincePause;
        this.Harvesters = Harvesters;
    }

    public void OnClick() {
        this.floatUpSpawner.GenerateFloatUp(value, type, transform.position);
        foreach (Harvester h in Harvesters) {
            h.AddAppPauseTime(secondsSincePause);
        }
        Destroy(gameObject);
    }
}
