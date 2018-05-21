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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateGeneral() {
        // Level 1 Choose 1 Skill

        System.Random rnd = new System.Random();
        int nameID = rnd.Next(0, names.Length);
        int countryID = rnd.Next(0, countrys.Length);
        float aktChanceToPermaDeath = UnityEngine.Random.Range(genMinChanceToPermaDeath, genMaxChanceToPermaDeath);

        GeneralButton attachedButton = Instantiate(Resources.Load<GameObject>("GeneralButton"), generalList.transform).GetComponent<GeneralButton>();
        attachedButton.setTexts(countrys[countryID], names[nameID], 0 + Environment.NewLine + "-" + Environment.NewLine + 0);

        General ret = attachedButton.gameObject.GetComponent<General>();
        ret.InitGeneral(aktChanceToPermaDeath, null, countrys[countryID], names[nameID], null);
    }
}
