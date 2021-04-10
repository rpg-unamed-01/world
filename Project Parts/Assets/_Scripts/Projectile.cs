using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public LayerMask whatProjectileHits;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);
        transform.position += transform.forward * speed * Time.deltaTime;
        Debug.Log(" " + transform.position);
        CheckCollision();
    }

    void CheckCollision()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, transform.localScale.x, whatProjectileHits);
        if (collisions.Length > 0)
        {
            OnHit();
        }
    }

    public virtual void OnHit()
    {
    }
}
