using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float moveSpeed = 5;
    //[Range(0,.3f)][SerializeField] float movementSmooth = 0.05f;

    //float horizontalMove = 0f;
    //bool isJump = false;

    //private bool facingRight = false;
    //private Vector3 velocity = Vector3.zero;

    private void Update() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);
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
