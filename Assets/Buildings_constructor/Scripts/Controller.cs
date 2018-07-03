using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	private Animator anim;
	private CharacterController controller;
	//	private int battle_state = 1;
	public float speed = 1.0f;
	public float runSpeed = 3.0f;
	public float turnSpeed = 90.0f;
	public float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float w_sp = 0.0f; //walk speed
	private float r_sp = 0.0f; //run speed

	
	
	// Use this for initialization
	void Start () 
	{						
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
		
		w_sp = speed; //read walk speed
		r_sp = runSpeed; //read run speed
		
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if (Input.GetKey ("up")) 
		{	
			runSpeed = w_sp;
		}
		if  (Input.GetKey ("down")) 
		{			
			runSpeed = w_sp/2;	
		}

		
		if (controller.isGrounded) 
		{
			moveDirection=transform.forward * Input.GetAxis ("Vertical") * speed * runSpeed;
			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);						
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
	}
}

