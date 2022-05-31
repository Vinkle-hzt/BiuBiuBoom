using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(transform.gameObject);
            }
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void QuieGame()
    {
        Application.Quit();
    }

    public int GetCurSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
