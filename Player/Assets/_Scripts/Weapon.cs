using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool melee;
    public float damage;
    public float knockback;
    public float range;
    public float fireRatePerFrame;
    public float totalAmmo;
    public float magazineAmmo;
    public float currentAmmo;

    public LayerMask enemy;

    public float xPos, yPos, zPos;
    public float xRotate, yRotate, zRotate;
    public float xScale = 1, yScale = 1 , zScale = 1;

    private Transform camera;
    private float coolDown;

    private void Awake()
    {
        camera = transform.parent;
        SetTransform();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Shoot();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Reload();
        }
    }

    private void Shoot() {
        if (coolDown <= 0)
        {
            coolDown = 0;
            if (Input.GetMouseButtonDown(0))
            {
                if (currentAmmo > 0 || melee)
                {
                    currentAmmo--;
                    Debug.Log("hey");
                    RaycastHit hit;
                    if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, enemy))
                    {
                        Debug.Log(hit.transform.name);
                        coolDown = fireRatePerFrame;
                        hit.collider.attachedRigidbody.AddForce(camera.transform.forward * knockback, ForceMode.VelocityChange);
                        hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }
        }
        else {
            coolDown--;
        }
    }

    private void Reload() {
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

    private void SetTransform() {
        transform.localPosition = new Vector3(xPos,yPos,zPos);
        transform.localRotation = Quaternion.Euler(xRotate, yRotate, zRotate);
        transform.localScale = new Vector3(xScale, yScale, zScale);
    }
}
