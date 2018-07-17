using UnityEngine;

/// <summary>
/// Controller for demo player from Buildings_constructor
/// </summary>
public class Controller : MonoBehaviour {
    /// <summary>The normal walk speed</summary>
    public float Speed = 1.0f;

    /// <summary>The speed while running</summary>
    public float RunSpeed = 3.0f;

    /// <summary>The turn speed of the character</summary>
    public float TurnSpeed = 90.0f;

    /// <summary>The gravity constant per controller</summary>
    private float gravity = 20.0f;

    /// <summary>Reference to the CharacterController</summary>
    private CharacterController controller;

    /// <summary>The direction to move to during the frame</summary>
    private Vector3 moveDirection = Vector3.zero;

    /// <summary>The speed during normal walking</summary>
    private float walkSpeed = 0.0f;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start() {
        this.controller = this.GetComponent<CharacterController>();
        this.walkSpeed = this.Speed; // read walk speed
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update() {
        if (Input.GetKey("up")) {
            this.RunSpeed = this.walkSpeed;
        }

        if (Input.GetKey("down")) {
            this.RunSpeed = this.walkSpeed / 2;
        }

        if (this.controller.isGrounded) {
            this.moveDirection = transform.forward * Input.GetAxis("Vertical") * this.Speed * this.RunSpeed;
            var turn = Input.GetAxis("Horizontal");
            this.transform.Rotate(0, turn * this.TurnSpeed * Time.deltaTime, 0);
        }

        this.moveDirection.y -= this.gravity * Time.deltaTime;
        this.controller.Move(this.moveDirection * Time.deltaTime);
    }
}