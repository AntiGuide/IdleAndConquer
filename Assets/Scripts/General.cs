using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour{
    private float chanceToPermaDeath;
    private Sprite portrait;
    private string country;
    private string generalName;
    private int wins;
    private int loses;
    private List<Passives> passives;

    public float ChanceToPermaDeath {
        get {
            return chanceToPermaDeath;
        }

        set {
            chanceToPermaDeath = value;
        }
    }

    public Sprite Portrait {
        get {
            return portrait;
        }

        set {
            portrait = value;
        }
    }

    public string Country {
        get {
            return country;
        }

        set {
            country = value;
        }
    }

    public string GeneralName {
        get {
            return generalName;
        }

        set {
            generalName = value;
        }
    }

    public int Wins {
        get {
            return wins;
        }

        set {
            wins = value;
        }
    }

    public int Loses {
        get {
            return loses;
        }

        set {
            loses = value;
        }
    }

    public void InitGeneral(float chanceToPermaDeath, Sprite portrait, string country, string generalName, List<Passives> passives) {
        this.chanceToPermaDeath = chanceToPermaDeath;
        this.portrait = portrait;
        this.country = country;
        this.generalName = generalName;
        this.passives = passives;
    }

    public bool Died() {
        if (UnityEngine.Random.value < chanceToPermaDeath + Passives.GeneralSurvivability) {
            return true;
        } else {
            return false;
        }
    }

    

}
