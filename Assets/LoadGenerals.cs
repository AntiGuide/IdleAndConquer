using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGenerals : MonoBehaviour {

    public GameObject generalButtonMissionMap;

    private float chanceDeath;
    private string country;
    private string generalName;
    private int wins;
    private int loses;

    // Use this for initialization
    void Start () {
        for (int tmpGeneralID = 0; true; tmpGeneralID++) {


            chanceDeath = PlayerPrefs.GetFloat("GeneralChanceDeath_" + tmpGeneralID, -1f);
            country = PlayerPrefs.GetString("GeneralCountry_" + tmpGeneralID, "");
            generalName = PlayerPrefs.GetString("GeneralName_" + tmpGeneralID, "");
            wins = PlayerPrefs.GetInt("GeneralWin_" + tmpGeneralID, -1);
            loses = PlayerPrefs.GetInt("GeneralLose_" + tmpGeneralID, -1);
            if (chanceDeath < 0f) {
                break;
            }
            GeneralButton attachedButton = Instantiate(generalButtonMissionMap, transform).GetComponent<GeneralButton>();
            attachedButton.SetTexts(country, generalName, wins + Environment.NewLine + "-" + Environment.NewLine + loses);

            General ret = attachedButton.gameObject.GetComponent<General>();
            ret.InitGeneral(chanceDeath, null, country, generalName, null);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
