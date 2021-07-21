using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 10;
    public Rigidbody2D enemyRB;
    [SerializeField] float enemyMovespeed = 3;
    [SerializeField] float horizontalKnockback = 300;
    [SerializeField] float verticalKnockback = 500;
    public GameObject targetPlayer;

    bool onDamagedPlayer = false;
    float delayChasingDuration = 1.5f;
    float delayChasingTimer = 0f;

    void Start() 
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() 
    {
        FacingtoPlayer();

        if(onDamagedPlayer == false)
        {
            ChasingPlayer();
        }
        else
        {
            delayChasingTimer += Time.deltaTime;

            if(delayChasingTimer >= delayChasingDuration)
            {
                onDamagedPlayer = false;
                delayChasingTimer = 0f;
            }
        }
    }

    void FacingtoPlayer()
    {
        Vector3 playerLocation = targetPlayer.transform.position;
        Vector3 enemyLocation = transform.position;

        if(enemyLocation.x > playerLocation.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);   //another way to flip is in Movement script
        }

        if(enemyLocation.x < playerLocation.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0); //another way to flip is in Movement script
        }
    }

    void ChasingPlayer()
    {
        Vector3 playerLocation = targetPlayer.transform.position;
        Vector3 enemyLocation = transform.position;

        if(enemyLocation.x > playerLocation.x)
        {
            enemyRB.velocity = new Vector2(-enemyMovespeed, enemyRB.velocity.y);
        }

        if(enemyLocation.x < playerLocation.x)
        {
            enemyRB.velocity = new Vector2(enemyMovespeed, enemyRB.velocity.y);
        }
    }

    public void Update_EnemyHealth(int damageReceived, GameObject Attacker) 
    {
        health -= damageReceived;

        Vector3 currentLocation = gameObject.transform.position;
        Vector3 damagedLocation = Attacker.transform.position;

        if(currentLocation.x > damagedLocation.x)
        {
            enemyRB.AddForce(new Vector2(horizontalKnockback, verticalKnockback));
        }
        else
        {
            enemyRB.AddForce(new Vector2(-horizontalKnockback, verticalKnockback));
        }

        if(health <= 0)
        {
            Movement playerMovement = targetPlayer.GetComponent<Movement>();
            playerMovement.playerScore += 1;
            playerMovement.killCountText.text = "Kill Count: " + playerMovement.playerScore;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            Movement PlayerMovement = collision.gameObject.GetComponent<Movement>();
            PlayerMovement.Update_PlayerHealth(5, gameObject);
            onDamagedPlayer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("EnemyJumpArea") == true)
        {
            enemyMovespeed += 1.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("EnemyJumpArea") == true)
        {
            enemyMovespeed -= 1.5f;
        }
    }
}
