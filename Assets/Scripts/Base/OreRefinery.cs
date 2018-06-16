using System.Collections.Generic;
using UnityEngine;

public class OreRefinery : MonoBehaviour{
    //For income. $
    //+1 Harvester each
    private MoneyManagement moneyManager;
    public int upgradeCost = 5000;
    //public Mine oreMine;
    public GameObject harvesterPrefab;
    private FloatUpSpawner floatUpSpawner;

    //private GameObject[] attachedHarvesters = new GameObject[5];
    private List<GameObject> attachedHarvesters = new List<GameObject>();

    private int level = 1;
    public int Level {
        get {
            return level;
        }
    }

    public void InitializeBuilt() {
        moneyManager = GameObject.Find("/Main/Canvas/BackgroundTopStripRessources/TextDollar").GetComponent<MoneyManagement>();
        floatUpSpawner = GameObject.Find("/Main/Canvas/UXElemente").GetComponent<FloatUpSpawner>();
        for (int i = 0; i < level; i++) {
            Mine mine = BuildBuilding.BuiltBuildings[2].GetComponentInChildren<Mine>();
            AddHarvester(ref attachedHarvesters, ref moneyManager, this, mine);
        }
    }

    void Start() {
        
    }

    public bool Upgrade(){
        if (moneyManager.subMoney(upgradeCost)) {
            level++;
            AddHarvester(ref attachedHarvesters, ref moneyManager, this, BuildBuilding.BuiltBuildings[2].GetComponentInChildren<Mine>());
            return true;
        }
        return false;
    }

    private void AddHarvester(ref List<GameObject> attachedHarvesters, ref MoneyManagement moneyManager, OreRefinery oreRefinery,Mine oreMine) {
        GameObject go;
        go = Instantiate(harvesterPrefab);
        Harvester harvester = go.GetComponentInChildren<Harvester>();
        harvester.Initialize(oreRefinery, oreMine, ref moneyManager, floatUpSpawner);
        attachedHarvesters.Add(go);
    }
}