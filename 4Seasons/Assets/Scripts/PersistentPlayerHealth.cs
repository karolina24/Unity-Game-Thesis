using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentPlayerHealth : MonoBehaviour
{
   public static PersistentPlayerHealth Instance { get; private set; }
    public int CurrentHealth { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
