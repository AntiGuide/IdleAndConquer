using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatUpSpawner : MonoBehaviour {

    public GameObject floatUp;
    public float fadeTime;
    public float travelDistance;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void generateFloatUp(long value, FloatUp.ResourceType type, Vector2 pos) {
        GameObject go = Instantiate(floatUp, transform);
        if (value > 0) {
            pos += new Vector2(0, 25);
        } else {
            pos += new Vector2(0, -25);
        }
        go.transform.position = pos;
        go.GetComponent<FloatUp>().Initialize(type, value, fadeTime, travelDistance);


    }

    

}
