using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadStartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
