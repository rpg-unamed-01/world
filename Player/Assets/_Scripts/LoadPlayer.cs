using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public GameObject[] weaponPrefabs;
    public Transform spawn;
    
    void Start()
    {   
        //Spawn chosen player customization into position
        int selectedPlayer = PlayerPrefs.GetInt("selectedPlayer");
        GameObject prefab = playerPrefabs[selectedPlayer];
        var clone = GameObject.Instantiate(prefab, spawn.position, Quaternion.identity);
        clone.transform.parent = GameObject.Find("Player/Head").transform;
        clone.transform.localScale = new Vector3(2,2,2);
        clone.transform.Rotate(0f,-90f,0f);

        //Spawn chosen weapon into position
        GameObject prefab1 = weaponPrefabs[selectedPlayer];
        var clone1 = GameObject.Instantiate(prefab1, spawn.position, Quaternion.identity);
        clone1.transform.parent = GameObject.Find("Player/Head/MainCamera/Weapon").transform;
    }
}
