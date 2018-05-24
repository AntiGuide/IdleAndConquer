using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatUpSpawner : MonoBehaviour {

    public GameObject floatUp;

    public enum ResourceType {
        POWERLEVEL = 0,
        DOLLAR
    }

    // Use this for initialization
    void Start () {
        //generateFloatUp(1337, ResourceType.POWERLEVEL).transform.position = new Vector2(200,200);
        //generateFloatUp(-9999, ResourceType.DOLLAR).transform.position = new Vector2(200, 300);
        //generateFloatUp(9999, ResourceType.DOLLAR).transform.position = new Vector2(200, 400);
        //generateFloatUp(-9999, ResourceType.POWERLEVEL).transform.position = new Vector2(200, 500);

    }

    // Update is called once per frame
    void Update () {
		
	}

    public GameObject generateFloatUp(int value, ResourceType type) {
        if (value == 0) {
            throw new ArgumentException("Value my not be zero!");
        }
        GameObject newFloatUp = Instantiate(floatUp,transform);
        Text text = newFloatUp.GetComponentInChildren<Text>();
        Image image = newFloatUp.GetComponentInChildren<Image>();
        if (value > 0) {
            text.text = "<color=#" + ColorToHex(Color.green) + ">+</color>";
        } else {
            text.text = "<color=#" + ColorToHex(new Color(0.8f,0f,0f)) + ">-</color>";
            image.transform.localEulerAngles = new Vector3(image.transform.localEulerAngles.x, image.transform.localEulerAngles.y, image.transform.localEulerAngles.z + 180f);
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
        return newFloatUp;
    }

    private string ColorToHex(Color color) {
        Color32 color32 = color;
        return color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2") + color32.a.ToString("X2");

    }

}
