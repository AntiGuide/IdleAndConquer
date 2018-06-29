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

    private int level = 0;

    private Text blueprintCountText;

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
        if (this.level >= BlueprintStack.NeededBlueprintsLevel.Length || this.BlueprintCount < BlueprintStack.NeededBlueprintsLevel[this.level]) {
            return;
        }

        this.BlueprintCount -= BlueprintStack.NeededBlueprintsLevel[this.level];
        this.level++;
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

    private void Update() {
        if (UnityEngine.Random.Range(0f, 1f) < 0.001f) {
            this.AddBlueprint();
        }
    }

    private void Start() {
        Text t = transform.Find("Text").GetComponent<Text>();
        if (CreateAndOrderUnitStack.AttachedUnit != null) {
            t.text = CreateAndOrderUnitStack.AttachedUnit.UnitName;
        } else {
            Debug.Log(gameObject.name = "Fail");
        }

        transform.Find("CountText").GetComponent<Text>().text = "Level " + level.ToString();
        blueprintCountText = transform.Find("BuildingCountText").GetComponent<Text>();
        blueprintCountText.text = BlueprintCount + "/" + BlueprintStack.NeededBlueprintsLevel[this.level];
    }

    public void AddBlueprint() {
        BlueprintCount++;
        blueprintCountText.text = BlueprintCount + "/" + BlueprintStack.NeededBlueprintsLevel[this.level];
    }
}
