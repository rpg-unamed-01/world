using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private BoundaryCheck myscript = new BoundaryCheck();
    public float speed = 0.2f;
    public float angleX = 20f;
    public float angleY = 45f;
    public float angleZ = 20f;
    public float r = 2.5f;
    public float health = 100;
    public float h;
    public float w;
    public float x;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        myscript.Initiate(r);
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    private void LateUpdate()
    {
        bool hitBound = myscript.CheckBounds(gameObject);
        h = myscript.h;
        w = myscript.w;
        x = myscript.x;
        y = myscript.y;
        if (hitBound)
        { 
            Vector3 pos = transform.position;
            if (myscript.hitLeft) {
                pos.x = -w + r;
            }
            else if (myscript.hitRight)
            {
                pos.x = w - r;
            }
            if (myscript.hitTop)
            {
                pos.y = h - r;
            }
            else if (myscript.hitBottom)
            {
                pos.y = -h + r;
            }
            transform.position = pos;
        }
        if (CheckCollision()) {
            Destroy(gameObject);
        }
    }

    void movePlayer() {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);

        transform.position += move * speed;
        transform.rotation = Quaternion.Euler(angleX * move.y, angleY * -move.x, angleZ * -move.x);
    }

    bool CheckCollision() {
        Collider[] enemys = Physics.OverlapSphere(transform.position, r, LayerMask.GetMask("Enemy"));
        foreach (var enemy in enemys) {
            Destroy(enemy.gameObject.transform.parent.gameObject);
            return true;
        }
        return false;
    }
}
