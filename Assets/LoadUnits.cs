using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUnits : MonoBehaviour {
    public GameObject unitButtonPrefab;
    private int unitID;
    private string unitName;

    // Use this for initialization
    void Start() {
    }

    private void OnEnable() {
        OnClickDeploy[] ocds = transform.GetComponentsInChildren<OnClickDeploy>();
        foreach (OnClickDeploy item in ocds) {
            Destroy(item.gameObject);
        }
        foreach (Unit item in Unit.AllUnits) {
            this.unitName = item.UnitName;
            int count = item.UnitCount - item.SentToMission;
            if (count > 0) {
                // addPowerlevel(count * Mathf.RoundToInt((hp * attack * defense) / 1000), true);
                // setUnitCount(count.ToString());
                // attachedUnit.setUnitCount(count);
                OnClickDeploy ocd = Instantiate(this.unitButtonPrefab, transform).GetComponent<OnClickDeploy>();
                ocd.Initialize(item);
            }
        }
    }
}
