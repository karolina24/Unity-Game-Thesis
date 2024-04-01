using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public Text playerDisplay;
    private void Start()
    {
        if (DBManager.LoggedIn){
            playerDisplay.text = "Zalogowano: " + DBManager.username;
        }
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(1); 
    }

   public void GoToLogin()
   {
       SceneManager.LoadScene(2); 
   }

   public void GoToGame()
   {
        SceneManager.LoadScene(3); 
   }
    public void ExitGame()
    {
       
        Application.Quit();
    }
}