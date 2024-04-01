using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.1f;
    Rigidbody2D myRigidbody;
    public int maxHealthEnemy = 100;
    int currentHealthEnemy;
    private bool isDying = false;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        currentHealthEnemy = maxHealthEnemy;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
    
    }

    void OnTriggerExit2D(Collider2D other) 
    {
         if(other.CompareTag("Player"))
    {
        return;
    }
        moveSpeed =  -moveSpeed;
        Flip();   
    }

    void Flip()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);      
    }


public void TakeDamage(int damage)
{
    if (isDying) return; 

    currentHealthEnemy -= damage;
    if (currentHealthEnemy <= 0 && !isDying)
    {
        isDying = true; 
        StartCoroutine(DieWithDelay(2f));
    }
}

IEnumerator DieWithDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    Die();
}

void Die()
{
    gameObject.SetActive(false);
    isDying = false; 
}
}