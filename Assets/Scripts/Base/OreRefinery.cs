using System.Collections.Generic;
using UnityEngine;

/// <summary>Handles OreRefinery being built</summary>
public class OreRefinery : MonoBehaviour {
    /// <summary>To initialize a harvester upon build confirmation</summary>
    public GameObject HarvesterPrefab;

    public bool IsBuiltOnStartup = false;

    /// <summary>Reference to MoneyManagement to add and sub money</summary>
    private MoneyManagement moneyManager;

    /// <summary>Used for Harvester initialization</summary>
    private FloatUpSpawner floatUpSpawner;

    /// <summary>The list of harvesters blonging to this Ore Refinery</summary>
    private List<GameObject> attachedHarvesters = new List<GameObject>();

    /// <summary>The mine that will be attached to the harvester</summary>
    private GameObject mine;

    /// <summary>Spawns harvesters on completed build process</summary>
    public void InitializeBuilt() {
        this.moneyManager = GameObject.Find("/Main/Canvas/BackgroundTopStripRessources/TextDollar").GetComponent<MoneyManagement>();
        this.floatUpSpawner = GameObject.Find("/Main/Canvas/UXElemente").GetComponent<FloatUpSpawner>();
        this.mine = transform.parent.transform.parent.gameObject.GetComponentInChildren<BuildBuilding>().BuiltBuildings[2].transform.GetChild(0).gameObject;
        this.AddHarvester(ref this.attachedHarvesters, ref this.moneyManager, this, this.mine);
    }
    
    /// <summary>
    /// Adds a harvester to the refinery
    /// </summary>
    /// <param name="attachedHarvesters">Reference to the list the new harvester should be inserted into</param>
    /// <param name="moneyManager">Reference to the MoneyManagement to add money when harvester is working</param>
    /// <param name="oreRefinery">The attached refinery</param>
    /// <param name="oreMine">The attached mine</param>
    private void AddHarvester(ref List<GameObject> attachedHarvesters, ref MoneyManagement moneyManager, OreRefinery oreRefinery, GameObject oreMine) {
        GameObject go = UnityEngine.Object.Instantiate(this.HarvesterPrefab, transform.parent);
        Harvester harvester = go.GetComponentInChildren<Harvester>();
        harvester.Initialize(oreRefinery, oreMine, ref moneyManager, this.floatUpSpawner);
        attachedHarvesters.Add(go);
    }

    private void Start() {
        if (this.IsBuiltOnStartup) {
            this.InitializeBuilt();
        }
    }
}