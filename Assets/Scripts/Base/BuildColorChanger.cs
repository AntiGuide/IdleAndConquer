using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the color of a building acording to its status
/// </summary>
public class BuildColorChanger : MonoBehaviour {
    /// <summary>Green transparent texture that is applied when building can be built</summary>
    public Texture2D GreenTransparent;

    /// <summary>Red transparent texture that is applied when building can not be built</summary>
    public Texture2D RedTransparent;

    /// <summary>For menue on click</summary>
    private MenueController menueController;

    /// <summary>The material of the building</summary>
    private Material buildMaterial;

    /// <summary>Saves the normal texture when beginning to build</summary>
    private Texture2D finishedBuildingTexture;

    /// <summary>Saves wether the building is built</summary>
    private bool isBuilt = true;

    /// <summary>The amount of buildings that the building collides with at the moment</summary>
    private int collidingBuildings = 0;

    /// <summary>Getter/Setter for isBuilt</summary>
    public bool IsBuilt {
        get { return this.isBuilt; }
        set { this.isBuilt = value; }
    }

    /// <summary>Getter/Setter for collidingBuildings</summary>
    public int CollidingBuildings {
        get { return this.collidingBuildings; }
        set { this.collidingBuildings = value; }
    }

    /// <summary>Getter/Setter for menueController</summary>
    public MenueController MenueControll {
        get { return this.menueController; }
        set { this.menueController = value; }
    }

    /// <summary>Use this for initialization</summary>
    void Start() {
        this.buildMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
        this.finishedBuildingTexture = (Texture2D)this.buildMaterial.GetTexture("_MainTex");

        if (!this.isBuilt) {
            if (this.buildMaterial != null && this.buildMaterial.HasProperty("_MainTex")) {
                this.buildMaterial.SetTexture("_MainTex", GreenTransparent);
            } else {
                Debug.Log("Non existent material or no color property");
            }
        }
    }
    
    /// <summary>Update is called once per frame</summary>
    void Update() {
        if (!this.isBuilt && !BuildBuilding.PlayerBuilding) {
            this.buildMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
            if (this.buildMaterial != null && this.buildMaterial.HasProperty("_MainTex")) {
                this.buildMaterial.SetTexture("_MainTex", this.finishedBuildingTexture);
            } else {
                Debug.Log("Non existent material or no color property");
            }

            this.isBuilt = true;
        }
    }

    /// <summary>
    /// Triggered when a collision is detected. Adds 1 to collidingBuildings and sets the proper texture.
    /// </summary>
    /// <param name="other">The collider of the colliding building</param>
    void OnTriggerEnter(Collider other) {
        if (!this.isBuilt && BuildBuilding.PlayerBuilding && other.tag == "Buildings") {
            this.collidingBuildings++;
            if (this.buildMaterial != null && this.buildMaterial.HasProperty("_MainTex")) {
                this.buildMaterial.SetTexture("_MainTex", RedTransparent);
            } else {
                Debug.Log("Non existent material or no color property");
            }
        }
    }

    /// <summary>
    /// Triggered when a collision is ended. Subs 1 from collidingBuildings and sets the proper texture.
    /// </summary>
    /// <param name="other">The collider of the colliding building</param>
    void OnTriggerExit(Collider other) {
        if (!this.isBuilt && BuildBuilding.PlayerBuilding && other.tag == "Buildings") {
            this.collidingBuildings--;
            if (this.collidingBuildings == 0) {
                if (this.buildMaterial != null && this.buildMaterial.HasProperty("_MainTex")) {
                    this.buildMaterial.SetTexture("_MainTex", GreenTransparent);
                } else {
                    Debug.Log("Non existent material or no color property");
                }
            }
        }
    }
}
