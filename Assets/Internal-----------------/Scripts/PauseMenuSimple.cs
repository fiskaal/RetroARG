using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuSimple : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject selectedPauseButton;
    public bool isPaused;

    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (Time.timeScale == 0)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        if (isPaused)
        {
            EventSystem.current.SetSelectedGameObject(selectedPauseButton);
            EventSystem.current.firstSelectedGameObject = selectedPauseButton;
        }
        
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
