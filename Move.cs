using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerCamera;
    public CharacterController character;
    public float sensitivity;
    private float xRotate = 0f;
    public bool mouseLock = true;
    public float speed = 1f;
    public float gravity = -9.81f;
    public float yVelocity = 0f;
    public bool grounded;
    public Transform feet;
    public LayerMask ground;
    public float jump = 5f;

    void Start()
    {
        if (mouseLock) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void FixedUpdate()
    {
        yVelocity += gravity * Time.deltaTime;
        if (checkGrounded())
        {
            yVelocity = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseMove();
        UpdatePlayerWalk();
        

        if (grounded && Input.GetKeyDown(KeyCode.Space)) {
            yVelocity = jump;
        }

        character.Move(Vector3.up * yVelocity * Time.deltaTime);
         
    }

    void UpdateMouseMove() {
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))*sensitivity*Time.deltaTime;
        xRotate -= mouse.y;

        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        this.transform.Rotate(Vector3.up * mouse.x);
        playerCamera.transform.localEulerAngles = new Vector3(xRotate, 0f, 0f);
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

    void UpdatePlayerWalk() {
        Vector3 move = this.transform.forward * Input.GetAxis("Vertical") +this.transform.right * Input.GetAxis("Horizontal");

        character.Move(move*speed);
    }
}
