using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{ 
    public bool melee;
    public float damage;
    public float knockback;
    public float range;
    public float fireRatePerSecond;
    public float totalAmmo;
    public float magazineAmmo;
    public float currentAmmo;

    public Animator leftArmAnimator;
    public Animator rightArmAnimator;
    public Animator animator;
    public LayerMask enemies;
    public LayerMask wall;

    public Transform camera;
    private float coolDown;


    public void Attack(bool stronger, PlayerController player)
    {
        if (coolDown <= 0)
        {
            coolDown = 0;
            if (Input.GetMouseButtonDown(0))
            {
                coolDown = 60/fireRatePerSecond;
                if (currentAmmo > 0 || melee)
                {
                    if (melee)
                    {
                        rightArmAnimator.Play("MeleeSwing");
                    }
                    else
                    {
                        rightArmAnimator.Play("GunShot");
                    }
                    currentAmmo--;
                    RaycastHit hit;
                    if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, enemies))
                    {
                        float hitKnockback = knockback;
                        if (stronger)
                        {
                            hitKnockback *= 2;
                        }
                        hit.collider.attachedRigidbody.AddForce(camera.transform.forward * hitKnockback - Vector3.up*hitKnockback*camera.transform.forward.y, ForceMode.VelocityChange);
                        hit.collider.GetComponentInParent<Enemy>().TakeDamage(damage);
                    }
                }
            }
        }
        else
        {
            coolDown -= 1;
        }
    }

    public virtual void Special() {

    }

    public virtual void Ability(PlayerController player) { }

    public void Reload()
    {
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