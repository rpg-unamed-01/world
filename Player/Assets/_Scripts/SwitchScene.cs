using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour
{
 void OnTriggerEnter(Collider other)//when player triggers the box
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//move to the current scene +1 in the build settings
    }
}
