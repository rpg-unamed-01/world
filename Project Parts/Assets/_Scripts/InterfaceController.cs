using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public PlayerController player;
    public Slider healthbar;
    public GameObject menu;
    public GameObject shop;
    public GameObject levelMenu;
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menu.SetActive(player.displayMenu);
        healthbar.value = player.health/player.maxHealth;
        shop.SetActive(player.displayShop);
        levelMenu.SetActive(player.displayLevelMenu);
        mainMenu.SetActive(player.displayMainMenu);
    }
}
