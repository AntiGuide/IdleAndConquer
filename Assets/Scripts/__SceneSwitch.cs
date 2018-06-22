using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class __SceneSwitch : MonoBehaviour {
    public GameObject timePrefab;
    public GameObject MissionMapContainer;
    public GameObject MainContainer;
    private static bool canvasHidden = false;

    public void MissionMapLoad() {
        this.MainContainer.SetActive(false);
        this.MissionMapContainer.SetActive(true);
    }

    public void MainLoad() {
        this.MissionMapContainer.SetActive(false);
        this.MainContainer.SetActive(true);
    }
}
