using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float force = 5;

    public GameObject player;
    private Vector3 hitForce;

    private void Start()
    {
        Invoke("EndExplosion", 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == player.layer)
        {
            Vector3 pos = other.transform.position;
            Vector3 pos2 = transform.position;
            hitForce = pos - pos2;

            float expForce = force / Mathf.Pow(hitForce.magnitude, 2);
            Debug.Log(expForce);
            other.attachedRigidbody.AddForce(hitForce * expForce, ForceMode.VelocityChange);
        }
    }

    public void EndExplosion() {
        Destroy(gameObject);
    }
}
