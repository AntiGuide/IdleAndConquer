using UnityEngine;

/// <summary>The class contains all the information and functions a general has</summary>
public class General : MonoBehaviour {
    public bool IsSentToMission = false;
    
    /// <summary>Chance between 0 and 1 to get removed upon death in a mission</summary>
    private float chanceToPermaDeath;

    /// <summary>The generals portrait</summary>
    private Sprite portrait;

    /// <summary>The generals country</summary>
    private string country;

    /// <summary>The generals name</summary>
    private string generalName;

    /// <summary>Getter/Setter for chanceToPermaDeath</summary>
    public float ChanceToPermaDeath {
        get { return this.chanceToPermaDeath; }
        set { this.chanceToPermaDeath = value; }
    }

    /// <summary>Getter/Setter for portrait</summary>
    public Sprite Portrait {
        get { return this.portrait; }
        set { this.portrait = value; }
    }

    /// <summary>Getter/Setter for country</summary>
    public string Country {
        get { return this.country; }
        set { this.country = value; }
    }

    /// <summary>Getter/Setter for generalName</summary>
    public string GeneralName {
        get { return this.generalName; }
        set { this.generalName = value; }
    }

    /// <summary>Getter/Setter for wins</summary>
    public int Wins { get; set; }

    /// <summary>Getter/Setter for loses</summary>
    public int Loses { get; set; }

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
        this.country = country;
        this.generalName = generalName;
        this.IsSentToMission = isSentToMission;
    }

    /// <summary>Calculates wether the general dies (random)</summary>
    /// <returns>If true the general is removed</returns>
    public bool Died() {
        if (UnityEngine.Random.value < this.chanceToPermaDeath + Passives.GeneralSurvivability) {
            return true;
        } else {
            return false;
        }
    }
}
