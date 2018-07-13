using UnityEngine;

public class Controller : MonoBehaviour {
    public float speed = 1.0f;
    public float runSpeed = 3.0f;
    public float turnSpeed = 90.0f;
    public float gravity = 20.0f;
    private Animator anim;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float walkSpeed = 0.0f; // walk speed
    private float aktRunSpeed = 0.0f; // run speed

    // Use this for initialization
    void Start() {
        this.anim = this.GetComponent<Animator>();
        this.controller = this.GetComponent<CharacterController>();
        this.walkSpeed = this.speed; // read walk speed
        this.aktRunSpeed = this.runSpeed; // read run speed
    }
    
    // Update is called once per frame
    void Update() {
        if (Input.GetKey("up")) {
            this.runSpeed = this.walkSpeed;
        }

        if (Input.GetKey("down")) {
            this.runSpeed = this.walkSpeed / 2;
        }

        if (this.controller.isGrounded) {
            this.moveDirection = transform.forward * Input.GetAxis("Vertical") * this.speed * this.runSpeed;
            float turn = Input.GetAxis("Horizontal");
            this.transform.Rotate(0, turn * this.turnSpeed * Time.deltaTime, 0);
        }

        this.moveDirection.y -= this.gravity * Time.deltaTime;
        this.controller.Move(this.moveDirection * Time.deltaTime);
    }
}