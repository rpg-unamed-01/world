using UnityEngine;


public class Projectile : MonoBehaviour
{
   
    public Rigidbody rb;//variable delcariing
    public GameObject explosion;
    public LayerMask whatIsEnemies;
    public bool useGravity;
    public int explosionDamage;
    public int maxCollisions;
    public float maxLifetime;
    int collisions;
    PhysicMaterial physics_mat;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        if (collisions > maxCollisions) Explode();
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode()
    {
        //add damage here maybe
        Invoke("Delay", 0.05f);//delay to avoid bugs when deleting
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)//limit number of collisions and control when the explode
    {
        if (collision.collider.CompareTag("Projectile")) return;

        collisions++;
        if (collision.collider.CompareTag("Player")) Explode();
    }

    private void Setup()
    {//initialize physics materials and colliders
        physics_mat = new PhysicMaterial();
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        GetComponent<SphereCollider>().material = physics_mat;
        rb.useGravity = useGravity;
    }

}
