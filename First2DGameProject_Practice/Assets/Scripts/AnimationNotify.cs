using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNotify : MonoBehaviour
{
    public bool onPreparation = false;
    public bool onContact = false;
    public bool onRecovery = false;
    public bool onAttacking = false;

    public void preparationStep()
    {
        onPreparation = true;
        onContact = false;
        onRecovery = false;
        Debug.Log("Preparation Step");
    }

    public void ContactStep()
    {
        onPreparation = false;
        onContact = true;
        onRecovery = false;
        Debug.Log("Contact Step");
    }
    public void RecoveryStep()
    {
        onPreparation = false;
        onContact = false;
        onRecovery = true;
        Debug.Log("Recovery Step");
    }

    public void EndAttack()
    {
        onPreparation = false;
        onContact = false;
        onRecovery = false;
        onAttacking = false;
        Debug.Log("End attack");
    }
}
