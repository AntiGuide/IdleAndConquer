using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMission : MonoBehaviour {

    

    public void OnClick() {
        PlayerPrefs.SetFloat("Mission", 60.0f);
        SceneManager.UnloadSceneAsync("MissionMap");
        //TODO Start mission
    }

}
