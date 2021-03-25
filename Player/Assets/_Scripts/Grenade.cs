using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 100;
    public LayerMask grenade;
    public LayerMask explode;
    public GameObject explosion;

    // Update is called once per frame

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        checkHit();
    }

    void checkHit() {
        Collider[] groundCollision = Physics.OverlapSphere(transform.position, 0.2f, grenade);
        if (groundCollision.Length > 0) {
            GameObject g = Instantiate(explosion, transform.position, Quaternion.identity);
            g.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
