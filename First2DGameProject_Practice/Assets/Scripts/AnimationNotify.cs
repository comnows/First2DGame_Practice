using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNotify : MonoBehaviour
{
    public bool onPreparation = false;
    public bool onContact = false;
    public bool onRecovery = false;
    public bool onAttacking = false;
    public BoxCollider2D attackCollision;

    public void preparationStep()
    {
        onPreparation = true;
        onContact = false;
        onRecovery = false;
        //Debug.Log("Preparation Step");
        attackCollision.enabled = false;
        SoundController.soundInstance.PlaySoundEffect(1);
    }

    public void ContactStep()
    {
        onPreparation = false;
        onContact = true;
        onRecovery = false;
        //Debug.Log("Contact Step");
        attackCollision.enabled = true;
    }
    public void RecoveryStep()
    {
        onPreparation = false;
        onContact = false;
        onRecovery = true;
        //Debug.Log("Recovery Step");
        attackCollision.enabled = false;
    }

    public void EndAttack()
    {
        onPreparation = false;
        onContact = false;
        onRecovery = false;
        onAttacking = false;
        //Debug.Log("End attack");
        attackCollision.enabled = false;
    }
}
