using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initiate : MonoBehaviour
{
    public string SceneName;
    public PlayerData playerData;
    public Transform spawnPoint;
    public LoadLevel[] levels;
    public GameObject enemies;
    public Text location;

    private float locationTime;

    public void Awake()
    {
        locationTime = 5;
        location.gameObject.SetActive(true);
        GameObject g = Instantiate(playerData.player, spawnPoint.position, Quaternion.identity);
        playerData.currentScene = SceneName;
        if (levels != null)
        {
            foreach (LoadLevel level in levels)
            {
                level.SetPlayer(g);
            }
        }
        if (enemies != null) {
            Enemy e;
            foreach (Transform enemy in enemies.transform)
            {
                e = enemy.GetComponent<Enemy>();
                e.SetPlayer(g);
            }
        }
    }

    public void Update()
    {
        if (location.IsActive())
        {
            locationTime -= Time.deltaTime;
            if (locationTime <= 0)
            {
                Color lessAlpha = location.color;
                lessAlpha.a = lessAlpha.a - Time.deltaTime;
                location.color = lessAlpha;
                if (location.color.a <= 0) {
                    location.gameObject.SetActive(false);
                }
            }
        }
    }
}
