using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootboxUI : MonoBehaviour {
    public Text PlayerRewards;
    public Text PlayerCommunication;
    public Image LootBoxOpened;
    public GameObject OKButton;
    private BlueprintManager BlueprintMan;

    public void ClickCase() {
        PlayerRewards.gameObject.SetActive(true);
        LootBoxOpened.gameObject.SetActive(true);
        PlayerRewards.text = "2x Tank 1 Blueprints";
        PlayerCommunication.gameObject.SetActive(false);
        OKButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClickOK() {
        BlueprintMan.SearchUnitNameAddBlueprint("Tank 1", 2);
        Destroy(transform.parent.parent.gameObject);
    }

    private void Start() {
        BlueprintMan = GameObject.Find("/Main/Canvas/MainMenue/MenueResearch").GetComponent<BlueprintManager>();
    }
}
