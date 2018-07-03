using UnityEngine;
using System.Collections;

public class TopDpwnCamera : MonoBehaviour {

	public Transform cam_target;
	public float smooth = 0.5f;
	public Vector3 cam_offset;
	public float rotSpeed = 50f;
	private Vector3 start_angle;

	void Start () 
	{
		start_angle = transform.position; 

	}

	void FixedUpdate () 
	{
		if (cam_target != null) 
		{
			Vector3 desPosition = cam_target.position;// + cam_offset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desPosition, smooth*Time.deltaTime);
			transform.position = smoothedPosition;	

	
			if (Input.GetKey ("q")) 
			{
				transform.Rotate(new Vector3(0,rotSpeed*Time.deltaTime,0));
			}
			if (Input.GetKey ("e")) 
			{
				transform.Rotate(new Vector3(0,rotSpeed*Time.deltaTime*-1,0));
			}
		}
	}
}
