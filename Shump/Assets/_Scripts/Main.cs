using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject[] enemies;
    private BoundaryCheck myscript = new BoundaryCheck();
    public float h;
    public float w;
    public float x;
    public int count = 0;
    // Update is called once per frame
    void Start()
    {
        myscript.Initiate(0);
        h = myscript.h;
        w = myscript.w;
        InvokeRepeating("SpawnEnemy", 0, 1);
    }

    void SpawnEnemy() {
        int e = Random.Range(0, enemies.Length);
        GameObject go = Instantiate(enemies[e]);


        float pad = 0;
        if (go.GetComponent<BoundaryCheck>() != null)
        {
            pad = Mathf.Abs(go.GetComponent<BoundaryCheck>().r);
        }

        x = Random.Range(-w+pad, w-pad);
        
        Vector3 pos = new Vector3(x, h+pad, 0);

        go.transform.position = pos;
    }
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(w * 2, h * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
