using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralButton : MonoBehaviour {

    public Image image;
    public Text country;
    public Text generalName;
    public Text winLoseHistory;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setTexts(string country, string generalName, string winLoseHistory) {
        this.country.text = country;
        this.generalName.text = generalName;
        this.winLoseHistory.text = winLoseHistory;
    }
}
