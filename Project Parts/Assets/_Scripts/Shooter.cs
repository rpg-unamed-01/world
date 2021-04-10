using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    public GameObject bullet;
    public Transform muzzle;
    public float attackCoolDown;
    private float currentCoolDown;

    private GameObject g;
    public override void Update()
    {
        base.Update();
        currentCoolDown -= Time.deltaTime;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        rb.velocity -= new Vector3(rb.velocity.x * Time.deltaTime, 0, rb.velocity.z * Time.deltaTime);
    }

    public override void Attack()
    {
        model.forward = playerDir;
        if (Physics.Raycast(transform.position, playerDir, range, playerLayer) && currentCoolDown <= 0) {
            currentCoolDown = attackCoolDown;
            g = Instantiate(bullet, muzzle.position, Quaternion.identity);
            g.transform.forward = playerDir;
            g.GetComponent<TurretBullet>().SetDamage(damage);
        }
    }
}
