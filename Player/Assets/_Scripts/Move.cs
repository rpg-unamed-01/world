using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera playerCamera;
    public Camera perspectiveCamera;
    public CharacterController character;
    public Transform head;
    public GameObject explosion;
    public Rigidbody player;
    public static float sensitivity = 400f;
    private float xRotate = 0f;
    public bool mouseLock = true;
    public static float speed = 0.1f;
    public static float gravity = -40f;
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

        Perspective();

        Sprint();

        Jump(); 

    }

    private void FixedUpdate()
    {
        Gravity();
    }

    private void Jump() {
        grounded = checkGrounded();
        if (Input.GetKeyDown(KeyCode.F) && grounded)
        {
            yVelocity = jump;
            grounded = false;
        }
        character.Move(Vector3.up * yVelocity * Time.deltaTime);
    }

    void Perspective() {
        if (Input.GetKeyDown(togglePerspective))
        {
            playerCamera.enabled = false;
            perspectiveCamera.enabled = true;
        }
        if (Input.GetKeyUp(togglePerspective))
        {
            playerCamera.enabled = true;
            perspectiveCamera.enabled = false;
        }
    }

    void Sprint() {
        if (sprintToggle && (Input.GetKeyUp(KeyCode.W)))
        {
            sprintToggle = false;
        }
        if (Input.GetKey(toggleSprint))
        {
            sprintToggle = true;
        }

        if (sprintToggle)
        {
            UpdatePlayerWalk(speed * 2);
        }
        else
        {
            UpdatePlayerWalk(speed);
        }
    }

    void UpdateMouseMove() {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))*sensitivity*Time.deltaTime;
        xRotate -= mouse.y;

        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        transform.Rotate(Vector3.up * mouse.x);
        head.transform.localEulerAngles = new Vector3(xRotate, 0f, 0f);
    }

    bool checkGrounded() {
        Collider[] groundCollision = Physics.OverlapSphere(feet.position, 0.2f, ground);
        grounded = false;
        if (groundCollision.Length > 0) {
            grounded = true;
            yVelocity = 0;
        }

        return grounded;
    }

    void UpdatePlayerWalk(float spd) {
        Vector3 move;
        move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        character.Move(move * spd);
    }


    void Gravity() {
        if (!grounded) {
            yVelocity += gravity * Time.fixedDeltaTime;
        }
    }
}
