using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAvailabilityManager : MonoBehaviour {
    [SerializeField] private CreateAndOrderUnit[] createAndOrderUnitsTanks;
    [SerializeField] private CreateAndOrderUnit[] createAndOrderUnitsSoldiers;
    [SerializeField] private CreateAndOrderUnit[] createAndOrderUnitsPlanes;

    public uint TankFactoryCount { get; set; }
    public uint BarracksCount { get; set; }
    public uint AirFieldCount { get; set; }
    public uint ResearchLabCount { get; set; }

    private void Start() {
        Refresh();
    }

    public void Refresh() {
        // Tanks
        for (var i = 0; i < TankFactoryCount; i++) {
            if (i == 0 || ResearchLabCount > i) {
                createAndOrderUnitsTanks[i].SetAvailability(true);
            }
        }

        if (this.createAndOrderUnitsTanks.Length > TankFactoryCount) {
            for (var i = TankFactoryCount; i < this.createAndOrderUnitsTanks.Length; i++) {
                createAndOrderUnitsTanks[i].SetAvailability(false);
            }
        }

        // Soldiers
        for (var i = 0; i < BarracksCount; i++) {
            if (i == 0 || ResearchLabCount > i) {
                createAndOrderUnitsSoldiers[i].SetAvailability(true);
            }
        }

        if (this.createAndOrderUnitsSoldiers.Length > BarracksCount) {
            for (var i = BarracksCount; i < this.createAndOrderUnitsSoldiers.Length; i++) {
                createAndOrderUnitsSoldiers[i].SetAvailability(false);
            }
        }

        // Planes
        for (var i = 0; i < AirFieldCount; i++) {
            if (i == 0 || ResearchLabCount > i) {
                createAndOrderUnitsPlanes[i].SetAvailability(true);
            }
        }

        if (this.createAndOrderUnitsPlanes.Length > AirFieldCount) {
            for (var i = AirFieldCount; i < this.createAndOrderUnitsPlanes.Length; i++) {
                createAndOrderUnitsPlanes[i].SetAvailability(false);
            }
        }


    }
}
