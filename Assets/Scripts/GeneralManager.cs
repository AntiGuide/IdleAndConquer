using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour {

    public float genMinChanceToPermaDeath;
    public float genMaxChanceToPermaDeath;
    public string[] countrys;
    public string[] names;
    public GameObject generalList;

    private int generalID = 0;

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
                generalID = tmpGeneralID;
                break;
            }
            GeneralButton attachedButton = Instantiate(Resources.Load<GameObject>("GeneralButton"), generalList.transform).GetComponent<GeneralButton>();
            attachedButton.setTexts(country, generalName, wins + Environment.NewLine + "-" + Environment.NewLine + loses);

            General ret = attachedButton.gameObject.GetComponent<General>();
            ret.InitGeneral(chanceDeath, null, country, generalName, null);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerPrefs.DeleteAll();
        }
	}

    public void GenerateGeneral() {
        // Level 1 Choose 1 Skill

        System.Random rnd = new System.Random();
        int nameID = rnd.Next(0, names.Length);
        int countryID = rnd.Next(0, countrys.Length);
        float aktChanceToPermaDeath = UnityEngine.Random.Range(genMinChanceToPermaDeath, genMaxChanceToPermaDeath);

        GeneralButton attachedButton = Instantiate(Resources.Load<GameObject>("GeneralButton"), generalList.transform).GetComponent<GeneralButton>();
        attachedButton.setTexts(countrys[countryID], names[nameID], 0 + Environment.NewLine + "-" + Environment.NewLine + 0);
        PlayerPrefs.SetFloat("GeneralChanceDeath_" + generalID, aktChanceToPermaDeath);
        PlayerPrefs.SetString("GeneralCountry_" + generalID, countrys[countryID]);
        PlayerPrefs.SetString("GeneralName_" + generalID, names[nameID]);
        PlayerPrefs.SetInt("GeneralWin_" + generalID, 0);
        PlayerPrefs.SetInt("GeneralLose_" + generalID, 0);
        generalID++;

        General ret = attachedButton.gameObject.GetComponent<General>();
        ret.InitGeneral(aktChanceToPermaDeath, null, countrys[countryID], names[nameID], null);
    }
}
