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

    /// <summary>Saves wether the building is built</summary>
    private bool isBuilt = true;

    /// <summary>The amount of buildings that the building collides with at the moment</summary>
    private int collidingBuildings = 0;

    private List<Material> materialList = new List<Material>();

    private List<Texture2D> finishedBuildingTextureList = new List<Texture2D>();

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
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer item in meshRenderers) {
            if (!this.materialList.Contains(item.material)) { // If this couses performance issues dont remove duplicates and double change texture later
                this.materialList.Add(item.material);
            }
        }

        foreach (Material item in this.materialList) {
            this.finishedBuildingTextureList.Add((Texture2D)item.GetTexture("_MainTex"));
        }
        
        if (!this.isBuilt) {
            this.SetGreen();
        }
    }

    private void SetGreen() {
        if (this.materialList.Count > 0) {
            foreach (Material item in this.materialList) {
                item.SetTexture("_MainTex", this.GreenTransparent);
            }
        } else {
            Debug.Log("Non existent material");
        }
    }

    private void SetFinished() {
        if (this.materialList.Count > 0) {
            for (int i = 0; i < this.materialList.Count; i++) {
                this.materialList[i].SetTexture("_MainTex", this.finishedBuildingTextureList[i]);
            }
        } else {
            Debug.Log("Non existent material");
        }
    }

    private void SetRed() {
        if (this.materialList.Count > 0) {
            foreach (Material item in this.materialList) {
                item.SetTexture("_MainTex", this.RedTransparent);
            }
        } else {
            Debug.Log("Non existent material");
        }
    }

    /// <summary>Update is called once per frame</summary>
    void Update() {
        if (!this.isBuilt && !BuildBuilding.PlayerBuilding) {
            // this.buildMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material; //?
            this.SetFinished();
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
            this.SetRed();
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
                this.SetGreen();
            }
        }
    }
}
