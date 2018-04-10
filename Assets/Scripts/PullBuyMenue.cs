using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PullBuyMenue : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public static GameObject itemBeingDragged;

    float canvasHeight;
    float startYMenue;

    public void OnBeginDrag(PointerEventData eventData) {
        itemBeingDragged = gameObject;
        
    }

    public void OnDrag(PointerEventData eventData) {
        float y = eventData.position.y;

        if (y > canvasHeight * 0.8f) {
            y = canvasHeight * 0.8f;
        }
        transform.position = new Vector3(transform.position.x,y,0);
    }

    public void OnEndDrag(PointerEventData eventData) {
        itemBeingDragged = null;
        float y = transform.position.y;
        if (y > canvasHeight * 0.6f) {
            y = canvasHeight * 0.8f;
        } else {
            y = startYMenue;
        }
        transform.position = new Vector3(transform.position.x, y, 0);
    }

    // Use this for initialization
    void Start () {
        startYMenue = transform.position.y;
        canvasHeight = GameObject.Find("/Canvas").GetComponent<RectTransform>().rect.height;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
