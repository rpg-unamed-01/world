using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject canvas;
    public Text text;

    private PlayerController player;
    private void Start()
    {
        canvas.SetActive(false);
    }

    public void Display(PlayerController player) {
        this.player = player;
        if (playerData.level == 0) {
            text.text = "Welcome Warrior, the robots are taking over the city! \n" +
                        "Hurry up and go help out in the building up ahead. \n" +
                        "There is a shop to your right if you need resources, \n" +
                        "I have given you 50 gold to start off, go destroy those ROBOTS!";
        }
        if (playerData.level == 1)
        {
            text.text = "Good job cleaning up the office building! \n" +
                        "Your next mission will be a lot tougher... you have to go to their lair!\n" +
                        "Watch out for the turrets down there, they slow you down and let\n" +
                        "the crawlers catch you. You know where the shop is, safe travels!";
        }
        if (playerData.level == 2)
        {
            text.text = "Wow you're back! I mean, I was totally expecting you to succeed...\n" +
                        "Anywho, good job beating the robot 'lair', I am glad to say you have\n" +
                        "passed the test with flying colors! You are now able to join our world\n" +
                        "Robot Eradication Team (RET) and travel with us throughout the world\n" +
                        "Just hang around here until we have your next mission.";
        }
        canvas.SetActive(true);
    }

    public void OnClick() {
        canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.inGuide = false;
    }
}
