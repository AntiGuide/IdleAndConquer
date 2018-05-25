using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionQueue : MonoBehaviour {

    private List<Unit> prodQueue = new List<Unit>();
    private List<CreateAndOrderUnit> buttonQueue = new List<CreateAndOrderUnit>();
    private Unit latestUnit;
    private float remainingTime;
    float overlayFill;

    // Use this for initialization
    void Start() {
        
    }
    //0.83//0.966
    // Update is called once per frame
    void Update() {
        if (prodQueue.Count > 0) {
            if (latestUnit == null) {
                latestUnit = prodQueue[0];
                remainingTime += latestUnit.Buildtime;
            }
            remainingTime -= Time.deltaTime;
            overlayFill = Mathf.Min(remainingTime / latestUnit.Buildtime, 1.0f);
            overlayFill = Mathf.Max(overlayFill, 0f);
            buttonQueue[0].setProductionOverlayFill(overlayFill);
            if (remainingTime <= 0f) {
                latestUnit.addSingleBuiltUnit();
                buttonQueue[0].subSingleUnitBuilding();
                prodQueue.Remove(latestUnit);
                buttonQueue.Remove(buttonQueue[0]);
                if (prodQueue.Count > 0) {
                    latestUnit = prodQueue[0];
                    remainingTime = latestUnit.Buildtime;
                } else {
                    latestUnit = null;
                }
            }

        }
    }

    public void addToQueue(Unit u, CreateAndOrderUnit cAOButton) {
        prodQueue.Add(u);
        buttonQueue.Add(cAOButton);
    }
}
