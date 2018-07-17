using UnityEngine;

public class LoadUnits : MonoBehaviour {
    public GameObject unitButtonPrefab;
    // private int unitID;
    // private string unitName;

    private void OnEnable() {
        var ocds = transform.GetComponentsInChildren<OnClickDeploy>();
        foreach (var item in ocds) {
            UnityEngine.Object.Destroy(item.gameObject);
        }

        foreach (var item in Unit.AllUnits) {
            // this.unitName = item.UnitName;
            var count = item.UnitCount - item.SentToMission;
            if (count <= 0) continue;
            var ocd = Instantiate(this.unitButtonPrefab, transform).GetComponent<OnClickDeploy>();
            ocd.Initialize(item);
        }
    }
}
