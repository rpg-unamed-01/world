using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1 : LoadLevel
{
    public override void LevelFunction()
    {
        playerData.SetCurrentScene("Level1");
        Destroy(player);
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
