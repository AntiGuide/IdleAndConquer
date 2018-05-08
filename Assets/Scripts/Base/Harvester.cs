using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour {
    //Operates automatically. Refinery level equals number of Havesters operating. Drives to Mine to harvest and then unloads at Refinery to generate income. $ is the only universal Resource (so far). Mine --> $
    public float miningSpeed = 20.0f;//Every 20 seconds
    public int miningAmount = 50;//50 $

    //public Mine attachedMine;
    public OreRefinery attachedOreRefinery;
    public MoneyManagement moneyManagement;

    private float currentProgressWay;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentProgressWay += Time.deltaTime;
        if (currentProgressWay <= (miningSpeed / 2)) {
            //transform.position = Vector3.Lerp(attachedOreRefinery.transform.position, attachedMine.transform.position, currentProgressWay / (miningSpeed / 2)); //Hinweg
        } else if (currentProgressWay <= miningSpeed) {
            //transform.position = Vector3.Lerp(attachedOreRefinery.transform.position, attachedMine.transform.position, currentProgressWay / miningSpeed); //Rückweg
            //Has ore loaded
        }else{
            currentProgressWay -= miningSpeed;
            moneyManagement.addMoney(miningAmount);//Sold ore
        }
    }
}
