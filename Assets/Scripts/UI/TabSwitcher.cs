using UnityEngine;

public class TabSwitcher : MonoBehaviour {
    public GameObject[] Tabs;

    /// <summary>Used to trigger sound</summary>
    public SoundController SoundControll;

    private int aktTabID;

    public void OnClickTab(int tabID) {
        if (tabID != this.aktTabID) {
            this.Tabs[this.aktTabID].SetActive(false);
            this.aktTabID = tabID;
            this.Tabs[tabID].SetActive(true);
            this.SoundControll.StartSound(SoundController.Sounds.MENUE_TAPS);
        }
    }

    private void Start() {
        foreach (GameObject item in this.Tabs) {
            item.SetActive(false);
        }

        this.Tabs[0].SetActive(true);
    }
}
