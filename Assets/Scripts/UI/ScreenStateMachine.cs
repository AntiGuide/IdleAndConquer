using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenStateMachine : MonoBehaviour {
    public static List<OnClickDeploy> OCDs = new List<OnClickDeploy>();

    [SerializeField] private MainMenueController mainMenueController;
    [SerializeField] private GameObject missionDetailsWindow;
    [SerializeField] private MissionManager missionManager;
    [SerializeField] private UIInteraction UIInteractions;
    [SerializeField] private ShowChosenGeneral ShowChosenGen;

    private WindowStates aktState = WindowStates.MISSION_SELECT;

    public enum WindowStates {
        MISSION_SELECT,
        GENERAL_SELECT,
        SQUAD_SELECT
    }

    public void NextState() {
        switch (aktState) {
            case WindowStates.MISSION_SELECT:
                SetToState(WindowStates.GENERAL_SELECT);
                break;
            case WindowStates.GENERAL_SELECT:
                SetToState(WindowStates.SQUAD_SELECT);
                break;
            case WindowStates.SQUAD_SELECT:
                SetToState(WindowStates.MISSION_SELECT);
                this.missionManager.StartMission();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        foreach (var ocd in OCDs) {
            ocd.ResetOCD();
        }
        ShowChosenGen.ResetUnits();
        OnClickDeploy.DeployedUnits = 0;
    }
    public void LastState() {
        switch (aktState) {
            case WindowStates.MISSION_SELECT:
                SetToState(WindowStates.MISSION_SELECT);
                missionManager.Reset();
                this.UIInteractions.MainLoad();
                break;
            case WindowStates.GENERAL_SELECT:
                SetToState(WindowStates.MISSION_SELECT);
                missionManager.Reset();
                break;
            case WindowStates.SQUAD_SELECT:
                SetToState(WindowStates.GENERAL_SELECT);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        foreach (var ocd in OCDs) {
            ocd.ResetOCD();
        }
        ShowChosenGen.ResetUnits();
        OnClickDeploy.DeployedUnits = 0;

    }

    public void SetToState(WindowStates windowState) {
        switch (windowState) {
            case WindowStates.MISSION_SELECT:
                missionDetailsWindow.SetActive(false);
                this.mainMenueController.ActivateDeployUI(false);
                mainMenueController.Unexpand();
                // Reset MissionSelect
                // Reset GeneralMenue
                // Reset SquadMenue
                break;
            case WindowStates.GENERAL_SELECT:
                this.missionDetailsWindow.SetActive(true);
                this.mainMenueController.ActivateDeployUI(false);
                if (!mainMenueController.IsExpanded || mainMenueController.EnabledMenue + 1 != 1) {
                    this.mainMenueController.ToggleMenue(1);
                }
                // Reset GeneralMenue
                // Reset SquadMenue
                break;
            case WindowStates.SQUAD_SELECT:
                this.missionDetailsWindow.SetActive(false);
                this.mainMenueController.ActivateDeployUI(true);
                if (!mainMenueController.IsExpanded || mainMenueController.EnabledMenue + 1 != 2) {
                    this.mainMenueController.ToggleMenue(2);
                }
                // Reset SquadMenue
                break;
            default:
                throw new ArgumentOutOfRangeException("windowState", windowState, null);
        }

        aktState = windowState;
    }

    public void ResetStates() {
        SetToState(WindowStates.MISSION_SELECT);
    }
}
