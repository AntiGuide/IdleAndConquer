
using System.Collections.Generic;
using UnityEngine;

public class BlueprintManager : MonoBehaviour {
    public List<BlueprintStack> BlueprintStacks = new List<BlueprintStack>();

    public void SearchUnitAddBlueprint(Unit unit, int count = 1) {
        foreach (var item in this.BlueprintStacks) {
            if (item.BlueprintTypeStack == BlueprintStack.BlueprintType.UNIT && item.CreateAndOrderUnitStack.AttachedUnit == unit) {
                item.AddBlueprint(count);
            }
        }
    }

    public void SearchUnitNameAddBlueprint(string unitName, int count = 1) {
        foreach (var item in this.BlueprintStacks) {
            if (item.BlueprintTypeStack == BlueprintStack.BlueprintType.UNIT && item.CreateAndOrderUnitStack.AttachedUnit.UnitName.Equals(unitName)) {
                item.AddBlueprint(count);
            }
        }
    }

    public BlueprintStack GetStackAndAdd(int count, BlueprintStack.BlueprintRarityType type) {
        var possibleBlueprintStacks = new List<BlueprintStack>();
        foreach (var item in this.BlueprintStacks) {
            if (item.BlueprintTypeRarity == type) {
                possibleBlueprintStacks.Add(item);
            }
        }

        if (possibleBlueprintStacks.Count <= 0) {
            return null;
        }

        

        var bs = possibleBlueprintStacks[Mathf.RoundToInt(Random.Range(0, possibleBlueprintStacks.Count - 1))];
        bs.AddBlueprint(count);
        return bs;
    }
}
