using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    public float airSpeed;
    public float sensitivity;
    public float jumpForce;

    public KeyCode sprint;
    public KeyCode perspective;
    public KeyCode jump;


    public Rigidbody rb;
    public Camera camera;
    public Camera perspectiveCamera;
    public Transform head;
    public Transform feet;
    public LayerMask ground;

    private float xRotate = 0f;
    private bool toggleSprint = false;
    public bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGrounded();
        Jump();
        TogglePerspective();
        LookAround();
        MoveAround();
    }

    void LookAround()
    {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity;

        xRotate -= mouse.y;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        
        head.transform.localEulerAngles = new Vector3(xRotate, 0f, 0f);
        head.Rotate(Vector3.up * mouse.x);
    }

    void MoveAround() {

        //Toggle Sprint
        if (Input.GetKey(sprint) && Input.GetKey(KeyCode.W)) {
            toggleSprint = true;
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            toggleSprint = false;
        }

        Vector3 move;
        move = head.forward * Input.GetAxis("Vertical") + head.right * Input.GetAxis("Horizontal");
        move = move.normalized * walkSpeed * Time.fixedDeltaTime;
        if (!grounded)
        {
            move *= airSpeed;
        }
        if (toggleSprint) {
            move *= sprintSpeed/walkSpeed;
        }
        rb.MovePosition(transform.position + move);
    }

    void TogglePerspective() {
        if (Input.GetKey(perspective))
        {
            camera.enabled = false;
            perspectiveCamera.enabled = true;
        }
        else {
            camera.enabled = true;
            perspectiveCamera.enabled = false;
        }
    }

    private void CheckGrounded() {
        if (Physics.OverlapSphere(feet.position, feet.localScale.x, ground).Length > 0)
        {
            grounded = true;
        }
        else {
            grounded = false;
        }
    }

    private void Jump() {
        if (grounded && Input.GetKeyDown(jump)) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            grounded = false;
        }
    }
}
