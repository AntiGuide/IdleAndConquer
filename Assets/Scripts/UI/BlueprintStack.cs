using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintStack : MonoBehaviour {
    public static int[] NeededBlueprintsLevel = { 1, 2, 8, 16, 32, 48, 64, 80, 96, 112 };

    public int BlueprintCount = 0;

    private int level = 0;

    public enum BLUEPRINT_TYPE {
        UNIT = 0,
        UNIT_GROUP,
        ARMOR_GROUP,
        HARVESTER,
        QUEUE,
        SQUADSLOTS
    }

    public void LevelUp() {
        if (this.level >= BlueprintStack.NeededBlueprintsLevel.Length || this.BlueprintCount < BlueprintStack.NeededBlueprintsLevel[this.level]) {
            return;
        }

        this.BlueprintCount -= BlueprintStack.NeededBlueprintsLevel[this.level];
        this.level++;
    }
}
