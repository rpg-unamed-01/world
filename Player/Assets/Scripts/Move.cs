using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera playerCamera;
    public Camera perspectiveCamera;
    public CharacterController character;
    public Transform head;
    public static float sensitivity = 400f;
    private float xRotate = 0f;
    public bool mouseLock = true;
    public static float speed = 0.1f;
    public static float gravity = -20f;
    public float yVelocity = 0f;
    public static float jump = 10f;
    public bool grounded;
    public Transform feet;
    public LayerMask ground;
    public static bool sprintToggle = false;
    public KeyCode togglePerspective = KeyCode.Space;
    public KeyCode toggleSprint = KeyCode.LeftShift;


    void Start()
    {
        playerCamera.enabled = true;
        perspectiveCamera.enabled = false;
        if (mouseLock) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseMove();


        if (Input.GetKeyDown(togglePerspective)) {
            playerCamera.enabled = false;
            perspectiveCamera.enabled = true;
        }
        if (Input.GetKeyUp(togglePerspective)) {
            playerCamera.enabled = true;
            perspectiveCamera.enabled = false;
        }

        if (sprintToggle && (Input.GetKeyUp(KeyCode.W))) {
            sprintToggle = false;
        }
        if (Input.GetKey(toggleSprint)) {
            sprintToggle = true;
        }

        if (sprintToggle) { 
            UpdatePlayerWalk(speed * 2);
        } else {

        }  UpdatePlayerWalk(speed);

        yVelocity += gravity * Time.deltaTime;
        if (checkGrounded())
        {
            yVelocity = 0;
        }


        if (Input.GetKeyDown(KeyCode.F) && grounded)
        {
            yVelocity = jump;
            grounded = false;
        }

        character.Move(Vector3.up * yVelocity * Time.deltaTime);

    }

    void UpdateMouseMove() {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))*sensitivity*Time.deltaTime;
        xRotate -= mouse.y;

        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        this.transform.Rotate(Vector3.up * mouse.x);
        head.transform.localEulerAngles = new Vector3(xRotate, 0f, 0f);
    }

    bool checkGrounded() {
        Collider[] groundCollision = Physics.OverlapSphere(feet.position, 0.2f, ground);
        Debug.Log(feet.position);
        Debug.Log(groundCollision.Length);
        grounded = false;
        if (groundCollision.Length > 0) {
            grounded = true;
        }

        return grounded;
    }

    void UpdatePlayerWalk(float spd) {
        Vector3 move;
        move = this.transform.forward * Input.GetAxis("Vertical") + this.transform.right * Input.GetAxis("Horizontal");
        character.Move(move * spd);
    }
}
