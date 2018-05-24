using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildColorChanger : MonoBehaviour {

    public Texture2D greenTransparent;
    public Texture2D redTransparent;

    public MenueController menueController;// For menue on click

    private Material buildMaterial;
    private Texture2D finishedBuildingTexture;

    private bool isBuilt = true;
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
        finishedBuildingTexture = (Texture2D)buildMaterial.GetTexture("_MainTex");

        if (!isBuilt) {
            if (buildMaterial != null && buildMaterial.HasProperty("_MainTex")) {
                buildMaterial.SetTexture("_MainTex", greenTransparent);
            } else {
                Debug.Log("Non existent material or no color property");
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isBuilt && !BuildBuilding.PlayerBuilding) {
            buildMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
            if (buildMaterial != null && buildMaterial.HasProperty("_MainTex")) {
                buildMaterial.SetTexture("_MainTex", finishedBuildingTexture);
            } else {
                Debug.Log("Non existent material or no color property");
            }
            isBuilt = true;
        }
	}

    void OnTriggerEnter(Collider other) {
        if (!isBuilt && BuildBuilding.PlayerBuilding && other.tag == "Buildings") {
            collidingBuildings++;
            if (buildMaterial != null && buildMaterial.HasProperty("_MainTex")) {
                buildMaterial.SetTexture("_MainTex",redTransparent);
            } else {
                Debug.Log("Non existent material or no color property");
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (!isBuilt && BuildBuilding.PlayerBuilding && other.tag == "Buildings") {
            collidingBuildings--;
            if (collidingBuildings == 0) {
                if (buildMaterial != null && buildMaterial.HasProperty("_MainTex")) {
                    buildMaterial.SetTexture("_MainTex", greenTransparent);
                } else {
                    Debug.Log("Non existent material or no color property");
                }
            }
        }
    }

    public MenueController GetMenueController() {
        return menueController;
    }

    public void SetMenueController(MenueController menueController) {
        this.menueController = menueController;
    }
}
