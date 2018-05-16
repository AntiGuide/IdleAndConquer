using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General {

    public static float genMinChanceToPermaDeath;
    public static float genMaxChanceToPermaDeath;

    private float chanceToPermaDeath;
    private Sprite portrait;
    private Country country;
    private string name;
    private int wins;
    private int loses;
    private List<Passives> passives;
    
    public enum Country
    {
        GERMANY = 1
    }

    private General(float chanceToPermaDeath, Sprite portrait, Country country, string name, List<Passives> passives) {
        this.chanceToPermaDeath = chanceToPermaDeath;
        this.portrait = portrait;
        this.country = country;
        this.name = name;
        this.passives = passives;
    }

    public bool Died() {
        if (Random.value < chanceToPermaDeath) {
            return true;
        } else {
            return false;
        }
    }

    public static General GenerateGeneral() {
        float aktChanceToPermaDeath = Random.Range(genMinChanceToPermaDeath, genMaxChanceToPermaDeath);
        General ret = new General(aktChanceToPermaDeath,null,Country.GERMANY, null,null);
        return ret;
    }

}
