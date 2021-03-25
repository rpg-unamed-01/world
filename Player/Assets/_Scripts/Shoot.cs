using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject grenade;
    public Camera camera;
    public Transform weaponHolder;
    public float grenadeCoolDown = 1;
    private float timer = 0;

    public GameObject[] weapons;

    private int currentWeapon = 0;
    private void Awake()
    {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i] = Instantiate(weapons[i], weaponHolder);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        SelectWeapon();
        ShootGrenade();
    }

    void ShootGrenade() {
        if (Input.GetMouseButtonDown(1) && timer <= 0) {
            timer = grenadeCoolDown;
            Vector3 pos;
            pos = camera.transform.position;

            GameObject g = Instantiate(grenade, pos, Quaternion.identity);
            g.transform.forward = camera.transform.forward;
        } 
    }

    public void SelectWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeapon = 0;
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
            weapons[0].SetActive(false);
            weapons[2].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
        }
        weapons[currentWeapon].SetActive(true);
    }
}
