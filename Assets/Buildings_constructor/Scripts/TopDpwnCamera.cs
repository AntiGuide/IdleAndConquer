using UnityEngine;

public class TopDpwnCamera : MonoBehaviour {
    public Transform camTarget;
    public float smooth = 0.5f;
    public float rotSpeed = 50f;

    private void FixedUpdate() {
        if (this.camTarget == null) return;
        var desPosition = this.camTarget.position; // + cam_offset;
        var smoothedPosition = Vector3.Lerp(this.transform.position, desPosition, this.smooth * Time.deltaTime);
        this.transform.position = smoothedPosition;
        if (Input.GetKey("q")) {
            this.transform.Rotate(new Vector3(0, this.rotSpeed * Time.deltaTime, 0));
        }
            
        if (Input.GetKey("e")) {
            this.transform.Rotate(new Vector3(0, this.rotSpeed * Time.deltaTime * -1, 0));
        }
    }
}
