using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

	private Animator anim = null;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> (); 
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		anim.SetBool ("itsopen", true);
	}
	void OnTriggerExit(Collider collider)
	{
		anim.SetBool ("itsopen", false);
	}
}
