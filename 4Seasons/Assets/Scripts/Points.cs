using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] int pointForPickup = 1;
    bool Collected = false;

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !Collected)
        {
          
            Collected = true;
            Session session = FindObjectOfType<Session>();
            
            if (session != null)
            {
                session.AddToScore(pointForPickup);
            }
            Destroy(gameObject);
        }
    }
}
