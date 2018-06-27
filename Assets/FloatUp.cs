using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles a single FloatUp UXElement
/// </summary>
public class FloatUp : MonoBehaviour {
    /// <summary>Time in which the FloatUp completely fades</summary>
    private float fadeTime;

    /// <summary>The position at which the FloatUp starts</summary>
    private Vector2 startPos;

    /// <summary>The position which the FloatUp floats to</summary>
    private Vector2 destination;

    /// <summary>The percentage of the way that is completed</summary>
    private float percentage = 0f;

    /// <summary>The text of the FloatUp</summary>
    private Text text;

    /// <summary>Reference to the shadows for control of their alpha channel</summary>
    private Shadow[] shadows;

    /// <summary>The accent color of the FloatUp</summary>
    private Color color;

    /// <summary>The resource type that got added</summary>
    private ResourceType type;

    /// <summary>The value of the FloatUp</summary>
    private long value;

    public enum ResourceType {
        POWERLEVEL = 0,
        DOLLAR
    }

    public void Initialize(ResourceType type, long value, float fadeTime, float travelDistance) {
        if (value == 0) {
            throw new ArgumentException("Value my not be zero!");
        }

        this.type = type;
        this.value = value;
        this.fadeTime = fadeTime;
        this.startPos = transform.position;
        travelDistance *= value < 0 ? -1f : 1f;
        this.destination = this.startPos + new Vector2(0f, travelDistance);
    }

    // Use this for initialization
    void Start() {
        this.text = gameObject.GetComponentInChildren<Text>();
        this.shadows = gameObject.GetComponentsInChildren<Shadow>();
    }

    // Update is called once per frame
    void Update() {
        this.percentage += Time.deltaTime / this.fadeTime;
        if (this.percentage >= 1f) {
            UnityEngine.Object.Destroy(this.gameObject);
        }

        transform.position = Vector2.Lerp(this.startPos, this.destination, this.percentage);
        this.text.color = new Color(this.text.color.r, this.text.color.g, this.text.color.b, 1f - this.percentage);
        foreach (Shadow shadow in this.shadows) {
            shadow.effectColor = new Color(shadow.effectColor.r, shadow.effectColor.g, shadow.effectColor.b, 1f - this.percentage);
        }

        if (this.value > 0) {
            this.color = Color.green;
            this.text.text = "<color=#" + this.ColorToHex(new Color(this.color.r, this.color.g, this.color.b, 1 - this.percentage)) + ">+</color>";
        } else {
            this.color = new Color(0.8f, 0f, 0f);
            this.text.text = "<color=#" + this.ColorToHex(new Color(this.color.r, this.color.g, this.color.b, 1 - this.percentage)) + ">-</color>";
        }

        this.text.text += " " + Math.Abs(this.value);
        switch (this.type) {
            case ResourceType.POWERLEVEL:
                this.text.text += " PL";
                break;
            case ResourceType.DOLLAR:
                this.text.text += " $";
                break;
            default:
                break;
        }
    }

    private string ColorToHex(Color color) {
        Color32 color32 = color;
        return color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2") + color32.a.ToString("X2");
    }
}
