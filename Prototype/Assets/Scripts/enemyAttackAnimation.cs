using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackAnimation : MonoBehaviour
{
    public Animator anim; //animator controller
    public Navigation navController; //reference to naviagtion script
    private bool shouldAttack = false; //should the enemy be attacking right now

    private void Update()
    { //this code resets the nav state when the attack anim finishes
        if (anim.GetNextAnimatorStateInfo(0).IsName("enemyIdle") && shouldAttack) //if idle, but still in range
        {
            navController.currentState = Navigation.EnemyState.alert; //follow the player
        }
    }

    private void OnTriggerEnter(Collider other) //if an object enters the sphere trigger in front of the enemy
    {
        if (other.tag == "Player") //if player enters the attack trigger
        {
            anim.SetTrigger("enemyAttack"); //start attack animation
            shouldAttack = true; //should attack
            navController.currentState = Navigation.EnemyState.attacking; //set nav state to attacking (stop following)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") //.. left the trigger
        {
            navController.currentState = Navigation.EnemyState.alert; //go back to the following state
            shouldAttack = false; //shouldn't attack
        }
    }
}
