using System;
using System.Collections.Generic;

public class MissionGoal {
    private readonly GoalTypes goalType;
    private Unit.Type typeRestriction;
    private int amountRestriction;

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

    public bool CheckGoal() {
        switch (goalType) {
            case GoalTypes.ONLY_USE_TYPE:
                break;
            case GoalTypes.MIN_TYPE_AMOUNT:
                break;
            case GoalTypes.MAX_TYPE_AMOUNT:
                break;
            case GoalTypes.MAX_AMOUNT:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return true;
    }
}
