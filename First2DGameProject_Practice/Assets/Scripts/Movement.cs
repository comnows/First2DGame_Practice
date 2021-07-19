using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpForce = 5;
    bool isOnGround = true;

    [SerializeField] AnimationNotify animNotify;
    //[Range(0,.3f)][SerializeField] float movementSmooth = 0.05f;

    //float horizontalMove = 0f;
    //bool isJump = false;

    //private bool facingRight = false;
    //private Vector3 velocity = Vector3.zero;

    private void Update() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
            playerAnim.SetBool("OnMoving", true);
        }
        
        if(Input.GetKey(KeyCode.D))
        {
            playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
            playerAnim.SetBool("OnMoving", true);
        }

        if(Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
        {
            playerAnim.SetBool("OnMoving", false);
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
            isOnGround = false;
            playerAnim.SetTrigger("OnJumping");
        }

        Checking_AttackingBehavior();

        if(Input.GetButtonDown("Fire1") && animNotify.onAttacking == false)
        {
            animNotify.onAttacking = true;
            playerAnim.SetTrigger("OnAttack");
        }
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
