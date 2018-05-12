using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    private Ray touchRay;
    private RaycastHit hitInformation;

    // Use this for initialization
    void Start () {
        /*objectToMove = GameObject.Find("__BakerHouseCollider");
        oToMovMaterial = objectToMove.GetComponent<MeshRenderer>().material;*/
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(Camera.main.transform.position, touchRay.direction, out hitInformation, 100.0f);//, layerMask);
            if (hitInformation.collider != null) {
                
            }
        } else {
            //oToMovMaterial = ChangeAlpha(oToMovMaterial, 1f);
        }
    }
    
    public static Material ChangeAlpha(Material mat, float alphaValue) {
        if (mat != null && mat.HasProperty("_Color")) {
            Color oldColor = mat.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
            mat.SetColor("_Color", newColor);
        }
        return mat;
    }
}
