using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject[] players;
    public GameObject playerModel;
    public GameObject weaponModel;
    public Text description;

    private GameObject player;
    private int currentPlayer = 0;

    private void Start()
    {
        player = players[currentPlayer];
        ShowPlayer();
    }

    private void ShowPlayer() {
        PlayerController p = player.GetComponent<PlayerController>();
        Weapon w = player.GetComponent<Weapon>();
        foreach (Transform child in playerModel.transform) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in weaponModel.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject g = Instantiate(p.playerModel, playerModel.transform);
        GameObject g2 = Instantiate(p.playerWeapon, weaponModel.transform);

        g.transform.localPosition = Vector3.zero;
        g2.transform.localPosition = Vector3.zero;
        description.text = w.Description();
    }

    public void Next() {
        currentPlayer = (currentPlayer + 1) % players.Length;
        Debug.Log(currentPlayer);
        player = players[currentPlayer];
        ShowPlayer();
    }

    public void Previous()
    {
        Debug.Log(currentPlayer);
        currentPlayer = currentPlayer == 0? currentPlayer = players.Length - 1 : currentPlayer - 1;
        player = players[currentPlayer];
        ShowPlayer();
    }

    public void OnPlay()
    {
        playerData.level = 0;
        playerData.currentScene = "rpg_spawn";
        playerData.SetPlayer(player);
        playerData.SetPlayerData(50, new GameObject[10], 100, player.GetComponent<Weapon>().damage);
        SceneManager.LoadScene("rpg_spawn", LoadSceneMode.Single);
    }
}
