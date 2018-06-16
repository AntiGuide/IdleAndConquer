using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class __SceneSwitch : MonoBehaviour {
    public GameObject timePrefab;

    public GameObject MissionMapContainer;

    public GameObject MainContainer;

    private static bool canvasHidden = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void MissionMapLoad() {
        MainContainer.SetActive(false);
        MissionMapContainer.SetActive(true);
    }

    public void MainLoad() {
        MissionMapContainer.SetActive(false);
        MainContainer.SetActive(true);
    }

}
