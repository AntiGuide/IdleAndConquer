using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootboxUI : MonoBehaviour {
    public Text PlayerRewards;
    public Text PlayerCommunication;
    public Image LootBoxOpened;
    public GameObject OKButton;
    public int RewardCount;
    public string RewardUnitName;
    private BlueprintManager BlueprintMan;


    public void ClickCase() {
        PlayerRewards.gameObject.SetActive(true);
        LootBoxOpened.gameObject.SetActive(true);
        PlayerRewards.text = RewardCount + "x " + RewardUnitName + " Blueprints";
        PlayerCommunication.gameObject.SetActive(false);
        OKButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ClickOK() {
        BlueprintMan.SearchUnitNameAddBlueprint(RewardUnitName, RewardCount);
        Destroy(transform.parent.parent.gameObject);
    }

    private void Start() {
        BlueprintMan = GameObject.Find("/Main/Canvas/MainMenue/MenueResearch").GetComponent<BlueprintManager>();
    }
}
