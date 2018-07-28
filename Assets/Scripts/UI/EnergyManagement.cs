using UnityEngine;
using UnityEngine.UI;

public class EnergyManagement : MonoBehaviour {
    public Image Bar;
    public int maxEnergy = 96;
    public int minEnergy = 0;
    public int redStep = 24;
    public float blinkTime = 0.25f;
    private int curEnergy = 0;
    private Color prevColor;
    private bool blinking = false;
    private float blinkTimeAkt;
    private bool isRed = false;

    private int curEnergyOld;
    private int curEnergyShown;
    private int energyToLerpTo;
    private float lerpTimeDone;
    private bool isFinished;
    public float lerpTimeStart = 1.0f;

    public void OutputEnergy(int curEnergy, int maxEnergy, int minEnergy, bool lerpEffect) {
        this.curEnergy = curEnergy;
        this.maxEnergy = maxEnergy;
        this.minEnergy = minEnergy;
        this.curEnergy = this.curEnergy <= this.redStep - 1 ? this.redStep - 1 : this.curEnergy;
        if (this.curEnergy <= this.redStep - 1) {
            this.blinking = true;
            this.blinkTimeAkt = this.blinkTime;
        } else {
            this.blinking = false;
            this.isRed = false;
        }

        if (lerpEffect) {
            this.curEnergyOld = this.curEnergyShown;
            this.energyToLerpTo = this.curEnergy;
            this.lerpTimeDone = 0.0f;
            this.isFinished = false;
        } else {
            var tmpOutEnergy = Mathf.RoundToInt(curEnergy * 0.986f);
            this.Bar.fillAmount = ((float)tmpOutEnergy / (this.maxEnergy - this.minEnergy)) - 0.0025833f;
            this.curEnergyShown = curEnergy;
            this.energyToLerpTo = curEnergy;
        }

        
    }

    // Use this for initialization
    private void Start() {
        this.prevColor = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    private void Update() {
        if (this.energyToLerpTo != curEnergyShown) {
            this.lerpTimeDone += Time.deltaTime;
            this.lerpTimeDone = Mathf.Min(this.lerpTimeStart, this.lerpTimeDone);

            curEnergyShown = (int) Mathf.Lerp(this.curEnergyOld, this.energyToLerpTo, this.lerpTimeDone / this.lerpTimeStart);
            var tmpOutEnergy = Mathf.RoundToInt(curEnergyShown * 0.986f);
            this.Bar.fillAmount = ((float)tmpOutEnergy / (this.maxEnergy - this.minEnergy)) - 0.0025833f;
        } else if (!this.isFinished) {
            this.isFinished = true;
        }

        this.blinkTimeAkt -= Time.deltaTime;
        if (this.blinking && this.blinkTimeAkt <= 0) {
            this.isRed = !this.isRed;
            this.blinkTimeAkt = this.blinkTime;
        }

        gameObject.GetComponent<Image>().color = this.isRed ? Color.red : this.prevColor;
    }
}
