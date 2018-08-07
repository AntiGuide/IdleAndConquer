using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

/// <summary> 
/// Class to hold the details of a mission 
/// </summary>
public class MissionDetails : MonoBehaviour {
    private const float MoneyPerMissionSecond = 0.1f;

    [Header("Mission Details")]
    public int MissionGroupID = 1;
    public int MissionVariantID = 0;
    public string MissionName = "Mission 1";
    public Ratings AktRating = Ratings.NOT_COMPLETED;
    public int MissionTimeMinutes;
    public int MissionRenownReward;
    public MissionGoal.GoalTypes goalThreeCondition = MissionGoal.GoalTypes.ONLY_USE_TYPE;
    public Unit.Type TypeRestriction = Unit.Type.TANK;
    public int AmountRestriction = 0;

    [Header("State")]
    public bool currentlyRunning = false;

    [Header("Arrays")]
    public CreateAndOrderUnit[] EnemyUnitsArr;

    [Header("References")]
    public MissionManager MissionMan;
    public UIInteraction UIInteractions;
    public MainMenueController MainMenueControll;
    public GameObject MissionDetailsWindow;
    public ScreenStateMachine ScreenStateMach;
    public MissionAvailabilityManager missionAvailabilityManager;

    private readonly List<Unit> EnemyUnits = new List<Unit>();
    private MissionGoal missionGoal;
    private bool available;

    public int MissionTime { get; private set; }

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

    public void SetAvailability(bool available) {
        if (MissionGroupID == 1) {
            available = true;
        }

        this.available = available;
        GetComponent<SpriteRenderer>().color = available ? Color.white : Color.grey;
    }

    public void OnClick() {
        if (currentlyRunning || !available) {
            return;
        }
        //this.MissionDetailsWindow.SetActive(true);
        var detailWindow =  this.MissionDetailsWindow.GetComponent<MissionDetailWindow>();
        var rewardText = "Reward:" + System.Environment.NewLine + "Test";
        if (MissionVariantID <= 0) {
            detailWindow.FillInfo("Mission " + MissionGroupID, rewardText, (ushort)this.AktRating);
        } else {
            detailWindow.FillInfo("Mission " + MissionGroupID + "-" + MissionVariantID, rewardText, (ushort)this.AktRating);
        }
        
        //this.MissionDetailsWindow.SetActive(false);
        ScreenStateMach.SetToState(ScreenStateMachine.WindowStates.GENERAL_SELECT);

        this.MissionMan.GenerateMission(this, this.UIInteractions, this.MainMenueControll);
    }

    public Ratings CalculateBattle(List<Unit> unitsSent, General generalSent) {
        //EnemyUnits;
        //unitsSent;

        var plPlayer = CalcPowerlevel(unitsSent, EnemyUnits);
        var plEnemy = CalcPowerlevel(EnemyUnits);
        var calculatedPercentage = (float)plPlayer / plEnemy; //Determine how much better / worse the player is in %

        var guaranteedLosslessWin = 2f + Random.Range(-0.05f, 0.05f); //Calculate guaranteed lossless win(200 % +/ -5 % RND)
        var guaranteedWin = 1f + Random.Range(-0.05f, 0.05f); //Calculate guaranteed win(100 % +/ -5 % RND)

        MissionDetails.Ratings tmpRating;

        if (calculatedPercentage < guaranteedWin) {
            foreach (var unit in unitsSent) {
                unit.KillSingleUnit();
            }

            generalSent.Died();
            tmpRating = Ratings.NOT_COMPLETED;
        } else if (calculatedPercentage < guaranteedLosslessWin) {
            Debug.Log("Player won mission but lost units!");
            CalcLosses((calculatedPercentage - guaranteedWin) * (guaranteedLosslessWin - guaranteedWin), ref unitsSent);
            tmpRating =  Ratings.ONE_STAR;
        } else {
            Debug.Log("Player won mission and lost no units!");
            tmpRating = Ratings.TWO_STAR;
            if (missionGoal.CheckGoal(unitsSent)) {
                tmpRating = Ratings.THREE_STAR;
            }
        }

        foreach (var unit in unitsSent) {
            unit.SentToMission--;
        }

        unitsSent = null;

        

        return tmpRating;
    }

    /// <summary>
    /// Calculates the losses on mission win.
    /// </summary>
    /// <param name="calculatedPercentage">How much better than the enemy is the player in relation to the randomized gates to win with/without losing units.</param>
    /// <param name="unitsSent">The units sent.</param>
    private static void CalcLosses(float calculatedPercentage, ref List<Unit> unitsSent) {
        var unitCountToKill = Mathf.Min(Mathf.Max(1, Mathf.RoundToInt(unitsSent.Count - (calculatedPercentage * unitsSent.Count))), unitsSent.Count - 1);
        Debug.Log("Player lost " + unitCountToKill + " Units!");
        KillUnits(ref unitsSent, unitCountToKill);
    }

    private static void KillUnits(ref List<Unit> units, int unitCountToKill) {
        for (; unitCountToKill > 0; unitCountToKill--) {
            //var avgSurvivability = 0f;
            var gesSurvivability = 0f;
            foreach (var unit in units) {
                gesSurvivability += unit.GetDef() + unit.GetHP();
            }

            //avgSurvivability = gesSurvivability / units.Count;
            var rnd = Random.Range(0f, 1f);
            var addedDeathChance = 0f;
            foreach (var unit in units) {
                var unitDeathChance = (unit.GetDef() + unit.GetHP()) / gesSurvivability;
                if (rnd >= unitDeathChance + addedDeathChance) {
                    addedDeathChance += unitDeathChance;
                } else {
                    unit.KillSingleUnit();
                    units.Remove(unit);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Calculates the powerlevel of player units attacking the enemy.
    /// </summary>
    /// <param name="unitsA">The units of the player.</param>
    /// <param name="unitsB">The units of the enemys.</param>
    /// <returns></returns>
    private static int CalcPowerlevel(IEnumerable<Unit> unitsA, List<Unit> unitsB) {
        var combPL = 0;

        foreach (var unit in unitsA) {
            combPL += Mathf.RoundToInt(unit.GetHP() * unit.GetAverageDamage(unitsB) * unit.GetDef() / 1000f);
        }

        return combPL;
    }

    private static int CalcPowerlevel(IEnumerable<Unit> units) {
        //GewichtetesPowerlevel

        return units.Sum(unit => unit.Powerlevel);
    }

    private void Start() {
        AktRating = (Ratings)PlayerPrefs.GetInt("Mission_" + MissionGroupID + "_" + MissionVariantID, (int)Ratings.NOT_COMPLETED);
        missionAvailabilityManager.Refresh();
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
        
        MissionTime = MissionTimeMinutes * 60;
    }

    public void SaveRating() {
        PlayerPrefs.SetInt("Mission_" + MissionGroupID + "_" + MissionVariantID, (int)AktRating);
    }
}