using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionQueue : MonoBehaviour {
    private List<Unit> prodQueue = new List<Unit>();
    private List<CreateAndOrderUnit> buttonQueue = new List<CreateAndOrderUnit>();
    private Unit latestUnit;
    private float remainingTime;
    private float overlayFill;

    public void AddToQueue(Unit u, CreateAndOrderUnit createAndOrderButton) {
        this.prodQueue.Add(u);
        this.buttonQueue.Add(createAndOrderButton);
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
            this.buttonQueue[0].SetProductionOverlayFill(this.overlayFill);
            if (this.remainingTime <= 0f) {
                this.latestUnit.AddSingleBuiltUnit();
                this.buttonQueue[0].SubSingleUnitBuilding();
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
