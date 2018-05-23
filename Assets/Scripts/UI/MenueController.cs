using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MenueController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public static GameObject itemBeingDragged;
    public MainMenueController mainMenueController;
    public float menueExpandedHeight = 0.8f;
    public float menueExbandTriggerHeight = 0.6f;
    public float lerpSpeed = 1000f;
    
    private RectTransform canvasRectTransform;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private static float canvasHeight;
    private static float startYMenue;
    private float lerpJourneyLength;
    private float lerpStartTime;
    private bool menueLerping;

    public void OnBeginDrag(PointerEventData eventData) {
        if (MainMenueController.IsExpanded) {
            itemBeingDragged = gameObject;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (MainMenueController.IsExpanded) {
            if (eventData.position.y > canvasHeight * menueExpandedHeight) {
                transform.position = new Vector3(transform.position.x, canvasHeight * menueExpandedHeight, 0);
            } else if (eventData.position.y < startYMenue) {
                transform.position = new Vector3(transform.position.x, startYMenue, 0);
            } else {
                transform.position = new Vector3(transform.position.x, eventData.position.y, 0);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (MainMenueController.IsExpanded) {
            itemBeingDragged = null;

            if (transform.position.y > canvasHeight * menueExbandTriggerHeight) {
                Expand(true);
            } else {
                Unexpand(true);
            }
        }
    }

    // Use this for initialization
    void Start () {
        canvasRectTransform = GameObject.Find("/Canvas").GetComponent<RectTransform>();
        canvasHeight = canvasRectTransform.rect.height * canvasRectTransform.localScale.y;
        startYMenue = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (menueLerping) {
            float distCovered = (Time.time - lerpStartTime) * (lerpSpeed/250) * lerpJourneyLength;
            
            float fracJourney = distCovered / lerpJourneyLength;
            
            transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
            
            if (fracJourney >= 1f) {
                menueLerping = false;
            }
        }
    }

    public void Expand(bool animated) {
        if (animated) {
            float y = canvasHeight * menueExpandedHeight;
            MainMenueController.IsExpanded = true;
            startMarker = transform.position;
            endMarker = new Vector3(transform.position.x, y, 0);

            lerpJourneyLength = Vector3.Distance(startMarker, endMarker);

            if (lerpJourneyLength <= 0.001f) {
                menueLerping = false;
            } else {
                menueLerping = true;
            }
            lerpStartTime = Time.time;
        } else {
            transform.position = new Vector3(transform.position.x, canvasHeight * menueExpandedHeight, 0);
        }
        MainMenueController.IsExpanded = true;
    }

    public void Unexpand(bool animated) {
        if (animated) {
            float y = startYMenue;
            MainMenueController.IsExpanded = false;
            startMarker = transform.position;
            endMarker = new Vector3(transform.position.x, y, 0);
            lerpJourneyLength = Vector3.Distance(startMarker, endMarker);
            menueLerping = true;
            lerpStartTime = Time.time;
        } else {
            transform.position = new Vector3(transform.position.x, startYMenue, 0);
        }
        MainMenueController.IsExpanded = false;
    }



}
