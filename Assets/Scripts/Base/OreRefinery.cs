using System.Collections.Generic;
using UnityEngine;

public class OreRefinery : MonoBehaviour {
    // For income. $ +1 Harvester each level
    public int upgradeCost = 5000;
    public GameObject harvesterPrefab;
    private MoneyManagement moneyManager;
    private FloatUpSpawner floatUpSpawner;
    private List<GameObject> attachedHarvesters = new List<GameObject>();
    private int level = 1;

    public int Level {
        get { return this.level; }
    }

    public void InitializeBuilt() {
        this.moneyManager = GameObject.Find("/Main/Canvas/BackgroundTopStripRessources/TextDollar").GetComponent<MoneyManagement>();
        this.floatUpSpawner = GameObject.Find("/Main/Canvas/UXElemente").GetComponent<FloatUpSpawner>();
        for (int i = 0; i < this.level; i++) {
            Mine mine = BuildBuilding.BuiltBuildings[2].GetComponentInChildren<Mine>();
            this.AddHarvester(ref this.attachedHarvesters, ref this.moneyManager, this, mine);
        }
    }

    public bool Upgrade() {
        if (this.moneyManager.SubMoney(this.upgradeCost)) {
            this.level++;
            this.AddHarvester(ref this.attachedHarvesters, ref this.moneyManager, this, BuildBuilding.BuiltBuildings[2].GetComponentInChildren<Mine>());
            return true;
        }

        return false;
    }
    
    private void AddHarvester(ref List<GameObject> attachedHarvesters, ref MoneyManagement moneyManager, OreRefinery oreRefinery, Mine oreMine) {
        GameObject go;
        go = UnityEngine.Object.Instantiate(this.harvesterPrefab, transform.parent);
        Harvester harvester = go.GetComponentInChildren<Harvester>();
        harvester.Initialize(oreRefinery, oreMine, ref moneyManager, this.floatUpSpawner);
        attachedHarvesters.Add(go);
    }
}