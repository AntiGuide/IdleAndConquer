using UnityEngine;

/// <summary>
/// Operates automatically. Refinery level equals number of Havesters operating. Drives to Mine to harvest and then unloads at Refinery to generate income.
/// $ is the only universal Resource (so far). Mine --> $
/// </summary>
public class Harvester : MonoBehaviour {
    /// <summary>Generates MiningAmount every 10 seconds</summary>
    public float miningSpeed = 10.0f;

    /// <summary>The time in seconds the harvester will take to load the ore at the mine</summary>
    public float LoadingOnSpeed = 2.0f;

    /// <summary>The time in seconds the harvester will take to load off the ore at the refinery</summary>
    public float LoadingOffSpeed = 2.0f;

    /// <summary>Every time the refinery is reached you get this amount of money</summary>
    public int miningAmount = 500;

    private int levelSpeed = 0;

    private int levelMoney = 0;

    /// <summary>The mine to drive to</summary>
    private GameObject attachedMine;

    /// <summary>The ore refinery to drive to</summary>
    private OreRefinery attachedOreRefinery;

    /// <summary>Reference to the money pool</summary>
    private MoneyManagement moneyManagement;

    /// <summary>Progress of the harvester in seconds (0 --> MinigSpeed)</summary>
    private float currentProgressWay;

    /// <summary>Reference to the FloatUpSpawner</summary>
    private FloatUpSpawner floatUpSpawner;

    /// <summary>Getter/Setter for miningSpeed</summary>
    public float MiningSpeed {
        get { return this.miningSpeed - (this.miningSpeed * Unit.HPBoostLevel[this.levelSpeed] - this.miningSpeed); }
    }

    /// <summary>Getter/Setter for miningAmount</summary>
    private int MiningAmount {
        get { return Mathf.RoundToInt(this.miningAmount * Unit.HPBoostLevel[this.levelMoney]); }
    }

    /// <summary>
    /// Gives the harvester all important values
    /// </summary>
    /// <param name="attachedOreRefinery">Ore Refinery to drive to</param>
    /// <param name="attachedMine">Mine to drive to</param>
    /// <param name="moneyManagement">Reference to the money pool</param>
    /// <param name="floatUpSpawner">Reference to the FloatUpSpawner</param>
    public void Initialize(OreRefinery attachedOreRefinery, GameObject attachedMine, ref MoneyManagement moneyManagement, FloatUpSpawner floatUpSpawner) {
        this.attachedOreRefinery = attachedOreRefinery;
        this.attachedMine = attachedMine;
        this.moneyManagement = moneyManagement;
        this.floatUpSpawner = floatUpSpawner;
        AppPauseHandler.Harvesters.Add(this);
    }

    public void UpgradeSpeed() {
        this.levelSpeed++;
    }

    public void UpgradeGeneratedMoney() {
        this.levelMoney++;
    }

    /// <summary>
    /// Adds progress for uncompleted harvester runs
    /// </summary>
    /// <param name="secondsToAdd">The seconds progress has to be added for</param>
    /// <returns>Money that was made (in case a harvester finished)</returns>
    public long AddAppPauseProgressTime(long secondsToAdd) {
        this.currentProgressWay += secondsToAdd % (long)this.MiningSpeed;
        if (this.currentProgressWay > this.MiningSpeed / 2 && this.currentProgressWay <= this.MiningSpeed) {
            transform.LookAt(this.attachedOreRefinery.transform.position);
        } else if (this.currentProgressWay > this.MiningSpeed) {
            this.currentProgressWay -= this.MiningSpeed;
            transform.LookAt(this.attachedMine.transform.position);
            return this.MiningAmount;
        }

        return 0;
    }

    /// <summary>
    /// Adds money for completed harvester runs
    /// </summary>
    /// <param name="secondsToAdd">The seconds money has to be added for</param>
    /// <returns></returns>
    public long AddAppPauseTime(long secondsToAdd) {
        this.moneyManagement.AddMoney(secondsToAdd / (long)this.MiningSpeed * this.MiningAmount);
        return secondsToAdd / (long)this.MiningSpeed * this.MiningAmount;
    }

    /// <summary>Use this for initialization</summary>
    private void Start() {
        transform.position = this.attachedOreRefinery.transform.position;
        transform.LookAt(this.attachedMine.transform.position);
    }

    /// <summary>Update is called once per frame</summary>
    private void Update() {
        if (this.attachedOreRefinery == null) {
            Destroy(gameObject);
            return;
        }
        this.currentProgressWay += Time.deltaTime;
        if (this.currentProgressWay <= this.MiningSpeed / 2) {
            transform.position = Vector3.Lerp(this.attachedOreRefinery.transform.position, this.attachedMine.transform.position, this.currentProgressWay / (this.MiningSpeed / 2)); // Hinweg
        } else if (this.currentProgressWay <= this.MiningSpeed / 2 + this.LoadingOnSpeed) {
            transform.position = this.attachedMine.transform.position;
        } else if (this.currentProgressWay <= this.MiningSpeed + this.LoadingOnSpeed) {
            transform.position = Vector3.Lerp(this.attachedMine.transform.position, this.attachedOreRefinery.transform.position, (this.currentProgressWay - this.MiningSpeed / 2 - this.LoadingOnSpeed) / (this.MiningSpeed / 2)); // Rückweg
            transform.LookAt(this.attachedOreRefinery.transform.position);
        } else if (this.currentProgressWay <= this.MiningSpeed + this.LoadingOnSpeed + this.LoadingOffSpeed) {
            transform.position = this.attachedOreRefinery.transform.position;
        } else {
            this.currentProgressWay -= this.MiningSpeed + this.LoadingOnSpeed + this.LoadingOffSpeed;
            this.moneyManagement.AddMoney(this.MiningAmount); // Sold ore
            this.floatUpSpawner.GenerateFloatUp(this.MiningAmount, FloatUp.ResourceType.DOLLAR, Camera.main.WorldToScreenPoint(transform.position));
            transform.LookAt(this.attachedMine.transform.position);
        }
    }
}
