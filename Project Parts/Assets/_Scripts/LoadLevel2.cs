using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel2 : LoadLevel
{
    public override void LevelFunction()
    {
        playerData.SetCurrentScene("Level2");
        Destroy(player);
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }

    public override bool CheckClearance()
    {
        return playerData.level > 0;
    }
}
