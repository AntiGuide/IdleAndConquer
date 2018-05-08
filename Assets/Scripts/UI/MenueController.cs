using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenueController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public static GameObject itemBeingDragged;
    
    public float menueExpandedHeight = 0.8f;
    public float menueExbandTriggerHeight = 0.6f;
    /*public enum MenueCategory
    {
        NONE = 0,
        MENUE_ONE,
        MENUE_TWO,
        MENUE_THREE,
        MENUE_FOUR,
        MENUE_FIVE
    };*/

    public MainMenueController mainMenueController;
    
    private RectTransform canvasRectTransform;
    float canvasHeight;
    float startYMenue;
    float lerpJourneyLength;
    float lerpStartTime;
    Vector3 startMarker;
    Vector3 endMarker;
    public float lerpSpeed = 1000f;
    bool menueLerping;

    //private Image bgButtonsImage;
    private float menueExpandedHeightNew;

    private void Awake() {
    }

    public void OnBeginDrag(PointerEventData eventData) {
        itemBeingDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData) {
        float y = eventData.position.y;

        if (y > canvasHeight * menueExpandedHeight) {
            y = canvasHeight * menueExpandedHeight;
        } else if (y < startYMenue) {
            y = startYMenue;
        }
        transform.position = new Vector3(transform.position.x,y,0);
    }

    public void OnEndDrag(PointerEventData eventData) {
        itemBeingDragged = null;
        float y = transform.position.y;
        //Debug.Log(y);
        if (y > canvasHeight * menueExbandTriggerHeight) {
            y = canvasHeight * menueExpandedHeight;
            //enabledScreen = MenueCategory.MENUE_ONE;
        } else {
            y = startYMenue;
            mainMenueController.closeMenue();
        }
        startMarker = transform.position;
        endMarker = new Vector3(transform.position.x, y, 0);
        lerpJourneyLength = Vector3.Distance(startMarker, endMarker);
        menueLerping = true;
        lerpStartTime = Time.time;
    }

    // Use this for initialization
    void Start () {
        mainMenueController.closeMenue();
        canvasRectTransform = GameObject.Find("/Canvas").GetComponent<RectTransform>();
        //bgButtonsImage = GameObject.Find("BackgroundButtons").GetComponent<Image>();
        
        canvasHeight = canvasRectTransform.rect.height * canvasRectTransform.localScale.y;
        startYMenue = transform.position.y;
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (menueLerping && lerpJourneyLength == 0f) {
            menueLerping = false;
        }
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

        } else {
            transform.position = new Vector3(transform.position.x, canvasHeight * menueExpandedHeight, 0);
        }
    }

    public void Unexpand(bool animated) {
        if (animated) {

        } else {
            transform.position = new Vector3(transform.position.x, startYMenue, 0);
        }
    }



}
