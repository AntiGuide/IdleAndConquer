using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to manage the generation of generals
/// </summary>
public class GeneralManager : MonoBehaviour {
    /// <summary>The minimum generatable chance of a general to die permanently</summary>
    public float GenMinChanceToPermaDeath;

    /// <summary>The maximum generatable chance of a general to die permanently</summary>
    public float GenMaxChanceToPermaDeath;

    /// <summary>Array of countrys to generate generals from</summary>
    public string[] Countrys;

    /// <summary>Array of names to generate generals from</summary>
    public string[] Names;

    /// <summary>Parent for all general buttons</summary>
    public GameObject GeneralList;

    public static List<General> AllGenerals = new List<General>();

    /// <summary>The highest general ID at the moment</summary>
    private int generalID = 0;

    /// <summary>Chance of permanent death of a single general</summary>
    private float chanceDeath;

    /// <summary>Country of a single general</summary>
    private string country;

    /// <summary>The name of a single general</summary>
    private string generalName;

    /// <summary>The wins of this general</summary>
    private int wins;

    /// <summary>The loses of this general</summary>
    private int loses;

    /// <summary>
    /// This method generates a general from the preset values
    /// </summary>
    public void GenerateGeneral() {
        // TODO Level 1 Choose 1 Skill
        System.Random rnd = new System.Random();
        int nameID = rnd.Next(0, this.Names.Length);
        int countryID = rnd.Next(0, this.Countrys.Length);
        float aktChanceToPermaDeath = UnityEngine.Random.Range(this.GenMinChanceToPermaDeath, this.GenMaxChanceToPermaDeath);

        GeneralButton attachedButton = Instantiate(Resources.Load<GameObject>("GeneralButton"), this.GeneralList.transform).GetComponent<GeneralButton>();
        attachedButton.SetTexts(this.Countrys[countryID], this.Names[nameID], 0 + Environment.NewLine + "-" + Environment.NewLine + 0);
        PlayerPrefs.SetFloat("GeneralChanceDeath_" + this.generalID, aktChanceToPermaDeath);
        PlayerPrefs.SetString("GeneralCountry_" + this.generalID, this.Countrys[countryID]);
        PlayerPrefs.SetString("GeneralName_" + this.generalID, this.Names[nameID]);
        PlayerPrefs.SetInt("GeneralWin_" + this.generalID, 0);
        PlayerPrefs.SetInt("GeneralLose_" + this.generalID, 0);
        this.generalID++;

        General ret = attachedButton.gameObject.GetComponent<General>();
        ret.InitGeneral(aktChanceToPermaDeath, null, this.Countrys[countryID], this.Names[nameID], null);
        GeneralManager.AllGenerals.Add(ret);
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start() {
        for (int tmpGeneralID = 0; true; tmpGeneralID++) {
            this.chanceDeath = PlayerPrefs.GetFloat("GeneralChanceDeath_" + tmpGeneralID, -1f);
            this.country = PlayerPrefs.GetString("GeneralCountry_" + tmpGeneralID, string.Empty);
            this.generalName = PlayerPrefs.GetString("GeneralName_" + tmpGeneralID, string.Empty);
            this.wins = PlayerPrefs.GetInt("GeneralWin_" + tmpGeneralID, -1);
            this.loses = PlayerPrefs.GetInt("GeneralLose_" + tmpGeneralID, -1);
            if (this.chanceDeath < 0f) {
                this.generalID = tmpGeneralID;
                break;
            }

            GeneralButton attachedButton = Instantiate(Resources.Load<GameObject>("GeneralButton"), this.GeneralList.transform).GetComponent<GeneralButton>();
            attachedButton.SetTexts(this.country, this.generalName, this.wins + Environment.NewLine + "-" + Environment.NewLine + this.loses);

            General ret = attachedButton.gameObject.GetComponent<General>();
            ret.InitGeneral(this.chanceDeath, null, this.country, this.generalName, null);
            GeneralManager.AllGenerals.Add(ret);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerPrefs.DeleteAll();
        }
    }
}
