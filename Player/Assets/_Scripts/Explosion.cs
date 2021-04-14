using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public LayerMask hittable;
    public float force;
    private Vector3 hitForce;

    private void Start()
    {
        Invoke("EndExplosion", 0.5f);
    }

    private void Awake()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f, hittable);
        foreach (var collider in colliders) {
            float expForce;
            Vector3 pos = collider.transform.position;
            Vector3 pos2 = transform.position;
            hitForce = pos - pos2;
            expForce = force / Mathf.Pow(hitForce.magnitude, 2);
            Debug.Log(hitForce);
            collider.attachedRigidbody.AddForce(hitForce.normalized * expForce, ForceMode.VelocityChange);
        }
    }

    private void EndExplosion()
    {
        Destroy(gameObject);
    }
}
