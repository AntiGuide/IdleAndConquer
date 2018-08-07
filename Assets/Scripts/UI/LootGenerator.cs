using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootGenerator : MonoBehaviour {

    public BlueprintManager BlueprintMan;

    public Text Description;

    private Vector2[,] amounts = new Vector2[3,4];

    private void Start() {
        amounts[0, 0] = new Vector2(1, 10);
        amounts[0, 1] = new Vector2(1, 2);
        amounts[0, 2] = new Vector2(1, 1);
        amounts[0, 3] = new Vector2(0, 0);

        amounts[1, 0] = new Vector2(10, 20);
        amounts[1, 1] = new Vector2(2, 4);
        amounts[1, 2] = new Vector2(1, 2);
        amounts[1, 3] = new Vector2(1, 1);

        amounts[2, 0] = new Vector2(20, 50);
        amounts[2, 1] = new Vector2(4, 10);
        amounts[2, 2] = new Vector2(2, 5);
        amounts[2, 3] = new Vector2(1, 3);
    }

    public void InstantiateLootbox(LootBoxStackManager.LootboxType type) {
        var typeChance = UnityEngine.Random.value;
        // Common
        var stackOne = BlueprintMan.GetStackAndAdd(Random.Range((int)amounts[(int)type, 0].x, (int)this.amounts[(int)type, 0].y), BlueprintStack.BlueprintRarityType.COMMON);
        BlueprintStack stackTwo;
        if (typeChance < 0.8) {
            // Rare
            stackTwo = BlueprintMan.GetStackAndAdd(Random.Range((int)amounts[(int)type, 1].x, (int)amounts[(int)type, 1].y), BlueprintStack.BlueprintRarityType.RARE);
        } else if (typeChance < 0.95 || amounts[(int)type, 3] == Vector2.zero) {
            // Epic
            stackTwo = BlueprintMan.GetStackAndAdd(Random.Range((int)amounts[(int)type, 2].x, (int)this.amounts[(int)type, 2].y), BlueprintStack.BlueprintRarityType.EPIC);
        } else {
            // Legendary
            stackTwo = BlueprintMan.GetStackAndAdd(Random.Range((int)amounts[(int)type, 3].x, (int)this.amounts[(int)type, 3].y), BlueprintStack.BlueprintRarityType.LEGENDARY);
        }

        Description.text = stackOne.LastAmountAdded + " x " + stackOne.Description() + System.Environment.NewLine +
                           stackTwo.LastAmountAdded + " x " + stackTwo.Description();
    }
}
