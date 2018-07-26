using UnityEngine;
using UnityEngine.UI;

public class LootboxUI : MonoBehaviour {
    public Text PlayerRewards;
    public Text PlayerCommunication;
    public Image LootBoxOpened;
    public GameObject OKButton;
    public int RewardCount;
    public string RewardUnitName;
    private BlueprintManager blueprintMan;

    public void ClickCase() {
        this.PlayerRewards.gameObject.SetActive(true);
        this.LootBoxOpened.gameObject.SetActive(true);
        this.PlayerRewards.text = this.RewardCount + "x " + this.RewardUnitName + " Blueprints";
        this.PlayerCommunication.gameObject.SetActive(false);
        this.OKButton.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ClickOK() {
        this.blueprintMan.SearchUnitNameAddBlueprint(this.RewardUnitName, this.RewardCount);
        UnityEngine.Object.Destroy(transform.parent.parent.gameObject);
    }

    public void ClickOKDaily() {
        this.blueprintMan.SearchUnitNameAddBlueprint(this.RewardUnitName, this.RewardCount);
        TimedResearchReward.BeginNewTimerNow = true;
        UnityEngine.Object.Destroy(transform.parent.parent.gameObject);
    }

    private void Start() {
        this.blueprintMan = GameObject.Find("/Main/Canvas/MainMenue/MenueResearch").GetComponent<BlueprintManager>();
    }
}
