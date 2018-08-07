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

    /// <summary>Saves wether the building is built</summary>
    private bool isBuilt = true;

    private readonly List<Material> materialList = new List<Material>();

    private readonly List<Texture2D> finishedBuildingTextureList = new List<Texture2D>();

    public BuildColorChanger() {
        this.CollidingBuildings = 0;
    }

    /// <summary>Getter/Setter for isBuilt</summary>
    public bool IsBuilt {
        set { this.isBuilt = value; }
    }

    /// <summary>Getter/Setter for collidingBuildings</summary>
    public int CollidingBuildings { get; private set; }

    /// <summary>Getter/Setter for menueController</summary>
    public MenueController MenueControll { get; set; }

    /// <summary>Use this for initialization</summary>
    private void Start() {
        var meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in meshRenderers) {
            if (!this.materialList.Contains(item.material)) { // If this couses performance issues dont remove duplicates and double change texture later
                this.materialList.Add(item.material);
            }
        }

        foreach (var item in this.materialList) {
            this.finishedBuildingTextureList.Add((Texture2D)item.GetTexture("_MainTex"));
        }
        
        if (!this.isBuilt) {
            this.SetGreen();
        }
    }

    private void SetGreen() {
        if (this.materialList.Count > 0) {
            foreach (var item in this.materialList) {
                item.SetTexture("_MainTex", this.GreenTransparent);
            }
        } else {
            Debug.Log("Non existent material");
        }
    }

    private void SetFinished() {
        if (this.materialList.Count > 0) {
            for (var i = 0; i < this.materialList.Count; i++) {
                this.materialList[i].SetTexture("_MainTex", this.finishedBuildingTextureList[i]);
            }
        } else {
            Debug.Log("Non existent material");
        }
    }

    private void SetRed() {
        if (this.materialList.Count > 0) {
            foreach (var item in this.materialList) {
                item.SetTexture("_MainTex", this.RedTransparent);
            }
        } else {
            Debug.Log("Non existent material");
        }
    }

    /// <summary>Update is called once per frame</summary>
    private void Update() {
        if (this.isBuilt || BuildBuilding.PlayerBuilding) return;
        // this.buildMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material; //?
        this.SetFinished();
        this.isBuilt = true;
    }

    /// <summary>
    /// Triggered when a collision is detected. Adds 1 to collidingBuildings and sets the proper texture.
    /// </summary>
    /// <param name="other">The collider of the colliding building</param>
    private void OnTriggerEnter(Collider other) {
        if (this.isBuilt || !BuildBuilding.PlayerBuilding || !other.CompareTag("Buildings")) return;
        this.CollidingBuildings++;
        this.SetRed();
    }

    /// <summary>
    /// Triggered when a collision is ended. Subs 1 from collidingBuildings and sets the proper texture.
    /// </summary>
    /// <param name="other">The collider of the colliding building</param>
    private void OnTriggerExit(Collider other) {
        if (this.isBuilt || !BuildBuilding.PlayerBuilding || !other.CompareTag("Buildings")) return;
        this.CollidingBuildings--;
        if (this.CollidingBuildings == 0) {
            this.SetGreen();
        }
    }
}
