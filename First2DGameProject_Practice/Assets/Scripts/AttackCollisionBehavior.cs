using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionBehavior : MonoBehaviour
{
    public List<GameObject> hitGameobjectsList;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Enemy") == true)
        {
            GameObject hitGameobject = collider.gameObject;
            hitGameobjectsList.Add(hitGameobject);
        }
    }
}
