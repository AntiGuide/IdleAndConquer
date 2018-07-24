using UnityEngine;

/// <summary>The class contains all the information and functions a general has</summary>
public class General : MonoBehaviour {
    public bool IsSentToMission = false;
    
    /// <summary>Chance between 0 and 1 to get removed upon death in a mission</summary>
    private float chanceToPermaDeath;

    private int generalID = -1;

    public General(int wins, int loses) {
        this.Wins = wins;
        this.Loses = loses;
    }

    /// <summary>Getter/Setter for country</summary>
    public string Country { get; private set; }

    /// <summary>Getter/Setter for generalName</summary>
    public string GeneralName { get; private set; }

    /// <summary>Getter/Setter for wins</summary>
    public int Wins { get; private set; }

    /// <summary>Getter/Setter for loses</summary>
    public int Loses { get; private set; }

    /// <summary>
    /// Gives the general all important values
    /// </summary>
    /// <param name="generalID">The ID of the general in PlayerPref</param>
    /// <param name="chanceToPermaDeath">Chance between 0 and 1 to get removed upon death in a mission</param>
    /// <param name="country">The generals country</param>
    /// <param name="generalName">The generals name</param>
    /// <param name="isSentToMission"></param>
    public void InitGeneral(ref int generalID, float chanceToPermaDeath, string country, string generalName, bool isSentToMission = false) {
        PlayerPrefs.SetFloat("GeneralChanceDeath_" + generalID, chanceToPermaDeath);
        PlayerPrefs.SetString("GeneralCountry_" + generalID, country);
        PlayerPrefs.SetString("GeneralName_" + generalID, generalName);
        PlayerPrefs.SetInt("GeneralWin_" + generalID, 0);
        PlayerPrefs.SetInt("GeneralLose_" + generalID, 0);
        InitGeneral(chanceToPermaDeath, country, generalName, generalID, isSentToMission);
        generalID++;
    }

    /// <summary>
    /// Gives the general all important values
    /// </summary>
    /// <param name="chanceToPermaDeath">Chance between 0 and 1 to get removed upon death in a mission</param>
    /// <param name="country">The generals country</param>
    /// <param name="generalName">The generals name</param>
    /// <param name="isSentToMission"></param>
    public void InitGeneral(float chanceToPermaDeath, string country, string generalName, int generalID, bool isSentToMission = false) {
        this.chanceToPermaDeath = chanceToPermaDeath;
        this.Country = country;
        this.GeneralName = generalName;
        this.generalID = generalID;
        this.IsSentToMission = isSentToMission;
        GeneralManager.AllGenerals.Add(this);
    }

    /// <summary>Calculates wether the general dies (random)</summary>
    /// <returns>If true the general is removed</returns>
    public bool Died() {
        if (!(UnityEngine.Random.value < this.chanceToPermaDeath + Passives.GeneralSurvivability)) return false;
        PlayerPrefs.SetFloat("GeneralChanceDeath_" + this.generalID, -1f);
        Destroy(this);
        return true;
    }
}
