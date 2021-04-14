using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Chaser : Enemy
{
    public float chaseSpeed;
    public float hitRange;
    public float attackCoolDown;
    public LightningBoltScript lightning;

    private float currentAttackCoolDown;

    public override void Start()
    {
        base.Start();
        lightning = GetComponent<LightningBoltScript>();
        lightning.SetEndPoint(player.gameObject);
    }

    public override void Update()
    {
        base.Update();
        currentAttackCoolDown -= Time.deltaTime;
    }

    public override void Attack()
    {
        if (playerDir.magnitude > 2)
        {
            MoveTowards(playerDir, chaseSpeed);
        }
        HitPlayer();
    }

    private void HitPlayer() {
        Collider[] hit = Physics.OverlapSphere(rb.transform.position, hitRange, playerLayer);
        if (hit.Length > 0 && currentAttackCoolDown <= 0)
        {
            currentAttackCoolDown = attackCoolDown;
            player.rb.AddForce(playerDir + Vector3.up, ForceMode.VelocityChange);
            player.TakeDamage(damage);
            lightning.Trigger();
        }
    }

    public override void CheckGrounded()
    {
        grounded = false;
        float overlapSphereScale = transform.localScale.x/5;
        if (Physics.OverlapSphere(transform.position, overlapSphereScale, ground).Length > 0) 
        {
            grounded = true;
        }
    }
}
