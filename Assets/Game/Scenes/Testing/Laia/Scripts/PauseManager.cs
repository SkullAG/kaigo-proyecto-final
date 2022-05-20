using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public InputActionReference _ref;
    public bool pausedAvailable = true;
    public GameObject pausedId;

    void Update()
    {
        //Debug.Log("entro en pause");
        if(pausedAvailable)
        {
            //Debug.Log("first");
            if (_ref.action.triggered)
            {
                //Debug.Log("trigger");
                if (GameIsPaused)
                {
                    Resume();

                    Debug.Log("GameIspaused");
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pausedId.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pausedId.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
