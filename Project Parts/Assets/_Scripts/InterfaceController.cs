using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    public PlayerData playerData;

    public PlayerController player;
    public Slider healthbar;
    public GameObject menu;
    public GameObject shop;
    public GameObject levelMenu;
    public GameObject mainMenu;
    public Text health;
    public Text money;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menu.SetActive(player.displayMenu);
        float healthPercent = player.health / player.maxHealth;
        healthbar.value = healthPercent;
        health.text = player.health.ToString();
        money.text = "$" + player.money;
        health.rectTransform.anchoredPosition = Vector3.right * (20 + healthPercent * 230) + Vector3.up * 10;
        shop.SetActive(player.displayShop);
        levelMenu.SetActive(player.displayLevelMenu);
        mainMenu.SetActive(player.displayMainMenu);
        button.gameObject.SetActive(false);
        if (player.health > 50) {
            button.gameObject.SetActive(true);
        }
    }

    public void OnClick() {
        if (player.displayLevelMenu)
        {
            playerData.SetPlayerData(player.money, player.items.items, player.maxHealth, playerData.damage);
            playerData.currentScene = "rpg_spawn";
            SceneManager.LoadScene("rpg_spawn", LoadSceneMode.Single);
        }
    }
}
