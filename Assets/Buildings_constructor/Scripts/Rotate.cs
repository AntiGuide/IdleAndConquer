using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    public float rotSpeed = 3f;
    public float rotMaxY = 0f;
    public float rotMinY = 0f;
    private float currentRotSpeed;

    // Use this for initialization
    void Start() {
        this.currentRotSpeed = this.rotSpeed;
    }
    
    // Update is called once per frame
    void Update() {
        if (rotMaxY != 0 && rotMinY != 0) {
            if (transform.rotation.y * 360 > rotMaxY) {
                this.currentRotSpeed = -this.rotSpeed;
            } else if (transform.rotation.y * 360 < rotMinY) {
                this.currentRotSpeed = this.rotSpeed;
            }
        }

        transform.Rotate(new Vector3(0, this.currentRotSpeed * Time.deltaTime, 0));
    }
}
