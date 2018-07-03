using UnityEngine;
using System.Collections;

public class Cycle_moving : MonoBehaviour {

	public float movSpeed=2f;
	public float range=1f;	
	[Header("Position phase in %")]
	public float Phase=0f;
	private int n=1;
	private Vector3 StartPosition;
	private float StartPhase;


	// Use this for initialization
	void Start () {
		if (Phase > 100)Phase = 100;
		if (Phase < 1)	Phase = 1;

		StartPosition = transform.position;
		StartPhase =(range * Phase / 100);
		transform.position = new Vector3 (StartPosition.x, StartPosition.y + StartPhase, StartPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
			if 	((transform.position.y > (StartPosition.y + range))) //|| (transform.position.y < StartPosition.y )) 
			{
				transform.position = new Vector3 (StartPosition.x, StartPosition.y + range, StartPosition.z);
				n = n * -1;
			}
			if 	((transform.position.y < (StartPosition.y))) //|| (transform.position.y < StartPosition.y )) 
			{
				transform.position = new Vector3 (StartPosition.x, StartPosition.y, StartPosition.z);
				n = n * -1;
			}

		transform.Translate (0,movSpeed*Time.deltaTime*n,0);

	}
}
