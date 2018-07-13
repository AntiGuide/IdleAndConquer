using UnityEngine;
using System.Collections;

public class Cycle_moving : MonoBehaviour {
    public float movSpeed = 2f;
    public float range = 1f;
    [Header("Position phase in %")]
    public float Phase = 0f;
    private int n = 1;
    private Vector3 StartPosition;
    private float StartPhase;

    // Use this for initialization
    void Start() {
        if (Phase > 100) Phase = 100;

        if (Phase < 1) Phase = 1;

        this.StartPosition = transform.position;
        StartPhase = range * Phase / 100;
        transform.position = new Vector3(this.StartPosition.x, this.StartPosition.y + StartPhase, this.StartPosition.z);
    }

    // Update is called once per frame
    void Update() {
        if (this.transform.position.y > (this.StartPosition.y + range)) {
            this.transform.position = new Vector3(this.StartPosition.x, this.StartPosition.y + range, this.StartPosition.z);
            this.n = this.n * -1;
        }

        if (transform.position.y < this.StartPosition.y) {
            this.transform.position = new Vector3(this.StartPosition.x, this.StartPosition.y, this.StartPosition.z);
            this.n = this.n * -1;
        }

        this.transform.Translate(0, this.movSpeed * Time.deltaTime * this.n, 0);

    }
}
