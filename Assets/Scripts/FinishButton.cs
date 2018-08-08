using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButton : MonoBehaviour {

    public MissionUI missionUI;

    public void OnClick() {
        missionUI.ForceFinish = true;
    }
}
