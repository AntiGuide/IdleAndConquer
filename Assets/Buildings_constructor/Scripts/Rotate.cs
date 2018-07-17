using UnityEngine;

public class Rotate : MonoBehaviour {
    public float rotSpeed = 3f;
    public float rotMaxY = 0f;
    public float rotMinY = 0f;
    private float currentRotSpeed;

    // Use this for initialization
    private void Start() {
        this.currentRotSpeed = this.rotSpeed;
    }
    
    // Update is called once per frame
    private void Update() {
        if (Mathf.RoundToInt(this.rotMaxY) != 0 && Mathf.RoundToInt(this.rotMinY) != 0) {
            if (this.transform.rotation.y * 360 > this.rotMaxY) {
                this.currentRotSpeed = -this.rotSpeed;
            } else if (this.transform.rotation.y * 360 < this.rotMinY) {
                this.currentRotSpeed = this.rotSpeed;
            }
        }

        this.transform.Rotate(new Vector3(0, this.currentRotSpeed * Time.deltaTime, 0));
    }
}
