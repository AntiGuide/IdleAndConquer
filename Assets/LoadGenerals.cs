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
    void Start() {
        for (int tmpGeneralID = 0; true; tmpGeneralID++) {
            this.chanceDeath = PlayerPrefs.GetFloat("GeneralChanceDeath_" + tmpGeneralID, -1f);
            this.country = PlayerPrefs.GetString("GeneralCountry_" + tmpGeneralID, string.Empty);
            this.generalName = PlayerPrefs.GetString("GeneralName_" + tmpGeneralID, string.Empty);
            this.wins = PlayerPrefs.GetInt("GeneralWin_" + tmpGeneralID, -1);
            this.loses = PlayerPrefs.GetInt("GeneralLose_" + tmpGeneralID, -1);
            if (this.chanceDeath < 0f) {
                break;
            }

            GeneralButton attachedButton = Instantiate(this.generalButtonMissionMap, transform).GetComponent<GeneralButton>();
            attachedButton.SetTexts(this.country, this.generalName, this.wins + Environment.NewLine + "-" + Environment.NewLine + this.loses);
            General ret = attachedButton.gameObject.GetComponent<General>();
            ret.InitGeneral(this.chanceDeath, null, this.country, this.generalName, null);
        }
    }
}
