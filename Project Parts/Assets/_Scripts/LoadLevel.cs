using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public PlayerData playerData;
    public LayerMask playerLayer;

    public GameObject player;
    public PlayerController p;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            if (CheckClearance())
            {
                playerData.SetPlayerData(p.money, p.items.items, p.maxHealth, p.myscript.damage);
                playerData.SetSpawnPoint(transform.position + Vector3.back * 5);
                LevelFunction();
            }
        }
    }

    public virtual void LevelFunction() {

    }

    public void SetPlayer(GameObject player) {
        this.player = player;
        p = player.GetComponent<PlayerController>();
    }

    public virtual bool CheckClearance() {
        return true;
    }
}
