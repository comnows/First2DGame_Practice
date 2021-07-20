using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 10;
    public Rigidbody2D enemyRB;
    [SerializeField] float horizontalKnockback = 300;
    [SerializeField] float verticalKnockback = 500;

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
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            Movement PlayerMovement = collision.gameObject.GetComponent<Movement>();
            PlayerMovement.Update_PlayerHealth(5, gameObject);
        }
    }
}
