using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : Projectile
{
    public LayerMask player;
    public float slowTime;

    private float damage;
    public PlayerController pController;

    private void Start()
    {
        Invoke("End", 10);
    }

    public override void OnHit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x, player);
        foreach (var collider in colliders) {
            pController = collider.gameObject.GetComponentInParent<PlayerController>();
            if (pController == null) return;
            pController.GetSlow(slowTime);
            pController.TakeDamage(damage);
            End();
            break;
        }
    }

    private void End() {
        Destroy(gameObject);
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }
}
