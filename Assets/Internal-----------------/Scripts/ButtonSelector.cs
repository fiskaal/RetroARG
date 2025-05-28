using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelector : MonoBehaviour
{
    [Header("Pause Menu")]
    public GameObject pauseMenuButton;
    public GameObject pauseMenuCanvas;
    [Header("Game Over Menu")]
    public GameObject gameOverButton;
    public GameObject gameOverCanvas;
    [Header("The End Menu")]
    public GameObject theEndButton;
    public GameObject theEndCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenuCanvas.activeInHierarchy) 
        {
            //EventSystem.current.SetSelectedGameObject(pauseMenuButton);
            EventSystem.current.firstSelectedGameObject = pauseMenuButton;
        }

        if (gameOverCanvas.activeInHierarchy)
        {
            //EventSystem.current.SetSelectedGameObject(gameOverButton);
            EventSystem.current.firstSelectedGameObject = gameOverButton;
        }

        if (theEndCanvas.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(theEndButton);
            EventSystem.current.firstSelectedGameObject = theEndButton;
        }
    }
}
