using UnityEngine;

public class Doors : MonoBehaviour {
    private Animator anim = null;

    // Use this for initialization
    private void Start() {
        this.anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider) {
        this.anim.SetBool("itsopen", true);
    }

    private void OnTriggerExit(Collider collider) {
        this.anim.SetBool("itsopen", false);
    }
}
