using UnityEngine;

/// <summary> 
/// Class to hold the details of a mission 
/// </summary> 
public class MissionDetails : MonoBehaviour {
    /// <summary>The name of the mission, e.g. Beat Juri</summary> 
    public string MissionName;
    public MainMenueController MainMenueControll;
    public float MissionTime;
    public long MissionMoneyReward = 1000;
    public long MissionRenownReward = 50;
    public long MissionVirtualReward = 10;
    public long MissionBlueprintReward = 1;
    public UIInteraction UIInteractions;
    public MissionManager MissionMan;
    public string EnemyGeneral;
    public GameObject MissionDetailsWindow;

    public void OnClick() {
        this.MissionDetailsWindow.SetActive(true);
        this.MissionMan.GenerateMission(this, this.UIInteractions, this.MainMenueControll);
        this.MainMenueControll.ToggleMenue(1);
    }
}