using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAvailability : MonoBehaviour {
    public Image UnavailableImage;

    public void SetUnavailable() {
        UnavailableImage.color = new Color(0f,0f,0f,0.75f);
    }

    public void SetAvailable() {
        UnavailableImage.color = new Color(0f, 0f, 0f, 0f);
    }
}
