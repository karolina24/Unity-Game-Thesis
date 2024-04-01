using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealthPlayer = 100;
    public static int currentHealthPlayer = 100; 
    Animator myAnimator;
    public MainCharacter mainCharacter;
    public bool isAlive = true;
    public HealthBar healthBar;

    void Start()
    {
        
        myAnimator = GetComponent<Animator>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        healthBar.SetHealth(currentHealthPlayer); 
    }

    void Update()
    {
        if (!isAlive) { return; }
    }

    public void TakeDamagePlayer(int damagePlayer)
    {
        if (!isAlive) { return; }
        if (currentHealthPlayer >= 1)
        {
            currentHealthPlayer -= damagePlayer;
            healthBar.SetHealth(currentHealthPlayer);
        }
        else if (currentHealthPlayer <= 0) 
        {
            DiePlayer();
            isAlive = false;
        }
        Debug.Log("Player Health:" + currentHealthPlayer);
    }

    void DiePlayer()
{
    isAlive = false;
    myAnimator.SetTrigger("Death");
    StartCoroutine(WaitAndLoadScene());
}
IEnumerator WaitAndLoadScene()
{
    yield return new WaitForSeconds(2);
    SceneManager.LoadScene(7); 
}
}
