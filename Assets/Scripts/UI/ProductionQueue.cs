using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the production of units per base
/// </summary>
public class ProductionQueue : MonoBehaviour {
    /// <summary>Defines which base this production queue belongs to</summary>
    public int BaseID;

    /// <summary>Used to trigger sound</summary>
    public SoundController SoundControll;

    /// <summary>The list of units that the queue works on/will produce</summary>
    private readonly List<Unit> prodQueue = new List<Unit>();

    /// <summary>Reference to buttons corresponding to the units being built. Needs this for updating the button overlay.</summary>
    private readonly List<CreateAndOrderUnit> buttonQueue = new List<CreateAndOrderUnit>();

    /// <summary>The unit that is producing at the moment</summary>
    private Unit latestUnit;

    /// <summary>The time that is remaining for the latestUnits build process</summary>
    private float remainingTime;

    /// <summary>The percentage the overlay will be filled</summary>
    private float overlayFill;

    /// <summary>The current count of orders</summary>
    // private int inProduction = 0;

    public void AddToQueue(Unit u, CreateAndOrderUnit createAndOrderButton) {
        // this.inProduction++;
        this.prodQueue.Add(u);
        this.buttonQueue.Add(createAndOrderButton);
        this.SoundControll.StartSound(SoundController.Sounds.QUEUE_TAPS);
    }

    public void ResetButtons() {
        foreach (var button in this.buttonQueue) {
            button.SetProductionOverlayFill(0);
            button.SetUnitsBuilding(0);
        }
    }

    public void BaseSwitchRoutine() {
        foreach (var item in this.buttonQueue) {
            item.AddSingleUnitBuilding();
        }
    }

    // Update is called once per frame
    private void Update() {
        if (this.prodQueue.Count <= 0) return;
        if (this.latestUnit == null) {
            this.latestUnit = this.prodQueue[0];
            this.remainingTime += this.latestUnit.Buildtime;
        }

        this.remainingTime -= Time.deltaTime;
        this.overlayFill = Mathf.Min(this.remainingTime / this.latestUnit.Buildtime, 1.0f);
        this.overlayFill = Mathf.Max(this.overlayFill, 0f);
        if (BaseSwitcher.CurrentBase == this.BaseID) {
            this.buttonQueue[0].SetProductionOverlayFill(this.overlayFill);
        }

        if (!(this.remainingTime <= 0f)) return;
        this.latestUnit.AddSingleBuiltUnit();
        StartCoroutine(SoundControll.StartSound(SoundController.Sounds.UNIT_READY, this.latestUnit.CreateAndOrderButton.UnitTrack));
        if (BaseSwitcher.CurrentBase == this.BaseID) {
            this.buttonQueue[0].SubSingleUnitBuilding();
        }

        // this.inProduction--;
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
