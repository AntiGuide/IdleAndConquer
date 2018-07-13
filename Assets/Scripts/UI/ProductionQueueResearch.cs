using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the research per base
/// </summary>
public class ProductionQueueResearch : MonoBehaviour {
    /// <summary>Defines which base this production queue belongs to</summary>
    public int BaseID;

    ///// <summary>The list of researches that the queue works on/will produce</summary>
    // private List<Unit> prodQueue = new List<Unit>();

    /// <summary>Used to trigger sound</summary>
    public SoundController SoundControll;

    /// <summary>Reference to buttons corresponding to the researches. Needs this for updating the button overlay.</summary>
    private List<BlueprintStack> buttonQueue = new List<BlueprintStack>();

    /// <summary>The unit that is producing at the moment</summary>
    private BlueprintStack latestBlueprintStack;

    /// <summary>The time that is remaining for the latest research build process</summary>
    private float remainingTime;

    /// <summary>The percentage the overlay will be filled</summary>
    private float overlayFill;

    ///// <summary>The current count of orders</summary>
    // private int inProduction = 0;

    public void AddToQueue(BlueprintStack blueprintStack) {
        // this.inProduction++;
        // this.prodQueue.Add(u);
        SoundControll.StartSound(SoundController.Sounds.UPGRADING);
        this.buttonQueue.Add(blueprintStack);
    }

    public void ResetButtons() {
        foreach (BlueprintStack button in this.buttonQueue) {
            button.BuildingOverlay.fillAmount = 0f;
        }
    }

    // Update is called once per frame
    void Update() {
        if (this.buttonQueue.Count > 0) {
            if (this.latestBlueprintStack == null) {
                this.latestBlueprintStack = this.buttonQueue[0];
                this.remainingTime += this.latestBlueprintStack.Buildtime;
            }

            this.remainingTime -= Time.deltaTime;
            this.overlayFill = Mathf.Min(this.remainingTime / this.latestBlueprintStack.Buildtime, 1.0f);
            this.overlayFill = Mathf.Max(this.overlayFill, 0f);
            if (BaseSwitcher.CurrentBase == this.BaseID) {
                this.buttonQueue[0].BuildingOverlay.fillAmount = this.overlayFill;
            }

            if (this.remainingTime <= 0f) {
                this.latestBlueprintStack.PerformLevelUp();
                this.buttonQueue.Remove(this.buttonQueue[0]);
                if (this.buttonQueue.Count > 0) {
                    this.latestBlueprintStack = this.buttonQueue[0];
                    this.remainingTime = this.latestBlueprintStack.Buildtime;
                } else {
                    this.latestBlueprintStack = null;
                }
            }
        }
    }
}
