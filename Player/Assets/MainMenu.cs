using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//move to the current scene +1 index in the build settings
    }
    public void QuitGame()
    {
        Application.Quit();//close the applications
    }
}
