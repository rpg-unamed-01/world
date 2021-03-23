using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : Enemy
{
    public float dir;

    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject;
        dir = Random.value;
        if (dir < 0.5f)
        {
            dir = 45;
        }
        else
        {
            dir = -45;  
        }
        transform.localEulerAngles = new Vector3(0, 0, dir);
    }

    //private void Update()
    //{
    //    Move();
    //}

    //private void LateUpdate()
    //{
    //    checkWalls();
    //}
}