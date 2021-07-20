using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] int atkDmg = 5;
    [SerializeField] int playerHealth;
    [SerializeField] float horizontalKnockback = 800;
    [SerializeField] float verticalKnockback = 800;
    bool isOnGround = true;

    [SerializeField] AnimationNotify animNotify;
    [SerializeField] AttackCollisionBehavior attackBehavior;

    bool isStun = false;
    float stunTimer = 0f;
    float stunDuration = 0.65f;
    //[Range(0,.3f)][SerializeField] float movementSmooth = 0.05f;

    //float horizontalMove = 0f;
    //bool isJump = false;

    //private bool facingRight = false;
    //private Vector3 velocity = Vector3.zero;

    private void Update() 
    {
        if(isStun == false)
        {
            if(Input.GetKey(KeyCode.A))
            {
                playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);    //another way to flip is in EnemyBehavior script
                playerAnim.SetBool("OnMoving", true);
            }
        
            if(Input.GetKey(KeyCode.D))
            {
                playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);   //another way to flip is in EnemyBehavior script
                playerAnim.SetBool("OnMoving", true);
            }
        
            if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRB.AddForce(new Vector2(0, jumpForce));
                isOnGround = false;
                playerAnim.SetTrigger("OnJumping");
            }
        }
        else
        {
            Update_StunStatus();
        }
        
        if(Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
        {
            playerAnim.SetBool("OnMoving", false);
        }
        Checking_AttackingBehavior();

        if(Input.GetButtonDown("Fire1") && animNotify.onAttacking == false)
        {
            animNotify.onAttacking = true;
            playerAnim.SetTrigger("OnAttack");
        }

        Checking_AttackingCollision();
        FallingCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Vector3 playerPosition = transform.position;
            Vector3 groundPosition = collision.transform.position;

            if(playerPosition.y >= groundPosition.y)
            {
                isOnGround = true;
            }
        }
    }

    void Checking_AttackingBehavior()
    {
        if(Input.GetButtonDown("Fire1") && animNotify.onAttacking == true)
        {
            if(animNotify.onContact == true || animNotify.onRecovery == true)
            {
                playerAnim.SetTrigger("OnCombo");
            }
        }

        if(animNotify.onAttacking == false)
        {
            playerAnim.ResetTrigger("OnCombo");
        }
    }

    void Checking_AttackingCollision()
    {
        if(attackBehavior.hitGameobjectsList.Count > 0)
        {
            for(int index = 0; index < attackBehavior.hitGameobjectsList.Count; index++)
            {
                GameObject currentHitGameobject = attackBehavior.hitGameobjectsList[index];

                EnemyBehavior enemyBehavior = currentHitGameobject.GetComponent<EnemyBehavior>();
                enemyBehavior.Update_EnemyHealth(atkDmg, gameObject);
            }

            attackBehavior.hitGameobjectsList.Clear();
        }
    }

    public void Update_PlayerHealth(int damageReceived, GameObject attacker)
    {
        playerHealth -= damageReceived;

        Vector3 currentLocation = gameObject.transform.position;
        Vector3 damagedLocation = attacker.transform.position;

        if(currentLocation.x > damagedLocation.x)
        {
            playerRB.AddForce(new Vector2(horizontalKnockback, verticalKnockback));
        }
        else
        {
            playerRB.AddForce(new Vector2(-horizontalKnockback, verticalKnockback));
        }

        isStun = true;
        stunTimer = 0f;

        Debug.Log("your health: " + playerHealth);

        if(playerHealth <= 0)
        {
            Debug.Log("Game Over!!");
            SceneManager.LoadScene("SampleScene");
        }
    }

    void Update_StunStatus()
    {
        if(isStun == true)
        {
            stunTimer += Time.deltaTime;

            if(stunTimer >= stunDuration)
            {
                isStun = false;
            }
        }
    }

    void FallingCheck()
    {
        if(transform.position.y <= -6.5)
        {
            Update_PlayerHealth(100, gameObject);
        }
    }

    //advance see in Brackeys Channel
    /*private void FixedUpdate() 
    {
        playerRB.velocity = new Vector2(horizontalMove * 10f, playerRB.velocity.y);
    }*/

    /*void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, playerRB.velocity.y);
        playerRB.velocity = Vector3.SmoothDamp(playerRB.velocity, targetVelocity, ref velocity, movementSmooth);

        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if(move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }*/
}
