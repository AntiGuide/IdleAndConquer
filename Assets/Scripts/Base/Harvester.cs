using UnityEngine;

public class Harvester : MonoBehaviour {
    //Operates automatically. Refinery level equals number of Havesters operating. Drives to Mine to harvest and then unloads at Refinery to generate income. $ is the only universal Resource (so far). Mine --> $
    public float miningSpeed = 20.0f;//Every 20 seconds
    public int miningAmount = 50;//50 $
    
    private Mine attachedMine;
    private OreRefinery attachedOreRefinery;
    private MoneyManagement moneyManagement;
    private float currentProgressWay;
    private FloatUpSpawner floatUpSpawner;

    public void Initialize(OreRefinery attachedOreRefinery, Mine attachedMine,ref MoneyManagement moneyManagement, FloatUpSpawner floatUpSpawner) {
        this.attachedOreRefinery = attachedOreRefinery;
        this.attachedMine = attachedMine;
        this.moneyManagement = moneyManagement;
        this.floatUpSpawner = floatUpSpawner;
    }

    // Use this for initialization
    void Start () {
        transform.position = attachedOreRefinery.transform.position;
        transform.LookAt(attachedMine.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        currentProgressWay += Time.deltaTime;
        if (currentProgressWay <= (miningSpeed / 2)) {
            transform.position = Vector3.Lerp(attachedOreRefinery.transform.position, attachedMine.transform.position, currentProgressWay / (miningSpeed / 2)); //Hinweg
        } else if (currentProgressWay <= miningSpeed) {
            transform.position = Vector3.Lerp(attachedMine.transform.position, attachedOreRefinery.transform.position, (currentProgressWay - (miningSpeed / 2)) / (miningSpeed / 2)); //Rückweg
            //Has ore loaded
            transform.LookAt(attachedOreRefinery.transform.position);
        } else{
            currentProgressWay -= miningSpeed;
            moneyManagement.addMoney(miningAmount);//Sold ore
            floatUpSpawner.GenerateFloatUp(miningAmount, FloatUp.ResourceType.DOLLAR, Camera.main.WorldToScreenPoint(transform.position));
            transform.LookAt(attachedMine.transform.position);
        }
    }
}
