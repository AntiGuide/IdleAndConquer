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
            this.blinkTimeAkt = this.blinkTime;
        } else {
            this.isRed = false;
        }

        this.Bar.fillAmount = this.curEnergy / (this.maxEnergy - this.minEnergy);
    }

    // Use this for initialization
    private void Start() {
        this.prevColor = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    private void Update() {
        this.blinkTimeAkt -= Time.deltaTime;
        if (this.blinking && this.blinkTimeAkt <= 0) {
            this.isRed = !this.isRed;
            this.blinkTimeAkt = this.blinkTime;
        }

        gameObject.GetComponent<Image>().color = this.isRed ? Color.red : this.prevColor;
    }
}
