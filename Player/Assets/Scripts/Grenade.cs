using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 10;
    private Vector3 direction;
    // Start is called before the first frame update

    void Start()
    {
        direction = transform.parent.forward;
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position += direction * speed * Time.deltaTime;
    }
}
