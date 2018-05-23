using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class __SceneSwitch : MonoBehaviour {

    private static GameObject canvas;
    private static bool canvasHidden = false;

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas");

    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.sceneCount <= 1 && canvasHidden) {
            canvas.SetActive(true);
            canvasHidden = false;
        }
    }

    public static void MissionMapLoad() {
        SceneManager.LoadScene("MissionMap", LoadSceneMode.Additive);
        canvasHidden = true;
        canvas.SetActive(false);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("MissionMap"));
        
        //SceneManager.LoadScene("MissionMap", LoadSceneMode.Single);
        //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
    }

}
