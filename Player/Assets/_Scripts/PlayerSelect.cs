using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public GameObject[] players;
    public int selectedPlayer = 0;
    //Next shown player option
    public void Next()
    {
        players[selectedPlayer].SetActive(false);
        selectedPlayer = (selectedPlayer + 1) % players.Length;
        players[selectedPlayer].SetActive(true);
    }
    //Previous shown player option
    public void Previous() 
    {
        players[selectedPlayer].SetActive(false);
        selectedPlayer--;
        if (selectedPlayer < 0)
        {
            selectedPlayer += players.Length;
        }
        players[selectedPlayer].SetActive(true);
    }
    //Start Level1 scene
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedPlayer", selectedPlayer);
        PlayerPrefs.SetInt("selectedWeapon", WeaponSelect.selectedWeapon);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    //Set initial shown player
    void Start()
    {
        players[selectedPlayer].SetActive(true);
    } 
}
