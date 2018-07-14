using UnityEngine;

public class TopDpwnCamera : MonoBehaviour {
    public Transform camTarget;
    public float smooth = 0.5f;
    public Vector3 camOffset;
    public float rotSpeed = 50f;
    //private Vector3 startAngle;

    void Start() {
        //this.startAngle = this.transform.position; 
    }
    
    void FixedUpdate() {
        if (this.camTarget != null) {
            Vector3 desPosition = this.camTarget.position; // + cam_offset;
            Vector3 smoothedPosition = Vector3.Lerp(this.transform.position, desPosition, this.smooth * Time.deltaTime);
            this.transform.position = smoothedPosition;
            if (Input.GetKey("q")) {
                this.transform.Rotate(new Vector3(0, this.rotSpeed * Time.deltaTime, 0));
            }
            
            if (Input.GetKey("e")) {
                this.transform.Rotate(new Vector3(0, this.rotSpeed * Time.deltaTime * -1, 0));
            }
        }
    }
}
