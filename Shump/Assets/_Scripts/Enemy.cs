using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static BoundaryCheck myscript = new BoundaryCheck();
    protected GameObject enemy;
    public float speed = 0.1f;
    public float r = -2.5f;
    // Update is called once per frame

    void Start()
    {
        myscript.Initiate(r);
    }

    public void Update()
    {
        Vector3 move = enemy.transform.up * speed;
        enemy.transform.position -= move;
    }

    public void LateUpdate()
    {
        myscript.CheckBounds(enemy);

        if (myscript.hitBounds && !myscript.hitTop)
        {
            Destroy(enemy);
        }
    }
}
