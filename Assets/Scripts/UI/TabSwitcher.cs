using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSwitcher : MonoBehaviour {
    public GameObject[] Tabs;
    private int aktTabID;

    public void OnClickTab(int tabID) {
        if (tabID != aktTabID) {
            Tabs[aktTabID].SetActive(false);
            aktTabID = tabID;
            Tabs[tabID].SetActive(true);
        }
    }

    private void Start() {
        foreach (GameObject item in Tabs) {
            item.SetActive(false);
        }
        Tabs[0].SetActive(true);
    }
}
