using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxStackManager : MonoBehaviour {
    [SerializeField] private RotateAndPulsate[] rotateAndPulsate;

    public enum LootboxType {
        LEATHER = 0,
        METAL,
        GOLD
    }

    public void AddLootbox(LootboxType lootboxType) {
        rotateAndPulsate[(int) lootboxType].AddBox();
    }

    private void Start() {
        foreach (var v in rotateAndPulsate) {
            v.StartUp();
        }
    }
}
