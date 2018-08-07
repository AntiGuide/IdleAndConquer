using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapEffectHandler : MonoBehaviour {

    [SerializeField] private Vector2 startEndScale = new Vector2(0.3f, 0.5f);
    [SerializeField] private float scaleTime = 0.1f;
    [SerializeField] private Image img;
    private bool animating;
    private float endTime;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(startEndScale.x, startEndScale.x, startEndScale.x);
	    this.img.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (!this.animating) {
	        transform.localScale = new Vector3(startEndScale.x, startEndScale.x, startEndScale.x);
            return;
	    }

	    var scale = this.endTime - Time.time;
	    scale = Mathf.Max(0f, scale);
	    if (scale <= float.Epsilon) {
	        this.animating = false;
	        this.img.enabled = false;
        }
	    scale *= (1f / this.scaleTime);
	    scale = 1f - scale;
	    scale = this.startEndScale.x + (scale * (this.startEndScale.y - this.startEndScale.x));
	    this.transform.localScale = new Vector3(scale, scale, scale);
	}

    public void Tap(Vector2 inputPoint) {
        img.enabled = true;
        transform.position = inputPoint;
        endTime = Time.time + scaleTime;
        animating = true;
}
}
