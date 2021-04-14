using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene("SelectPlayer", LoadSceneMode.Single);
        }
    }
}
