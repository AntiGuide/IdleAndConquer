using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenueController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public static GameObject itemBeingDragged;

    public enum MenueCategory
    {
        NONE = 0,
        MAP,
        RESEARCH,
        PRODUCTION_BOOST,
        BLACK_MARKET,
        OPTIONS
    };

    MenueCategory enabledScreen;
    float canvasHeight;
    float startYMenue;
    private float lerpJourneyLength;
    private float lerpStartTime;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private float lerpSpeed;
    private bool menueLerping;

    public void OnBeginDrag(PointerEventData eventData) {
        itemBeingDragged = gameObject;
        
    }

    public void OnDrag(PointerEventData eventData) {
        float y = eventData.position.y;

        if (y > canvasHeight * 0.8f) {
            y = canvasHeight * 0.8f;
        } else if (y < startYMenue) {
            y = startYMenue;
        }
        transform.position = new Vector3(transform.position.x,y,0);
    }

    public void OnEndDrag(PointerEventData eventData) {
        itemBeingDragged = null;
        float y = transform.position.y;
        if (y > canvasHeight * 0.6f) {
            y = canvasHeight * 0.8f;
            enabledScreen = MenueCategory.MAP;
        } else {
            y = startYMenue;
        }
        startMarker = transform.position;
        endMarker = new Vector3(transform.position.x, y, 0);
        lerpJourneyLength = Vector3.Distance(startMarker, endMarker);
        menueLerping = true;
        lerpStartTime = Time.time;
        //transform.position = new Vector3(transform.position.x, y, 0);
    }

    // Use this for initialization
    void Start () {
        enabledScreen = MenueCategory.NONE;
        canvasHeight = GameObject.Find("/Canvas").GetComponent<RectTransform>().rect.height;
        startYMenue = transform.position.y;
        lerpSpeed = 1000.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (menueLerping && lerpJourneyLength == 0f) {
            menueLerping = false;
        }
        if (menueLerping) {
            float distCovered = (Time.time - lerpStartTime) * lerpSpeed;
            float fracJourney = distCovered / lerpJourneyLength;
            transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
            if (fracJourney >= 1f) {
                menueLerping = false;
            }
        }
    }

    /**
     * <summary>This method opens the called type of menue instantly an without an animation.</summary>
     * <param name="screenType">Der Typ des zu öffnenden Menüs</param>
     * <returns>Nichts</returns>
     * <remarks>This method opens the called type of menue instantly an without an animation. The types to be called are: MAP, RESEARCH, PRODUCTION_BOOST, BLACK_MARKET and OPTIONS</remarks>
     * <value>The method sets the _enabledScreen data member.</value>
     * */
    public void OpenMenue(MenueCategory screenType) {
        switch (screenType) {
            case MenueCategory.MAP:
                if (enabledScreen == MenueCategory.MAP) {
                    float y = transform.position.y;
                    if (y > canvasHeight * 0.8f) {
                        y = canvasHeight * 0.8f;
                    }
                    transform.position = new Vector3(transform.position.x, y, 0);
                    enabledScreen = MenueCategory.NONE;
                } else {
                    enabledScreen = MenueCategory.MAP;
                }
                break;
            default:
                break;
        }

        //transform.localPosition = Vector3.zero;
    }
}
