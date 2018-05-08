using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenueController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

    public static GameObject itemBeingDragged;
    
    public float menueExpandedHeight = 0.8f;
    public float menueExbandTriggerHeight = 0.6f;
    public enum MenueCategory
    {
        NONE = 0,
        MENUE_ONE,
        MENUE_TWO,
        MENUE_THREE,
        MENUE_FOUR,
        MENUE_FIVE
    };

    MenueCategory enabledScreen;
    private RectTransform canvasRectTransform;
    float canvasHeight;
    float startYMenue;
    float lerpJourneyLength;
    float lerpStartTime;
    Vector3 startMarker;
    Vector3 endMarker;
    public float lerpSpeed = 1000f;
    bool menueLerping;

    private Image bgButtonsImage;
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
            enabledScreen = MenueCategory.MENUE_ONE;
        } else {
            y = startYMenue;
            enabledScreen = MenueCategory.NONE;
        }
        startMarker = transform.position;
        endMarker = new Vector3(transform.position.x, y, 0);
        lerpJourneyLength = Vector3.Distance(startMarker, endMarker);
        menueLerping = true;
        lerpStartTime = Time.time;
    }

    // Use this for initialization
    void Start () {
        enabledScreen = MenueCategory.NONE;
        canvasRectTransform = GameObject.Find("/Canvas").GetComponent<RectTransform>();
        bgButtonsImage = GameObject.Find("BackgroundButtons").GetComponent<Image>();
        
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

    /**
     * <summary>This method opens the called type of menue instantly an without an animation.</summary>
     * <param name="screenType">Der Typ des zu öffnenden Menüs</param>
     * <returns>Nichts</returns>
     * <remarks>This method opens the called type of menue instantly an without an animation.</remarks>
     * <value>The method sets the enabledScreen data member.</value>
     * */
    public void OpenMenue(MenueCategory screenType) {
        switch (screenType) {
            case MenueCategory.MENUE_ONE:
                if (enabledScreen != MenueCategory.MENUE_ONE) {
                    
                    transform.position = new Vector3(transform.position.x, canvasHeight * menueExpandedHeight, 0);
                    enabledScreen = MenueCategory.MENUE_ONE;
                } else {
                    transform.position = new Vector3(transform.position.x, startYMenue, 0);
                    enabledScreen = MenueCategory.NONE;
                }
                break;
            default:
                break;
        }

        //transform.localPosition = Vector3.zero;
    }



}
