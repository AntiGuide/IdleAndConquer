using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerlevelManagement : MonoBehaviour {

    private static long powerlevel = 0;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        addPowerlevel(1);
    }

    public void addPowerlevel(long powerlevelToAdd) {
        powerlevel = powerlevel + powerlevelToAdd;
        outputPowerlevel(ref powerlevel);
    }

    public bool subPowerlevel(long powerlevelToSub) {
        if (powerlevel >= powerlevelToSub) {
            powerlevel = powerlevel - powerlevelToSub;
            outputPowerlevel(ref powerlevel);
            return true;
        } else {
            return false;
        }
    }

    public void setPowerlevel(long valueToSet) {
        if (valueToSet >= 0) {
            powerlevel = valueToSet;
            outputPowerlevel(ref powerlevel);
        } else {
            throw new ArgumentException("Can not set a negative Dollar Value", "valueToSet");
        }
    }

    private void outputPowerlevel(ref long powerlevel) {
        GetComponent<Text>().text = powerlevel.ToString() + " PL";
    }

}
