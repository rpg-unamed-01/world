using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool melee;
    public bool damage;
    public float knockback;
    public float range;
    public float fireRatePerFrame;
    public float totalAmmo;
    public float magazineAmmo;
    public float currentAmmo;

    public Transform head;
    public LayerMask enemy;

    private float coolDown;

    public void Shoot() {
        if (coolDown <= 0)
        {
            coolDown = 0;
            if (currentAmmo > 0)
            {
                currentAmmo--;
                RaycastHit hit;
                if (Physics.Raycast(head.position, head.forward, out hit, range, enemy))
                {
                    coolDown = fireRatePerFrame;
                    hit.collider.attachedRigidbody.AddForce(head.forward * knockback, ForceMode.VelocityChange);
                    if (hit.collider.tag == "Enemy") {
                        //Put Enemy Script Call here to add damage.
                    }
                }
            }
        }
        else {
            coolDown--;
        }
    }

    public void Reload() {
        if (!melee)
        {
            float drawAmmo = magazineAmmo - currentAmmo;
            if (drawAmmo >= totalAmmo)
            {
                currentAmmo += totalAmmo;
                totalAmmo = 0;
            }
            else
            {
                currentAmmo += drawAmmo;
                totalAmmo -= drawAmmo;
            }
        }
    }
}
