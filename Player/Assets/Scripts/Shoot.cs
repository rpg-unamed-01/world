using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject grenade;
    public Transform player;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void shoot() {
        if (Input.GetMouseButtonDown(0)) {
            pos = new Vector3(player.position.x, player.position.y, player.position.z);
            GameObject g = Instantiate(grenade, pos, Quaternion.identity);
            
        } 
    }
}
