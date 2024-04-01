using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{  
    public GameObject pauseMenuCanvas;
    public Canvas gameplayUI; 
    public Canvas settingsUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);
      
        if (pauseMenuCanvas.activeSelf)
        {
            Time.timeScale = 0; 
            gameplayUI.enabled = false;
            settingsUI.enabled = false; 
        }
        else
        {
            Time.timeScale = 1;
            gameplayUI.enabled = true; 
        }
    }

  public void ShowSettingsMenu()
{
    settingsUI.gameObject.SetActive(true); 
    settingsUI.enabled = true; 
    pauseMenuCanvas.SetActive(false);
}


    public void BackToPauseMenu()
    {
        pauseMenuCanvas.SetActive(true);
        settingsUI.enabled = false;
    }
}
