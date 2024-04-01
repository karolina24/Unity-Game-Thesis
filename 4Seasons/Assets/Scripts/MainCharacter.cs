using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] float walkSpeed = 7f;
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float climbSpeed = 5f;
    public Transform attackPoint;
    public float attackRange = 0.7f;
    public LayerMask enemyLayer;
    public int AttackDamage;
     Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    PlayerHealth playerHealth;
    private bool isHurtCoroutineRunning = false;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(!playerHealth.isAlive){return;}
        Walk();
        FlipSprite();
        ClimbLadder();
        Die();
        Hurt();
        ExitLevel();
    }

    void Walk()
    {
      if( !playerHealth.isAlive ){return;}
        Vector2 playerVelocity = new Vector2 (moveInput.x * walkSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
        myAnimator.SetBool("isRunning", true);
        }else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }
    void OnMove(InputValue value)
{
    if (!playerHealth.isAlive) { return; }
    moveInput = value.Get<Vector2>();

  
    if (moveInput.x > 0)
    {
        myRigidbody.velocity = new Vector2(walkSpeed, myRigidbody.velocity.y);
        
        if (moveInput.y > 0)
        {
            myAnimator.SetTrigger("Jump");
        }
    }
}

   void OnJump(InputValue value)
    {
        if (!playerHealth.isAlive || IsTouchingLadder() || !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return;
        }
        
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            myAnimator.SetTrigger("Jump");
            
            StartCoroutine(DelayedJumpReset());
        }
    }
     IEnumerator DelayedJumpReset()
    {
        yield return new WaitForEndOfFrame();
         myAnimator.ResetTrigger("Jump");
    }
    bool IsTouchingLadder()
    {
        return myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }


    void OnAttack(InputValue value)
    {
        if(!playerHealth.isAlive){return;}
        if (value.isPressed)
        {
            myAnimator.SetTrigger("Attack");
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D Enemy in hitEnemies)
            {
                Enemy.GetComponent<Enemies>().TakeDamage(AttackDamage);
            }
        }
    }
    

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null){return;}
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

    void FlipSprite()
    {
        if(!playerHealth.isAlive){return;}
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    void Hurt()
    {
        if(!playerHealth.isAlive){return;}
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
            if (!isHurtCoroutineRunning)
            {
                StartCoroutine(HurtCoroutine());
            }
            }else
            {
                StopCoroutine(HurtCoroutine());
                isHurtCoroutineRunning = false;
            }
    }
    IEnumerator HurtCoroutine()
    {
        isHurtCoroutineRunning = true;
        myAnimator.SetTrigger("Hurt");
        playerHealth.TakeDamagePlayer(10);
        yield return new WaitForSeconds(3f);
        isHurtCoroutineRunning = false;
    }

   void ClimbLadder()
{
    if (!playerHealth.isAlive) return;

    if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
    {
        myRigidbody.gravityScale = gravityScaleAtStart;
        myAnimator.SetBool("isClimbing", false);
        return;
    }
    Vector2 ClimbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
    myRigidbody.velocity = ClimbVelocity;
    myRigidbody.gravityScale = 0f; 

    bool playerVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

    myAnimator.SetBool("isClimbing", playerVerticalSpeed);
}


    void Die()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            myAnimator.SetTrigger("Death");
            
        }
    }

    void ExitLevel()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Exit")))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
             SceneManager.LoadScene(nextSceneIndex);
        }
    }

}
