using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject player;
    public float force;
    private Vector3 hitForce;

    private void Start()
    {
        Invoke("EndExplosion", 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == player.layer)
        {
            float expForce = 0; 
            Vector3 pos = other.transform.position;
            Vector3 pos2 = transform.position;
            hitForce = pos - pos2;
            expForce = force / Mathf.Pow(hitForce.magnitude, 2);
            other.attachedRigidbody.AddForce(hitForce * expForce, ForceMode.Impulse);
        }
    }

    private void EndExplosion() {
        Destroy(gameObject);
    }
}
