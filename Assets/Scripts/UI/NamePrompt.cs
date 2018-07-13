﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamePrompt : MonoBehaviour {
    public GameObject InputNamePrompt;
    public GameObject ChooseFraction;
    public GameObject OKButton;
    public GameObject Name;

    public void OnOKClick() {
        this.Name.SetActive(false);
        this.ChooseFraction.SetActive(true);
    }

    public void OnFactionChoose() {
        this.OKButton.SetActive(true);
    }

    public void OnFactionChooseOK() {
        UnityEngine.Object.Destroy(this.InputNamePrompt);
    }
}
