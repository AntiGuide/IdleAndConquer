using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintStack : MonoBehaviour {
    public static int[] NeededBlueprintsLevel = { 1, 2, 8, 16, 32, 48, 64, 80, 96, 112 };

    public static Unit[] Units;

    public CreateAndOrderUnit CreateAndOrderUnitStack;

    public int BlueprintCount = 0;

    public BlueprintType BlueprintTypeStack;

    public Image BuildingOverlay;

    public BaseSwitcher BaseSwitch;

    private static float[] buildTime = { 60f, 120f, 480f, 960f, 1920f, 2280f, 3840f, 4800f, 5760f, 6720f };

    private int level = 0;

    private int buildingTowardsLevel = 0;

    private Text blueprintCountText;

    private Text levelText;

    public float Buildtime {
        get { return buildTime[this.level]; }
    }

    public enum BlueprintType {
        UNIT = 0,
        UNIT_GROUP,
        ARMOR_GROUP,
        HARVESTER,
        QUEUE,
        SQUADSLOTS,
        CATCHING_JETS,
        FIND_THE_BOX
    }

    public void LevelUp() {
        if (this.buildingTowardsLevel >= BlueprintStack.NeededBlueprintsLevel.Length || this.BlueprintCount < BlueprintStack.NeededBlueprintsLevel[this.buildingTowardsLevel]) {
            return;
        }
        this.BlueprintCount -= BlueprintStack.NeededBlueprintsLevel[this.buildingTowardsLevel];
        buildingTowardsLevel++;
        blueprintCountText.text = BlueprintCount + "/" + BlueprintStack.NeededBlueprintsLevel[buildingTowardsLevel];
        this.BaseSwitch.GetResearchQueue().AddToQueue(this);
    }

    private void Update() {
        if (UnityEngine.Random.Range(0f, 1f) < 0.001f) {
            this.AddBlueprint();
        }
    }

    internal void PerformLevelUp() {
        this.level++;
        levelText.text = "Level " + level.ToString();
        
        switch (BlueprintTypeStack) {
            case BlueprintType.UNIT:
                CreateAndOrderUnitStack.AttachedUnit.LevelUp();
                break;
            case BlueprintType.UNIT_GROUP:
                break;
            case BlueprintType.ARMOR_GROUP:
                break;
            case BlueprintType.HARVESTER:
                break;
            case BlueprintType.QUEUE:
                break;
            case BlueprintType.SQUADSLOTS:
                break;
            case BlueprintType.CATCHING_JETS:
                break;
            case BlueprintType.FIND_THE_BOX:
                break;
            default:
                break;
        }
    }

    private void Start() {

        Text t = transform.Find("Text").GetComponent<Text>();
        if (CreateAndOrderUnitStack.AttachedUnit != null) {
            t.text = CreateAndOrderUnitStack.AttachedUnit.UnitName;
        } else {
            Debug.Log(gameObject.name = "Fail");
        }

        levelText = transform.Find("CountText").GetComponent<Text>();
        levelText.text = "Level " + level.ToString();
        blueprintCountText = transform.Find("BuildingCountText").GetComponent<Text>();
        blueprintCountText.text = BlueprintCount + "/" + BlueprintStack.NeededBlueprintsLevel[buildingTowardsLevel];
    }

    public void AddBlueprint() {
        BlueprintCount++;
        blueprintCountText.text = BlueprintCount + "/" + BlueprintStack.NeededBlueprintsLevel[buildingTowardsLevel];
    }
}
