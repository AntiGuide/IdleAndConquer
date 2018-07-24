using System;
using System.Collections.Generic;

public class MissionGoal {
    private readonly GoalTypes goalType;
    private readonly Unit.Type typeRestriction;
    private readonly int amountRestriction;

    public MissionGoal(GoalTypes goalType, Unit.Type typeRestriction) {
        this.goalType = goalType;
        this.typeRestriction = typeRestriction;
    }

    public MissionGoal(GoalTypes goalType, int amountRestriction) {
        this.goalType = goalType;
        this.amountRestriction = amountRestriction;
    }

    public enum GoalTypes {
        ONLY_USE_TYPE = 0,
        MIN_TYPE_AMOUNT,
        MAX_TYPE_AMOUNT,
        MAX_AMOUNT,
        
    }

    public bool CheckGoal(List<Unit> unitsSent) {
        switch (goalType) {
            case GoalTypes.ONLY_USE_TYPE:
                foreach (var unit in unitsSent) {
                    if (unit.UnitType != typeRestriction) {
                        return false;
                    }
                }
                return true;
            case GoalTypes.MIN_TYPE_AMOUNT:
                var typesMin = new List<Unit.Type>(3);
                foreach (var unit in unitsSent) {
                    if (!typesMin.Contains(unit.UnitType)) {
                        typesMin.Add(unit.UnitType);
                    }
                }
                return typesMin.Count >= amountRestriction;
            case GoalTypes.MAX_TYPE_AMOUNT:
                var typesMax = new List<Unit.Type>(3);
                foreach (var unit in unitsSent) {
                    if (!typesMax.Contains(unit.UnitType)) {
                        typesMax.Add(unit.UnitType);
                    }
                }
                return typesMax.Count <= amountRestriction;
            case GoalTypes.MAX_AMOUNT:
                return unitsSent.Count <= amountRestriction;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
