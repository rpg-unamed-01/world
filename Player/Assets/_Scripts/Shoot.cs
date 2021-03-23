using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject grenade;
    public Transform player;
    private Vector3 pos;
    public float cooldown = 1;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        shoot();
    }

    void shoot() {
        if (Input.GetMouseButtonDown(1) && timer <= 0) {
            timer = cooldown;
            pos = new Vector3(player.position.x, player.position.y, player.position.z);
            GameObject g = Instantiate(grenade, pos, Quaternion.identity);
            g.transform.forward = transform.forward;
        } 
    }
}
