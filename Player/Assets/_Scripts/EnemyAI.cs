
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;//declaring public variables
    public float health;
    //declaring patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    // declaring attacking variables
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    //declaring enemy state variables
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;//find the player from gameobject name
        agent = GetComponent<NavMeshAgent>();//initialize the agent
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);//check if the player is in sight ranger
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);//check if the player is in attack ranger
        if (!playerInSightRange && !playerInAttackRange) Patroling();//patrolling state
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();//chasing state
        if (playerInAttackRange && playerInSightRange) AttackPlayer();//attacking state
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();//random walking for enemy

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)//if enemy reaches the set walkpoint
            walkPointSet = false;//find new walk point
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        //random value for the walkpoint
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))//make sure walk point is in map
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);//follow until enemy reaches the player
    }

    private void AttackPlayer()
    {
        //doesnt move on attack
        agent.SetDestination(transform.position);
        //enemy faces player
        transform.LookAt(player);
        //if its not already attacking
        if (!alreadyAttacked)
        {
            //create projectile
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
           
            alreadyAttacked = true;
            //reset the attack with a delay of timeBetweenAttack
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;//damge the enemy ( not working)
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);//destroy enemy when health = 0
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {//not working
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);//set colour to red when in attack range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);//set colour to yellow when player is seen
    }
}
