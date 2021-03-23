using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject boxPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go0 = Instantiate<GameObject>(boxPrefab);
        GameObject go1 = Instantiate<GameObject>(boxPrefab);
        GameObject go2 = go0;
        print(go0 == go1);
        print(go0 == go2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
