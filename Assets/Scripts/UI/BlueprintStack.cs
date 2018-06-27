using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintStack : MonoBehaviour {
    public int BlueprintCount = 0;

    public static int[] NeededBlueprintsLevel = { 1, 2, 8, 16, 32, 48, 64, 80, 96, 112 };

    public enum BLUEPRINT_TYPE {
        UNIT = 0,
        UNIT_GROUP,
        ARMOR_GROUP,
        HARVESTER,
        QUEUE,
        SQUADSLOTS,

    }

    private int Level = 0;

    public void LevelUp() {
        if (Level >= NeededBlueprintsLevel.Length || BlueprintCount < NeededBlueprintsLevel[Level]) {
            return;
        }

        BlueprintCount -= NeededBlueprintsLevel[Level];
        Level++;
    }
}
