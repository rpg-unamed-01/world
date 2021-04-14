using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool patrols;
    public bool grappled = false;
    public bool grounded = false;
    public float health = 100;
    public float damage;
    public float range;
    public float speed;
    public float gravity = -15f;
    public float grappleEscapeCoolDown = 3;

    public Vector3[] patrolPoints;

    public PlayerController player;
    public Knight knight;
    public LayerMask ground;
    public LayerMask playerLayer;
    public Transform bottom;
    public Animator animator;
    public Slider healthBar;
    public Transform model;


    public Rigidbody rb;
    public Vector3 playerDir;
    public float playerDist;

    private Vector3 patrolDir;
    private float pointAllowance = 0.5f;

    private int currentPoint = 0;
    private float escapeCoolDown;
    private float currentHealth;
    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        healthBar.value = currentHealth / health;
        escapeCoolDown -= Time.deltaTime;
        if (escapeCoolDown <= 0) EscapeGrapple();

        CheckGrounded();
        if (!grounded)
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
            if (patrols)
            {
                animator.SetBool("Walk_Anim", false);
            }
        }
        
        rb.velocity -= new Vector3(rb.velocity.x * Time.deltaTime, 0, rb.velocity.z * Time.deltaTime);
    }

    public virtual void LateUpdate()
    {
        playerDir = player.transform.position - transform.position;
        playerDist = playerDir.magnitude;

        if (grounded) {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);
            if (patrols)
            {
                animator.SetBool("Walk_Anim", true);
            }
            if (!grappled || knight == null)
            {
                if (player.invisible || playerDist > range)
                {
                    Patrol();
                }
                else if (playerDist < range)
                {
                    Attack();
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == ground)
        {
            grounded = true;
        }
    }

    public void GetGrappled() {
        if (patrols)
        {
            animator.SetBool("Walk_Anim", false);
        }
        grappled = true;
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        escapeCoolDown = grappleEscapeCoolDown;
    }

    private void EscapeGrapple() {
        if (grappled) {
            grappled = false;
            knight.enemy = null;
            knight.grappled = false;
            knight.leftArmAnimator.Play("GrappleEnd");
        }
    }

    public virtual void CheckGrounded()
    {
        grounded = false;
        Vector3 overlapBoxScale = new Vector3(rb.transform.localScale.x / 10, rb.transform.localScale.y / 5, rb.transform.localScale.z / 2);
        if (Physics.OverlapBox(bottom.position, overlapBoxScale, Quaternion.identity, ground).Length > 0)
        {
            grounded = true;
        }
    }

    private void Patrol() {
        if (patrols) {
            patrolDir = patrolPoints[currentPoint] - transform.position;
            if (patrolDir.magnitude < pointAllowance) {
                currentPoint = (currentPoint+1)%patrolPoints.Length;
                patrolDir = patrolPoints[currentPoint] - transform.position;
            }
            MoveTowards(patrolDir, speed);
        }
    }

    public void MoveTowards(Vector3 pos, float velocity) {
        pos.Normalize();
        pos *= velocity;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Vector3 velocityChange = pos - rb.velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -velocity/2, velocity/2);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -velocity/2, velocity/2);
        Vector3 lookDir = new Vector3(playerDir.x, 0, playerDir.z);
        model.forward = lookDir;
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public virtual void Attack() {

    }

    public void TakeDamage(float hp) {
        EscapeGrapple();
        currentHealth -= hp;
        if (currentHealth < 0) Die();
    }

    public void Die() {
        EscapeGrapple();
        player.money += 10;
        Destroy(gameObject);
    }

    public void SetPlayer(GameObject character) {
        player = character.GetComponent<PlayerController>();
        knight = character.GetComponent<Knight>();
    }
}
