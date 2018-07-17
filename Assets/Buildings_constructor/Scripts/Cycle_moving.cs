using UnityEngine;

public class Cycle_moving : MonoBehaviour {
    public float movSpeed = 2f;
    public float range = 1f;
    [Header("Position phase in %")]
    public float Phase = 0f;
    private int n = 1;
    private Vector3 startPosition;
    private float startPhase;

    // Use this for initialization
    private void Start() {
        if (this.Phase > 100) {
            this.Phase = 100;
        }

        if (this.Phase < 1) {
            this.Phase = 1;
        }

        this.startPosition = transform.position;
        this.startPhase = this.range * this.Phase / 100;
        this.transform.position = new Vector3(this.startPosition.x, this.startPosition.y + this.startPhase, this.startPosition.z);
    }

    // Update is called once per frame
    private void Update() {
        if (this.transform.position.y > this.startPosition.y + this.range) {
            this.transform.position = new Vector3(this.startPosition.x, this.startPosition.y + this.range, this.startPosition.z);
            this.n = this.n * -1;
        }

        if (this.transform.position.y < this.startPosition.y) {
            this.transform.position = new Vector3(this.startPosition.x, this.startPosition.y, this.startPosition.z);
            this.n = this.n * -1;
        }

        this.transform.Translate(0, this.movSpeed * Time.deltaTime * this.n, 0);
    }
}
