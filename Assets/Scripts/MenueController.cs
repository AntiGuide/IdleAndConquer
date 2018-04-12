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
        MAP,
        RESEARCH,
        PRODUCTION_BOOST,
        BLACK_MARKET,
        OPTIONS
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

    private void Awake() {
        //Set screen size for Standalone
#if UNITY_STANDALONE
         Screen.SetResolution(576, 1024, false);
         Screen.fullScreen = false;
#endif
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
            enabledScreen = MenueCategory.MAP;
        } else {
            y = startYMenue;
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
        canvasHeight = canvasRectTransform.rect.height * canvasRectTransform.localScale.y;
        startYMenue = transform.position.y;
        
        /*Vector3[] tmpVec = new Vector3[4];
        canvasRectTransform.GetLocalCorners(tmpVec);
        for (int i = 0; i < 4; i++) {
            Debug.Log(tmpVec[i].x + " | " + tmpVec[i].y);

        }
        tmpVec = new Vector3[4];
        canvasRectTransform.GetWorldCorners(tmpVec);
        for (int i = 0; i < 4; i++) {
            Debug.Log(tmpVec[i].x + " | " + tmpVec[i].y);

        }
        
        DrawLine(new Vector3(0f,111f,0f), new Vector3(300f, 111f, 0f), Color.red,20.0f);*/
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
                    if (y > canvasHeight * menueExpandedHeight) {
                        y = canvasHeight * menueExpandedHeight;
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



    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f) {
        
        GameObject myLine = new GameObject("Line", typeof(LineRenderer));
        myLine.transform.parent = GameObject.Find("/Canvas").transform;
        myLine.transform.position = start;
        //myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 1.0f;
        lr.endWidth = 1.0f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }



}
