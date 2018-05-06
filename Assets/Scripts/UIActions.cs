using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIActions : MonoBehaviour {

    public GameObject buildConfirmUI;
    public float percentageXOffsetUI;
    public float percentageYOffsetUI;

    private Ray touchRay;
    private RaycastHit hitInformation;
    private int layerMask;
    //private GameObject objectToMove;
    private Material newBuildingMaterial;
    private bool playerBuilding;
    private GameObject newBuilding;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            if (EventSystem.current.IsPointerOverGameObject()){    // is the touch on the GUI
                Debug.Log("GUI");
            }else if(playerBuilding) {
                touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                layerMask = LayerMask.GetMask("Plane", "UI");
                Physics.Raycast(Camera.main.transform.position, touchRay.direction, out hitInformation, 100.0f, layerMask);
                //Physics.Raycast(Camera.main.transform.position, touchRay.direction, out hitInformation, 100.0f);
                //Debug.DrawRay(Camera.main.transform.position, touchRay.direction * 100, Color.red, 10);

                if (hitInformation.collider != null) {
                    newBuilding.transform.position = CameraHandler.toGrid(hitInformation.point);
                    
                }
            }
        }
	}

    private void LateUpdate() {
        if (playerBuilding) {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(newBuilding.transform.position);
            screenPoint = new Vector2(screenPoint.x - (Screen.width * percentageXOffsetUI), screenPoint.y - (Screen.height * percentageYOffsetUI));
            buildConfirmUI.transform.position = screenPoint;
            
        }
    }

    public void BuildNewBuilding(GameObject buildingPrefab){//, GameObject buildConfirmUI) {
        Destroy(GameObject.Find("Canvas/BackgroundBuildMenueDarken"));
        newBuilding = Instantiate(buildingPrefab);
        newBuilding.transform.position = new Vector3(-64.4f,0,0);
        Color tmpColorPre = newBuilding.GetComponent<MeshRenderer>().material.color;
        newBuildingMaterial = CameraHandler.ChangeAlpha(newBuildingMaterial, 0.5f);
        //this.buildConfirmUI = buildConfirmUI;
        playerBuilding = true;
    }
}
