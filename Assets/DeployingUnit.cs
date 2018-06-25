using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployingUnit : MonoBehaviour {
    private Unit unit;
    private int deployinCount = 1;

    public void Initialize(Unit unit) {
        this.unit = unit;
    }

    public void AddDeployingCount(int addVal) {
        this.deployinCount += addVal;
    }
}
