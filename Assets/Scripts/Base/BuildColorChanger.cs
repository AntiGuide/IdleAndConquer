using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildColorChanger : MonoBehaviour {

    private Material buildMaterial;

    private bool isBuilt;
    public bool IsBuilt {
        get {
            return isBuilt;
        }

        set {
            isBuilt = value;
        }
    }

    private int collidingBuildings = 0;
    public int CollidingBuildings {
        get {
            return collidingBuildings;
        }

        set {
            collidingBuildings = value;
        }
    }


    // Use this for initialization
    void Start () {
        buildMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
        if (buildMaterial != null && buildMaterial.HasProperty("_Color")) {
            buildMaterial.SetColor("_Color", new Color(0, 1, 0, 0.44f));
            //TODO: Maybe disable texture if needed to see color and transparency
        } else {
            Debug.Log("Non existent material or no color property");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isBuilt && !BuildBuilding.PlayerBuilding) {
            buildMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
            if (buildMaterial != null && buildMaterial.HasProperty("_Color")) {
                buildMaterial.SetColor("_Color", new Color(1, 1, 1, 1));
                //TODO: Maybe disable texture if needed to see color and transparency
            } else {
                Debug.Log("Non existent material or no color property");
            }
            isBuilt = true;
        }
	}

    void OnTriggerEnter(Collider other) {
        if (!isBuilt && BuildBuilding.PlayerBuilding && other.tag == "Buildings") {
            collidingBuildings++;
            if (buildMaterial != null && buildMaterial.HasProperty("_Color")) {
                buildMaterial.SetColor("_Color", new Color(1, 0, 0, 0.44f));
            } else {
                Debug.Log("Non existent material or no color property");
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (!isBuilt && BuildBuilding.PlayerBuilding && other.tag == "Buildings") {
            collidingBuildings--;
            if (collidingBuildings == 0) {
                if (buildMaterial != null && buildMaterial.HasProperty("_Color")) {
                    buildMaterial.SetColor("_Color", new Color(0, 1, 0, 0.44f));
                } else {
                    Debug.Log("Non existent material or no color property");
                }
            }
        }
    }
}
