using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelect : MonoBehaviour
{
    public GameObject[] weapons;
    public static int selectedWeapon = 0;
    //Show next weapon
    public void Next()
    {
        weapons[selectedWeapon].SetActive(false);
        selectedWeapon = (selectedWeapon + 1) % weapons.Length;
        weapons[selectedWeapon].SetActive(true);
    }
    //Show previous weapon
    public void Previous() 
    {
        weapons[selectedWeapon].SetActive(false);
        selectedWeapon--;
        if (selectedWeapon < 0)
        {
            selectedWeapon += weapons.Length;
        }
        weapons[selectedWeapon].SetActive(true);
    }
    //Initialize first showed weapon
    void Start()
    {
        weapons[selectedWeapon].SetActive(true);
    } 
}
