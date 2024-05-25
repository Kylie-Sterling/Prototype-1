using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackAnimation : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other) //if an object enters the sphere trigger in front of the enemy
    {
        anim.SetTrigger("enemyAttack"); //start attack animation
    }
}
