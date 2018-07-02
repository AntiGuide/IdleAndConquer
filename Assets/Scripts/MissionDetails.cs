using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary> 
/// Class to hold the details of a mission 
/// </summary> 
public class MissionDetails : MonoBehaviour {
    /// <summary>The name of the mission, e.g. Beat Juri</summary> 
    public string MissionName;
    public MainMenueController MainMenueControll;
    public float MissionTime;
    public long MissionMoneyReward = 1000;
    public UIInteraction UIInteractions;

    public void OnClick() {
        Debug.Log(MissionName);
        MissionManager.GenerateMission(this, UIInteractions, MainMenueControll);
        MainMenueControll.ToggleMenue(1);
    }
}