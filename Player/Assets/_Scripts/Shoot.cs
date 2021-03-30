using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject grenade;
    public Camera camera;
    public Transform weaponHolder;
    public float grenadeCoolDown = 1;
    private float timer = 0;

    public KeyCode[] weaponSelect;
    private int currentWeapon = 0;

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
        int count = 0;
        foreach (KeyCode key in weaponSelect) {
            if (Input.GetKeyDown(key)) {
                weaponHolder.GetChild(currentWeapon).gameObject.SetActive(false);
                currentWeapon = count;
                break;
            }
        }
        weaponHolder.GetChild(currentWeapon).gameObject.SetActive(true);
    }
}
