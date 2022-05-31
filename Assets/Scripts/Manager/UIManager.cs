using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject escPanel;
    public GameObject mainPanel;

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

    private void Update()
    {
        OpenEscPanel();

        if (SceneManage.instance.GetCurSceneIndex() == 0)
        {
            mainPanel.SetActive(true);
        }
        else
        {
            mainPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        SceneManage.instance.QuieGame();
    }

    public void StartGame()
    {
        SceneManage.instance.LoadNextScene();
    }

    public void GoBackToMain()
    {
        if (escPanel.activeSelf)
        {
            escPanel.SetActive(false);
        }

        SceneManage.instance.GoBackToMain();
    }

    public void OpenEscPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManage.instance.GetCurSceneIndex() != 0)
            {
                escPanel.SetActive(true);
                escPanel.GetComponent<Animator>().Play("open");
                Time.timeScale = 0;
            }
        }
    }

    public void CloseEscPanel()
    {
        Time.timeScale = 1;
        escPanel.GetComponent<Animator>().Play("close");
    }
}
