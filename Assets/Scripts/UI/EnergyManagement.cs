using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManagement : MonoBehaviour {
    public Image Bar;
    public float maxEnergy = 100;
    public float minEnergy = 0;
    public float redStep = 24f;
    public float blinkTime = 0.25f;
    private float curEnergy = 0;
    private Color prevColor;
    private bool blinking = false;
    private float blinkTimeAkt;
    private bool isRed = false;

    public void OutputEnergy(float curEnergy, float maxEnergy, float minEnergy) {
        this.curEnergy = curEnergy;
        this.maxEnergy = maxEnergy;
        this.minEnergy = minEnergy;
        this.curEnergy = this.curEnergy <= this.redStep - 1 ? this.redStep - 1 : this.curEnergy;
        if (this.curEnergy <= this.redStep - 1) {
            this.blinking = true;
            blinkTimeAkt = blinkTime;
        } else {
            isRed = false;
        }
        this.Bar.fillAmount = this.curEnergy / (this.maxEnergy - this.minEnergy);
    }

    // Use this for initialization
    void Start() {
        this.prevColor = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update() {
        blinkTimeAkt -= Time.deltaTime;
        if (blinking && blinkTimeAkt <= 0) {
            isRed = !isRed;
            blinkTimeAkt = blinkTime;
        }
        gameObject.GetComponent<Image>().color = isRed ? Color.red : this.prevColor;
    }
}
