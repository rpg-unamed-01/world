using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelClear : MonoBehaviour
{
    public PlayerData playerData;

    public Canvas clearScreen;
    public GameObject enemies;
    public GameObject win;
    public GameObject inlevel;
    public Text buttonText;

    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        clearScreen.gameObject.SetActive(false);
        win.SetActive(false);
        inlevel.SetActive(false);
    }

    public void Display(PlayerController player) {
        this.player = player;
        clearScreen.gameObject.SetActive(true);
        if (enemies.transform.childCount == 0)
        {
            win.SetActive(true);
            buttonText.text = "Warp Back to City";
        }
        else {
            inlevel.SetActive(true);
            buttonText.text = "Back to Fighting";
        }
    }

    public void OnClick() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (win.active)
        {
            if (playerData.currentScene == "Level1")
            {
                playerData.level = 1;
            } else if (playerData.currentScene == "Level2")
            {
                playerData.level = 2;
            }
            playerData.currentScene = "rpg_spawn";
            playerData.SetPlayerData(player.money + 100,
                player.items.items, player.maxHealth + 50,
                player.myscript.damage + 5);
            win.SetActive(false);
            clearScreen.gameObject.SetActive(false);
            Destroy(player.gameObject);
            SceneManager.LoadScene("rpg_spawn", LoadSceneMode.Single);
        }
        else
        {
            inlevel.SetActive(false);
            clearScreen.gameObject.SetActive(false);
            player.levelClearScreen = false;
        }
    }
}
