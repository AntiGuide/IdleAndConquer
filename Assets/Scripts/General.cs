using System.Collections.Generic;
using UnityEngine;

/// <summary>The class contains all the information and functions a general has</summary>
public class General : MonoBehaviour {
    /// <summary>Chance between 0 and 1 to get removed upon death in a mission</summary>
    private float chanceToPermaDeath;

    /// <summary>The generals portrait</summary>
    private Sprite portrait;

    /// <summary>The generals country</summary>
    private string country;

    /// <summary>The generals name</summary>
    private string generalName;

    /// <summary>The generals wins</summary>
    private int wins;

    /// <summary>The generals loses</summary>
    private int loses;

    /// <summary>The generals active passives</summary>
    private List<Passives> passives;

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
    public int Wins {
        get { return this.wins; }
        set { this.wins = value; }
    }

    /// <summary>Getter/Setter for loses</summary>
    public int Loses {
        get { return this.loses; }
        set { this.loses = value; }
    }

    /// <summary>
    /// Gives the general all important values
    /// </summary>
    /// <param name="chanceToPermaDeath">Chance between 0 and 1 to get removed upon death in a mission</param>
    /// <param name="portrait">The generals portrait</param>
    /// <param name="country">The generals country</param>
    /// <param name="generalName">The generals name</param>
    /// <param name="passives">The generals active passives</param>
    public void InitGeneral(float chanceToPermaDeath, Sprite portrait, string country, string generalName, List<Passives> passives) {
        this.chanceToPermaDeath = chanceToPermaDeath;
        this.portrait = portrait;
        this.country = country;
        this.generalName = generalName;
        this.passives = passives;
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
