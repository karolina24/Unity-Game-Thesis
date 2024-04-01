using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebTest : MonoBehaviour
{
    IEnumerator Start()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost/sqlconnect/webtest.php"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string[] webresults = request.downloadHandler.text.Split('\t');
                foreach (string s in webresults)
                {
                    Debug.Log(s);
                }

                if (webresults.Length > 1) // Ensure there's at least two elements
                {
                    Debug.Log(webresults[0]);
                    int webNumber;
                    if (int.TryParse(webresults[1], out webNumber)) // Safely try to parse the integer
                    {
                        webNumber *= 2;
                        Debug.Log(webNumber);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse webNumber from webresults[1]");
                    }
                }
                else
                {
                    Debug.LogError("The web response is not in the expected format.");
                }
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}