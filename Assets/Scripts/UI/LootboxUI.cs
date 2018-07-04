using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootboxUI : MonoBehaviour {
    public Text PlayerRewards;
    public Text PlayerCommunication;
    public Image LootBoxOpened;
    public GameObject OKButton;

    public void ClickCase() {
        PlayerRewards.gameObject.SetActive(true);
        LootBoxOpened.gameObject.SetActive(true);
        PlayerRewards.text = "2x Tank 1 Blueprints";
        PlayerCommunication.gameObject.SetActive(false);
        OKButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClickOK() {
        Destroy(transform.parent.parent.gameObject);
    }
}
