using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject grenade;
    public Camera camera;
    public GameObject[] weapons;

    public float cooldown = 1;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        shootGrenade();
    }

    void shootGrenade() {
        if (Input.GetMouseButtonDown(1) && timer <= 0) {
            timer = cooldown;
            Vector3 pos;
            pos = camera.transform.position;

            GameObject g = Instantiate(grenade, pos, Quaternion.identity);
            g.transform.forward = camera.transform.forward;
        } 
    }
}
