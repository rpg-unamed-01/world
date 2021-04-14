using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName
= "Player Data")]
public class PlayerData : ScriptableObject
{
    public int level = 0;
    public GameObject[] potions;
    public GameObject player;
    public int money;
    public float maxHealth;
    public float damage;
    public string[] items = new string[10];
    public Transform spawnPoint;
    public string currentScene;

    public void SetPlayer(GameObject player) {
        this.player = player;
    }

    public void SetPlayerData(int money, GameObject[] items, float maxHealth, float damage) {
        this.money = money;
        for (int i = 0; i < 10; i ++)
        {
            if (items[i] != null)
            {
                this.items[i] = items[i].name;
            }
            else {
                this.items[i] = null;
            }
        }
        this.maxHealth = maxHealth;
        this.damage = damage;
    }

    public void SetSpawnPoint(Vector3 pos)
    {
        spawnPoint.position = pos;
    }

    public void SetCurrentScene(string scene) {
        currentScene = scene;
    }
}
