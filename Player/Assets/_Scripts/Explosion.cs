using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject player;
    private Vector3 hitForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == player.layer)
        {
            Vector3 pos = other.transform.position;
            Vector3 pos2 = transform.position;
            hitForce = pos - pos2;
            Debug.Log(hitForce);
        }
    }
}
