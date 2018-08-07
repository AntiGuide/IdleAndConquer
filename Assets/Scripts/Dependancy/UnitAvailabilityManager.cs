using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAvailabilityManager : MonoBehaviour {
    [SerializeField] private CreateAndOrderUnit[] createAndOrderUnitsTanks;
    [SerializeField] private CreateAndOrderUnit[] createAndOrderUnitsSoldiers;
    [SerializeField] private CreateAndOrderUnit[] createAndOrderUnitsPlanes;

    public uint[] TankFactoryCount { get; set; }
    public uint[] BarracksCount { get; set; }
    public uint[] AirFieldCount { get; set; }
    public uint ResearchLabCount { get; set; }

    private void Start() {
        TankFactoryCount = new uint[] { 0, 0 };
        BarracksCount = new uint[] { 0, 0 };
        AirFieldCount = new uint[] { 0, 0 };
        Refresh();
    }

    public void Refresh() {
        var curBaseID = BaseSwitcher.CurrentBase;

        // Tanks
        for (var i = 0; i < TankFactoryCount[curBaseID]; i++) {
            if (i == 0 || ResearchLabCount > i) {
                createAndOrderUnitsTanks[i].SetAvailability(true);
            }
        }

        if (this.createAndOrderUnitsTanks.Length > TankFactoryCount[curBaseID]) {
            for (var i = TankFactoryCount[curBaseID]; i < this.createAndOrderUnitsTanks.Length; i++) {
                createAndOrderUnitsTanks[i].SetAvailability(false);
            }
        }

        // Soldiers
        for (var i = 0; i < BarracksCount[curBaseID]; i++) {
            if (i == 0 || ResearchLabCount > i) {
                createAndOrderUnitsSoldiers[i].SetAvailability(true);
            }
        }

        if (this.createAndOrderUnitsSoldiers.Length > BarracksCount[curBaseID]) {
            for (var i = BarracksCount[curBaseID]; i < this.createAndOrderUnitsSoldiers.Length; i++) {
                createAndOrderUnitsSoldiers[i].SetAvailability(false);
            }
        }

        // Planes
        for (var i = 0; i < AirFieldCount[curBaseID]; i++) {
            if (i == 0 || ResearchLabCount > i) {
                createAndOrderUnitsPlanes[i].SetAvailability(true);
            }
        }

        if (this.createAndOrderUnitsPlanes.Length > AirFieldCount[curBaseID]) {
            for (var i = AirFieldCount[curBaseID]; i < this.createAndOrderUnitsPlanes.Length; i++) {
                createAndOrderUnitsPlanes[i].SetAvailability(false);
            }
        }


    }
}
