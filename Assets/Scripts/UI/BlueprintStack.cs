using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintStack : MonoBehaviour {
    public static int[] NeededBlueprintsLevel = { 1, 2, 8, 16, 32, 48, 64, 80, 96, 112 };

    public static Unit[] Units;

    public BlueprintManager BlueprintMan;

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
        this.buildingTowardsLevel++;
        this.blueprintCountText.text = this.BlueprintCount + "/" + BlueprintStack.NeededBlueprintsLevel[this.buildingTowardsLevel];
        this.BaseSwitch.GetResearchQueue().AddToQueue(this);
    }

    private void Update() {
    }

    internal void PerformLevelUp() {
        this.level++;
        this.levelText.text = "Level " + this.level.ToString();
        
        switch (this.BlueprintTypeStack) {
            case BlueprintType.UNIT:
                this.CreateAndOrderUnitStack.AttachedUnit.LevelUp();
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

    private void Awake() {

        Text t = transform.Find("Text").GetComponent<Text>();
        if (this.CreateAndOrderUnitStack.AttachedUnit != null) {
            t.text = this.CreateAndOrderUnitStack.AttachedUnit.UnitName;
        } else {
            Debug.Log(gameObject.name = "Fail");
        }

        this.levelText = transform.Find("CountText").GetComponent<Text>();
        this.levelText.text = "Level " + this.level.ToString();
        this.blueprintCountText = transform.Find("BuildingCountText").GetComponent<Text>();
        this.blueprintCountText.text = this.BlueprintCount + "/" + BlueprintStack.NeededBlueprintsLevel[this.buildingTowardsLevel];

        this.BlueprintMan.BlueprintStacks.Add(this);
    }

    public void AddBlueprint(int count = 1) {
        this.BlueprintCount += count;
        this.blueprintCountText.text = this.BlueprintCount + "/" + BlueprintStack.NeededBlueprintsLevel[this.buildingTowardsLevel];
    }
}
