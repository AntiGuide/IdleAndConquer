using UnityEngine;

public class Doors : MonoBehaviour {
    private Animator anim = null;

    // Use this for initialization
    void Start() {
        this.anim = this.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collider) {
        this.anim.SetBool("itsopen", true);
    }

    void OnTriggerExit(Collider collider) {
        this.anim.SetBool("itsopen", false);
    }
}
