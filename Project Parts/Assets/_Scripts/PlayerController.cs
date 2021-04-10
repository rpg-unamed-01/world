using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool grounded = false;
    public bool abilityReady;

    public float sensitivity = 10;
    public float walkSpeed = 5;
    public float sprintSpeed = 8;
    public float airSpeed = 2;
    public float jumpHeight = 4;
    public float gravity = -15f;

    public KeyCode sprint;
    public KeyCode perspective;
    public KeyCode jump;
    public KeyCode reload;
    public KeyCode useItem;
    public KeyCode useAbility;

    public float maxHealth = 100;
    public float health;
    public int money = 200;
    public float fistRange = 3;
    public bool displayMenu = false;
    public bool displayShop = false;
    public bool displayLevelMenu = false;
    public bool displayMainMenu = false;
    public bool inLevel = true;
    public bool invisible = false;
    public bool speedier = false;
    public bool jumpier = false;
    public bool stronger = false;
    public bool slower = false;

    public int selectedItemIndex;

    public Weapon myscript;
    public ItemHolder items;
    public Shop shopController;
    public GameObject selectedItem;
    public GameObject holster;
    public Text currentItem;

    public GameObject ammo;
    public Text currentAmmo;
    public Text totalAmmo;

    public GameObject invisStatus;
    public GameObject speedStatus;
    public GameObject jumpStatus;
    public GameObject strengthStatus;
    public GameObject slowStatus;

    public GameObject abilityImage;

    public LayerMask ground;
    public LayerMask wall;
    public LayerMask shop;

    public Animator animator;

    public Rigidbody rb;
    public Transform head;
    public Transform feet;

    public Camera camera;
    public Camera perspectiveCamera;
    
    private float xRotate = 0f;
    private bool toggleSprint = false;
    private float invisCoolDown;
    private float speedCoolDown;
    private float jumpCoolDown;
    private float slowCoolDown;
    private float strengthCoolDown;
    private Vector3 velocityChange;

    public virtual void Awake()
    {
        abilityImage.SetActive(false);
        //selectedItem = new GameObject();
        health = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        myscript = rb.gameObject.GetComponent<Weapon>();
        if (!myscript.melee) {
            ammo.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        invisStatus.SetActive(invisible);
        speedStatus.SetActive(speedier);
        jumpStatus.SetActive(jumpier);
        strengthStatus.SetActive(stronger);
        slowStatus.SetActive(slower);

        invisStatus.GetComponent<Text>().text = Mathf.Round(invisCoolDown * 100)/100 + "s";
        speedStatus.GetComponent<Text>().text = Mathf.Round(speedCoolDown * 100) / 100 + "s";
        jumpStatus.GetComponent<Text>().text = Mathf.Round(jumpCoolDown * 100) / 100 + "s";
        strengthStatus.GetComponent<Text>().text = Mathf.Round(strengthCoolDown * 100) / 100 + "s";
        slowStatus.GetComponent<Text>().text = Mathf.Round(slowCoolDown * 100) / 100 + "s";

        if (invisible) { 
            invisCoolDown -= Time.deltaTime;
            if (invisCoolDown <= 0)
            {
                invisCoolDown = 0;
                invisible = false;
            }
        }

        if (speedier)
        {
            speedCoolDown -= Time.deltaTime;
            if (speedCoolDown <= 0)
            {
                speedCoolDown = 0;
                speedier = false;
            }
        }

        if (jumpier)
        {
            jumpCoolDown -= Time.deltaTime;
            if (jumpCoolDown <= 0)
            {
                jumpCoolDown = 0;
                jumpier = false;
            }
        }

        if (stronger)
        {
            strengthCoolDown -= Time.deltaTime;
            if (strengthCoolDown <= 0)
            {
                strengthCoolDown = 0;
                stronger = false;
            }
        }

        if (slower)
        {
            slowCoolDown -= Time.deltaTime;
            if (slowCoolDown <= 0)
            {
                slowCoolDown = 0;
                slower = false;
            }
        }

        if (currentAmmo != null) currentAmmo.text = "" + myscript.currentAmmo;
        if (totalAmmo != null) totalAmmo.text = "" + myscript.totalAmmo;

        CheckShop();
        ToggleMenu();
        CheckGrounded();
        if (!displayMenu)
        {
            if (health > 30) abilityReady = true;
            else if (abilityReady) {
                abilityImage.SetActive(true);
                if (Input.GetKeyDown(useAbility)) {
                    myscript.Ability(this);
                    abilityImage.SetActive(false);
                    abilityReady = false;
                }
            }
            if (Input.GetKeyDown(reload)) myscript.Reload();
            myscript.Attack(stronger, this);
            myscript.Special();
            UseItem();
            TogglePerspective();
            MouseMove();
            KeyMove();
        }
    }

    private void CheckShop() {
        if (!displayMenu)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, fistRange, shop))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    items.PopulateItems();
                    displayMenu = true;
                    displayShop = true;
                    displayLevelMenu = false;
                    displayMainMenu = false;
                    items.SetShopText();
                    shopController.shopItemName.text = "No Item Selected";
                    shopController.money.text = "" + money;
                }
            }
        }
    }

    private void LateUpdate()
    {
        Die();
    }

    private void MouseMove() {
        Vector2 move = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity;
        xRotate -= move.y;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        camera.transform.localEulerAngles = new Vector3(xRotate, 0f, 0f);
        head.Rotate(Vector3.up * move.x);
    }

    private void KeyMove() {
        //Toggle Sprint
        if (Input.GetKey(sprint) && Input.GetKey(KeyCode.W))
        {
            toggleSprint = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            toggleSprint = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (toggleSprint)
            {
                animator.Play("Run");
            }
            else
            {
                animator.Play("Walk");
            }

        }
        else {
            animator.Play("idle1");
        }

        Vector3 targetVelocity;
        targetVelocity = head.forward * Input.GetAxis("Vertical") + head.right * Input.GetAxis("Horizontal");
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity = targetVelocity.normalized;

        Vector3 currentVelocity = rb.velocity;
        
        float maxVelocityChange;
        if (toggleSprint)
        {
            targetVelocity *= sprintSpeed;
            maxVelocityChange = sprintSpeed;
        }
        else
        {
            targetVelocity *= walkSpeed;
            maxVelocityChange = walkSpeed;
        }

        velocityChange = targetVelocity - currentVelocity;
        if (speedier)
        {
            maxVelocityChange *= 2;
            velocityChange = targetVelocity * 2 - currentVelocity;
        }
        if (slower)
        {
            maxVelocityChange *= 0.5f;
            float factor = speedier ? 1 : 0.5f;
            velocityChange = targetVelocity * factor - currentVelocity;
        }
        if (grounded)
        {
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            CheckWalls();
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
            Jump();
        }
        else
        {
            velocityChange = targetVelocity - currentVelocity;
            velocityChange.Normalize();
            velocityChange *= airSpeed;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            CheckWalls();
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
    }

    private void CheckWalls() {
        RaycastHit hit;
        float zDir = velocityChange.z / Mathf.Abs(velocityChange.z);
        float xDir = velocityChange.x / Mathf.Abs(velocityChange.x);
        if (rb.SweepTest(Vector3.forward * zDir, out hit, rb.transform.localScale.z / 2))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            velocityChange.z = 0;
        }

        if (rb.SweepTest(Vector3.right * xDir, out hit, rb.transform.localScale.x / 2))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            velocityChange.x = 0;
        }
    }

    private void TogglePerspective()
    {
        if (Input.GetKey(perspective))
        {
            camera.enabled = false;
            perspectiveCamera.enabled = true;
        }
        else
        {
            camera.enabled = true;
            perspectiveCamera.enabled = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(jump))
        {
            Debug.Log(health);
            float jumpForce = Mathf.Sqrt(2 * jumpHeight * -gravity);
            if (jumpier) {
                jumpForce *= 2;
            }
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void CheckGrounded()
    {
        grounded = false;
        Vector3 overlapBoxScale = new Vector3(rb.transform.localScale.x / 2, rb.transform.localScale.y / 10, rb.transform.localScale.z / 2);
        if (Physics.OverlapBox(feet.position, overlapBoxScale, Quaternion.identity, ground).Length > 0)
        {
            grounded = true;
        }
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            Die();
        }
    }

    private void Die() {
        
    }

    private void ToggleMenu() {
        //Add raycast check with a shopkeeper prefab to open shop menu

        if (Input.GetKeyDown(KeyCode.Q)) {
            displayMenu = !displayMenu;
            displayLevelMenu = false;
            displayMainMenu = false;
            displayShop = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (displayMenu)
            {
                Cursor.lockState = CursorLockMode.None;
                if (inLevel)
                {
                    displayLevelMenu = true;
                    items.PopulateItems();
                    if (items.currentItem == null)
                    {
                        currentItem.text = "No Item Selected";
                    }
                    else {
                        currentItem.text = items.currentItem.GetComponent<Item>().getName();
                    }
                }
                else
                {
                    displayMainMenu = true;
                }
            }
            Cursor.visible = displayMenu;
        }
    }

    public void AddHealth(float hp) {
        health += hp;
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    public void GoInvisible(float time) {
        invisCoolDown = time;
        invisible = true;
    }

    public void GoFast(float time) {
        speedCoolDown = time;
        speedier = true;
    }

    public void JumpHigh(float time) {
        jumpCoolDown = time;
        jumpier = true;
    }

    public void HitHard(float time) {
        strengthCoolDown = time;
        stronger = true;
    }

    public void UseItem() {
        if (Input.GetKeyDown(useItem) && items.currentItem != null) {
            selectedItem = items.currentItem;
            selectedItemIndex = items.index;
            selectedItem.GetComponent<Item>().Use(this);
            items.items[items.index] = null;
            items.currentItem = null;
            foreach (Transform child in holster.transform) {
                Destroy(child.gameObject);
            }
            selectedItem = items.currentItem;
            currentItem.text = "No Item Selected";
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == ground) {
            grounded = true;
        }
    }

    public void GetSlow(float time) {
        slowCoolDown = time;
        slower = true;
    }
}
