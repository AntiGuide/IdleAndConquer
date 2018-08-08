using UnityEngine;
using UnityEngine.UI;

public class EnergyManagement : MonoBehaviour {
    public Image Bar;
    public int maxEnergy = 80;
    public int redStep = 0;
    public float blinkTime = 0.25f;
    public bool IsWaitingOnEBackUp = false;
    public TutorialFlow TutorialF;
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

    public void OutputEnergy(int curEnergy, bool lerpEffect) {
        this.curEnergy = curEnergy;
        //this.curEnergy = this.curEnergy <= this.redStep - 1 ? this.redStep - 1 : this.curEnergy;
        if (this.curEnergy < this.redStep) {
            this.blinking = true;
            this.blinkTimeAkt = this.blinkTime;
        } else {
            if (IsWaitingOnEBackUp) {
                IsWaitingOnEBackUp = false;
                TutorialF.PowerBackUp();
            }
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
            tmpOutEnergy = Mathf.Max(tmpOutEnergy, 0);
            tmpOutEnergy += 16;
            this.Bar.fillAmount = ((float)tmpOutEnergy / this.maxEnergy) - 0.0025833f;
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
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerPrefs.DeleteAll();
        }

        if (this.energyToLerpTo != curEnergyShown) {
            this.lerpTimeDone += Time.deltaTime;
            this.lerpTimeDone = Mathf.Min(this.lerpTimeStart, this.lerpTimeDone);

            curEnergyShown = (int) Mathf.Lerp(this.curEnergyOld, this.energyToLerpTo, this.lerpTimeDone / this.lerpTimeStart);
            var tmpOutEnergy = Mathf.RoundToInt(curEnergyShown * 0.986f);
            tmpOutEnergy = Mathf.Max(tmpOutEnergy, 0);
            tmpOutEnergy += 16;
            this.Bar.fillAmount = ((float)tmpOutEnergy / this.maxEnergy) - 0.0025833f;
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
