using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking; 

public class Session : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points;
    void Start()
    {
        if(DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        points.text = DBManager.score.ToString();
    }

     public void AddToScore(int pointsToAdd)
    {
        DBManager.score += pointsToAdd;
        points.text = DBManager.score.ToString();
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

 IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Błąd w zapisie. Błąd #" + www.error);
            }
            else
            {
                if(www.downloadHandler.text == "0"){
                    Debug.Log("Gra zapisana.");
                } else {
                    Debug.Log("Błąd w zapisie. Błąd #" + www.downloadHandler.text);
                }
            }
        }

        DBManager.LogOut();
        SceneManager.LoadScene(0);
    }
    public void GoToScore(){
        SceneManager.LoadScene(4); 
    }

    
}
