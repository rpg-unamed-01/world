using UnityEngine;

public class Knight : Weapon
{
    public float grappleRange;
    public float grappleSpeed;
    public float enemyGrappleSpeed;
    public float groundPoundForce;
    public float tugForce;

    public LayerMask whatIsGrappleable;
    public LayerMask whatIsGroundPoundable;
    public Rigidbody rb;
    public Transform GrappleHolder;
    public Material grappleMaterial;

    public LineRenderer lineRenderer;

    public bool grappled = false;
    private Vector3 hitPoint;
    private Vector3 currentDistance;
    private Rigidbody target;
    private float radius;
    private bool hookedWall = false;
    public Enemy enemy;

    private void Update()
    {
        if (grappled)
        {
            Rigidbody r;
            lineRenderer.SetPosition(0, GrappleHolder.position);
            if (hookedWall)
            {
                lineRenderer.SetPosition(1, hitPoint);
                currentDistance = hitPoint - camera.position;
                r = rb;
            }
            else
            {
                lineRenderer.SetPosition(1, target.position);
                currentDistance = camera.position - target.position;
                r = target;
            }
            if (currentDistance.magnitude > radius)
            {
                Debug.Log(currentDistance);
                leftArmAnimator.Play("PullGrapple");
                Vector3 force = (currentDistance) - r.velocity;
                r.AddForce(force, ForceMode.VelocityChange);
            }
        }
        else if (lineRenderer != null) Destroy(lineRenderer);
    }

    public override void Special()
    {
        if (Input.GetMouseButtonDown(1)) {
            if (!grappled)
            {
                enemy = null;
                leftArmAnimator.Play("GrappleStart");
                RaycastHit hit;
                if (Physics.Raycast(camera.position, camera.forward, out hit, grappleRange, whatIsGrappleable))
                {
                    grappled = true;
                    target = hit.collider.attachedRigidbody;
                    hitPoint = hit.point;
                    radius = hit.distance * 1.5f;
                    radius = radius > grappleRange ? grappleRange : radius;
                    radius = radius < 3 ? 3 : radius;

                    lineRenderer = camera.gameObject.AddComponent<LineRenderer>();
                    lineRenderer.startColor = Color.black;
                    lineRenderer.endColor = Color.black;
                    lineRenderer.material = grappleMaterial;
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.useWorldSpace = true;
                    lineRenderer.gameObject.layer = default;

                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    {
                        hookedWall = true;
                        Debug.Log(hookedWall);
                    }
                    else {
                        enemy = hit.collider.GetComponentInParent<Enemy>();
                        enemy.GetGrappled();
                    }
                }
                else {
                    leftArmAnimator.Play("GrappleEnd");
                }
                  
            }
            else
            {
                grappled = false;

                Vector3 direction;
                if (!hookedWall)
                {
                    enemy.grappled = false;
                    direction = camera.position - target.transform.position;

                    target.AddForce(direction * enemyGrappleSpeed, ForceMode.VelocityChange);
                }
                else
                {
                    direction = hitPoint - camera.position;
                    rb.AddForce(direction * grappleSpeed, ForceMode.VelocityChange);
                }
                leftArmAnimator.Play("GrappleEnd");
                hookedWall = false;
            }
        }
    }

    public override void Ability(PlayerController player) {
        Collider[] collisions = Physics.OverlapSphere(transform.position, 10, whatIsGroundPoundable);
        float dist;
        foreach (var collision in collisions) {
            dist = Vector3.Magnitude(collision.transform.position - transform.position);
            collision.attachedRigidbody.AddForce(Vector3.up*groundPoundForce/dist, ForceMode.VelocityChange);
        }
    }
}
