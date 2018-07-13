using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSwitcher : MonoBehaviour {
    public GameObject[] Tabs;

    /// <summary>Used to trigger sound</summary>
    public SoundController SoundControll;

    private int aktTabID;

    public void OnClickTab(int tabID) {
        if (tabID != this.aktTabID) {
            Tabs[this.aktTabID].SetActive(false);
            this.aktTabID = tabID;
            Tabs[tabID].SetActive(true);
            SoundControll.StartSound(SoundController.Sounds.MENUE_TAPS);
        }
    }

    private void Start() {
        foreach (GameObject item in Tabs) {
            item.SetActive(false);
        }
        Tabs[0].SetActive(true);
    }
}
