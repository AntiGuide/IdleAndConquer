using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedGeneral : MonoBehaviour {

    private static General general;
    public static General General {
        get {
            return general;
        }

        set {
            general = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
