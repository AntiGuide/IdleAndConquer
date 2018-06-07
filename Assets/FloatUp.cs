using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatUp : MonoBehaviour {

    private float fadeTime;
    private Vector2 startPos;
    private Vector2 destination;
    private float percentage = 0f;
    private Text text;
    private Shadow[] shadows;
    private Color color;
    private ResourceType type;
    private long value;

    public enum ResourceType {
        POWERLEVEL = 0,
        DOLLAR
    }

    // Use this for initialization
    void Start () {
        text = gameObject.GetComponentInChildren<Text>();
        shadows = gameObject.GetComponentsInChildren<Shadow>();
    }
	
	// Update is called once per frame
	void Update () {
        percentage += Time.deltaTime / fadeTime;
        if (percentage >= 1f) {
            Destroy(gameObject);
        }
        transform.position = Vector2.Lerp(startPos, destination, percentage);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f - percentage);
        foreach (Shadow shadow in shadows) {
            shadow.effectColor = new Color(shadow.effectColor.r, shadow.effectColor.g, shadow.effectColor.b, 1f - percentage);
        }
        if (value > 0) {
            this.color = Color.green;
            text.text = "<color=#" + ColorToHex(new Color(color.r, color.g, color.b, 1 - percentage)) + ">+</color>";
        } else {
            this.color = new Color(0.8f, 0f, 0f);
            text.text = "<color=#" + ColorToHex(new Color(color.r, color.g, color.b, 1 - percentage)) + ">-</color>";
        }
        text.text += " " + Math.Abs(value);
        switch (type) {
            case ResourceType.POWERLEVEL:
                text.text += " PL";
                break;
            case ResourceType.DOLLAR:
                text.text += " $";
                break;
            default:
                break;
        }
    }

    private string ColorToHex(Color color) {
        Color32 color32 = color;
        return color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2") + color32.a.ToString("X2");
    }

    public void Initialize(ResourceType type, long value, float fadeTime, float travelDistance) {
        if (value == 0) {
            throw new ArgumentException("Value my not be zero!");
        }
        this.type = type;
        this.value = value;
        this.fadeTime = fadeTime;
        startPos = transform.position;
        travelDistance *= value < 0 ? -1f : 1f;
        destination = startPos + new Vector2(0f, travelDistance);
    }
}
