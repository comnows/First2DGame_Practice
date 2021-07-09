using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    Animator playerAnim;

    [SerializeField]
    Rigidbody2D playerRB;

    [SerializeField]
    float moveSpeed = 5;

    private void Update() 
    {
        if(Input.GetKey(KeyCode.A))
        {
            playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
        }

        if(Input.GetKey(KeyCode.D))
        {
            playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);
        }
    }
}
