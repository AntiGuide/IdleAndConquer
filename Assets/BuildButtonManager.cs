using UnityEngine;
using UnityEngine.UI;

public class BuildButtonManager : MonoBehaviour {

    public BuildBuilding builder;
    public Text cost;
    public GameObject attachedBuilding;
    public MoneyManagement moneyManager;

    private long costBuilding;

    // Use this for initialization
    void Start () {
        costBuilding = attachedBuilding.GetComponentInChildren<BuildingManager>().BuildCost;
        cost.text = MoneyManagement.formatMoney(costBuilding);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clickBuildBuilding(int i) {
        if (moneyManager.hasMoney(costBuilding)) {
            builder.buildBuilding(i, costBuilding);
        }
    }
}
