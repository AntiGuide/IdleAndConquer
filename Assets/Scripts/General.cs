using UnityEngine;

/// <summary>The class contains all the information and functions a general has</summary>
public class General : MonoBehaviour {
    public bool IsSentToMission = false;
    
    /// <summary>Chance between 0 and 1 to get removed upon death in a mission</summary>
    private float chanceToPermaDeath;

    /// <summary>The generals portrait</summary>
    private Sprite portrait;

    public General(int wins, int loses) {
        Wins = wins;
        Loses = loses;
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
    /// <param name="chanceToPermaDeath">Chance between 0 and 1 to get removed upon death in a mission</param>
    /// <param name="portrait">The generals portrait</param>
    /// <param name="country">The generals country</param>
    /// <param name="generalName">The generals name</param>
    /// <param name="isSentToMission"></param>
    public void InitGeneral(float chanceToPermaDeath, Sprite portrait, string country, string generalName,
        bool isSentToMission = false) {
        this.chanceToPermaDeath = chanceToPermaDeath;
        this.portrait = portrait;
        this.Country = country;
        this.GeneralName = generalName;
        this.IsSentToMission = isSentToMission;
    }

    /// <summary>Calculates wether the general dies (random)</summary>
    /// <returns>If true the general is removed</returns>
    public bool Died() {
        return UnityEngine.Random.value < this.chanceToPermaDeath + Passives.GeneralSurvivability;
    }
}
