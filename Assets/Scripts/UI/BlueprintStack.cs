using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintStack : MonoBehaviour {
    public int BlueprintCount = 0;

    public static int[] NeededBlueprintsLevel = { 1, 2, 8, 16, 32, 48, 64, 80, 96, 112 };

    private int Level = 0;

    public void LevelUp() {
        if (Level >= NeededBlueprintsLevel.Length || BlueprintCount < NeededBlueprintsLevel[Level]) {
            return;
        }

        BlueprintCount -= NeededBlueprintsLevel[Level];
        Level++;
    }
}
