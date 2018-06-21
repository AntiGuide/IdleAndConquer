using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManagement : MonoBehaviour {
    public Image Bar;
    public float maxEnergy = 100;
    public float minEnergy = 0;
    public float redStep = 24f;
    private float curEnergy = 0;
    private Color prevColor;

    // Use this for initialization
    void Start() {
        //this.SetEnergy(50);
        this.prevColor = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update() {
        gameObject.GetComponent<Image>().color = this.curEnergy < this.redStep ? Color.red : this.prevColor;
    }

    public void OutputEnergy(float curEnergy, float maxEnergy, float minEnergy) {
        this.curEnergy = curEnergy;
        this.maxEnergy = maxEnergy;
        this.minEnergy = minEnergy;
        this.curEnergy = this.curEnergy <= this.redStep - 1 ? this.redStep - 1 : this.curEnergy;
        this.Bar.fillAmount = this.curEnergy / (this.maxEnergy - this.minEnergy);
    }
}
