using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionQueue : MonoBehaviour {
    public int baseID;
    private List<Unit> prodQueue = new List<Unit>();
    private List<CreateAndOrderUnit> buttonQueue = new List<CreateAndOrderUnit>();
    private Unit latestUnit;
    private float remainingTime;
    private float overlayFill;
    private int inProduction = 0;

    public void AddToQueue(Unit u, CreateAndOrderUnit createAndOrderButton) {
        this.inProduction++;
        this.prodQueue.Add(u);
        this.buttonQueue.Add(createAndOrderButton);
    }

    public void ResetButtons() {
        foreach (CreateAndOrderUnit button in buttonQueue) {
            button.SetProductionOverlayFill(0);
            button.SetUnitsBuilding(0);
        }
    }

    // Update is called once per frame
    void Update() {
        if (this.prodQueue.Count > 0) {
            if (this.latestUnit == null) {
                this.latestUnit = this.prodQueue[0];
                this.remainingTime += this.latestUnit.Buildtime;
            }

            this.remainingTime -= Time.deltaTime;
            this.overlayFill = Mathf.Min(this.remainingTime / this.latestUnit.Buildtime, 1.0f);
            this.overlayFill = Mathf.Max(this.overlayFill, 0f);
            if (BaseSwitcher.CurrentBase == baseID) {
                this.buttonQueue[0].SetUnitsBuilding(this.inProduction);
                this.buttonQueue[0].SetProductionOverlayFill(this.overlayFill);
            }
            if (this.remainingTime <= 0f) {
                this.latestUnit.AddSingleBuiltUnit();
                if (BaseSwitcher.CurrentBase == baseID) {
                    this.buttonQueue[0].SubSingleUnitBuilding();
                }

                this.inProduction--;
                this.prodQueue.Remove(this.latestUnit);
                this.buttonQueue.Remove(this.buttonQueue[0]);
                if (this.prodQueue.Count > 0) {
                    this.latestUnit = this.prodQueue[0];
                    this.remainingTime = this.latestUnit.Buildtime;
                } else {
                    this.latestUnit = null;
                }
            }
        }
    }
}
