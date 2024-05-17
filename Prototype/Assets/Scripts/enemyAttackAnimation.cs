using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackAnimation : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("enemyAttack");
    }
}
