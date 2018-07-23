using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary> 
/// Class to hold the details of a mission 
/// </summary>
public class MissionDetails : MonoBehaviour {
    private static readonly float MoneyPerMissionSecond = 0.1f;

    [Header("Mission Details")]
    public Ratings AktRating = Ratings.NOT_COMPLETED;
    public int MissionTimeMinutes;
    public int MissionRenownReward;
    public MissionGoal.GoalTypes goalThreeCondition = MissionGoal.GoalTypes.ONLY_USE_TYPE;
    public Unit.Type TypeRestriction = Unit.Type.TANK;
    public int AmountRestriction = 0;

    [Header("Arrays")]
    public CreateAndOrderUnit[] EnemyUnitsArr;

    [Header("References")]
    public MissionManager MissionMan;
    public UIInteraction UIInteractions;
    public MainMenueController MainMenueControll;
    public GameObject MissionDetailsWindow;

    private readonly List<Unit> EnemyUnits = new List<Unit>();
    private MissionGoal missionGoal;
    private int missionTime;

    public int MissionTime {
        get {
            return missionTime;
        }
    }

    public int MissionMoneyReward {
        get {
            return AppPauseHandler.Harvesters.Count * Mathf.RoundToInt(MissionTime * MoneyPerMissionSecond);
        }
    }

    public enum Ratings {
        NOT_COMPLETED = 0,
        ONE_STAR,
        TWO_STAR,
        THREE_STAR,
    }

    public void OnClick() {
        this.MissionDetailsWindow.SetActive(true);
        this.MissionMan.GenerateMission(this, this.UIInteractions, this.MainMenueControll);
        this.MainMenueControll.ToggleMenue(1);
    }

    public Ratings CalculateBattle(List<Unit> unitsSent) {
        //EnemyUnits;
        //unitsSent;

        var plPlayer = CalcPowerlevel(unitsSent, EnemyUnits);
        var plEnemy = CalcPowerlevel(EnemyUnits);
        var calculatedPercentage = (float)plPlayer / plEnemy; //Determine how much better / worse the player is in %

        var guaranteedLosslessWin = 2f + UnityEngine.Random.Range(-0.05f, 0.05f); //Calculate guaranteed lossless win(200 % +/ -5 % RND)
        var guaranteedWin = 1f + UnityEngine.Random.Range(-0.05f, 0.05f); //Calculate guaranteed win(100 % +/ -5 % RND)

        if (calculatedPercentage < guaranteedWin) {
            //throw new NotImplementedException();
            // Kill Units
            // Check if general died
            return Ratings.NOT_COMPLETED;
        } else if (calculatedPercentage < guaranteedLosslessWin) {
            //throw new NotImplementedException();
            AktRating = AktRating < Ratings.ONE_STAR ? Ratings.ONE_STAR : AktRating;
            CalcLosses(calculatedPercentage);
            return Ratings.ONE_STAR;
        } else {
            if (missionGoal.CheckGoal()) { //Check if Condition3 is matched
                //throw new NotImplementedException();
                AktRating = AktRating < Ratings.THREE_STAR ? Ratings.THREE_STAR : AktRating;
                return Ratings.THREE_STAR;
            } else {
                //throw new NotImplementedException();
                AktRating = AktRating < Ratings.TWO_STAR ? Ratings.TWO_STAR : AktRating;
                return Ratings.TWO_STAR;
            }
        }
    }

    private void CalcLosses(float calculatedPercentage) {
        calculatedPercentage--;

        // Calc damage of enemies
        // Take calculatedPercentage of Enemy damage
        throw new NotImplementedException();
    }

    /// <summary>
    /// Calculates the powerlevel of player units attacking the enemy.
    /// </summary>
    /// <param name="unitsA">The units of the player.</param>
    /// <param name="unitsB">The units of the enemys.</param>
    /// <returns></returns>
    private static int CalcPowerlevel(IEnumerable<Unit> unitsA, List<Unit> unitsB) {
        var combAttack = 0;
        var combHP = 0;
        var combDef = 0;

        foreach (var unit in unitsA) {
            combAttack += unit.GetAverageDamage(unitsB);
            combHP += unit.GetHP();
            combDef += unit.GetDef();
        }

        return Mathf.RoundToInt(combHP * combAttack * combDef / 1000f);
    }

    private static int CalcPowerlevel(IEnumerable<Unit> units) {
        //GewichtetesPowerlevel

        return units.Sum(unit => unit.Powerlevel);
    }

    private void Start() {
        foreach (var createAndOrderUnit in EnemyUnitsArr) {
            EnemyUnits.Add(createAndOrderUnit.AttachedUnit);
        }

        switch (goalThreeCondition) {
            case MissionGoal.GoalTypes.ONLY_USE_TYPE:
                missionGoal = new MissionGoal(goalThreeCondition, TypeRestriction);
                break;
            case MissionGoal.GoalTypes.MAX_AMOUNT:
            case MissionGoal.GoalTypes.MIN_TYPE_AMOUNT:
            case MissionGoal.GoalTypes.MAX_TYPE_AMOUNT:
                missionGoal = new MissionGoal(goalThreeCondition, AmountRestriction);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        missionTime = MissionTimeMinutes * 60;
    }
}