using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
     public Canvas gameplayUI; 
    public void Continue()
    {
        
        Time.timeScale = 1;
        gameplayUI.enabled = true; 
        pauseMenuCanvas.SetActive(false);
    }

    public void menu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0); 
    }
}
