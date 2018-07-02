using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSwitcher : MonoBehaviour {
    public GameObject[] tabs;
    private int aktTabID;

    public void OnClickTab(int tabID) {
        if (tabID != aktTabID) {
            tabs[aktTabID].SetActive(false);
            aktTabID = tabID;
            tabs[tabID].SetActive(true);
        }
    }
}
