using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCheck : MonoBehaviour
{
    public float r;

    public float w;
    public float h;
    public float x;
    public float y;

    public bool hitBottom = false;
    public bool hitTop = false;
    public bool hitBounds = false;
    public bool hitRight = false;
    public bool hitLeft = false;

    // Start is called before the first frame update
    public void Initiate(float radius)
    {
        r = radius;
        h = Camera.main.orthographicSize;
        w = h * Camera.main.aspect;
        Debug.Log(w + " x " + h);
    }

    public bool CheckBounds(GameObject go)
    {
        Vector3 pos = go.transform.position;

        x = pos.x;
        y = pos.y;
        bool hBound = false;
        bool hL = false;
        bool hR = false;
        bool hB = false;
        bool hT = false;

        if (x < -w + r)
        {
            hBound = true;
            hL = true;
        }
        else if (x > w - r)
        {
            hBound = true;
            hR = true;
        }
        if (y < -h + r)
        {
            hBound = true;
            hB = true;
            Debug.Log("hit bottom");
        }
        else if (y > h - r)
        {
            hBound = true;
            hT = true;
        }
        hitBounds = hBound;
        hitBottom = hB;
        hitTop = hT;
        hitLeft = hL;
        hitRight = hR;
        return hitBounds;
    }
}
