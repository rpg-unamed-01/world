using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed;

    public GameObject explosion;
    public LayerMask ground;

    private float maxDistance;
    private Vector3 startPoint;

    private void Start()
    {
        startPoint = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, ground)) {
            maxDistance = hit.distance;
        }
        
    }

    private void FixedUpdate()
    {
        Move();
        checkHit();
        checkDistance();
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ground) {
            Instantiate(explosion, transform.position, Quaternion.identity); 
        }
    }

    private void checkHit() {
        if (Physics.OverlapSphere(transform.position, transform.localScale.x, ground).Length > 0) {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void checkDistance() {
        Vector3 distVector = transform.position - startPoint;
        if (distVector.magnitude >= maxDistance) {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
