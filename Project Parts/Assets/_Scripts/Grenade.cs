using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    public GameObject explosion;

    private void Awake()
    {
        Invoke("OnHit", 3f);
    }

    public override void OnHit() {
        GameObject g = Instantiate(explosion, transform.position, Quaternion.identity);
        g.transform.position = transform.position;
        Destroy(gameObject);
    }
}
