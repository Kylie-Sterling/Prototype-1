using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{

    public enum EnemyState
    {
        patrolling,
        alert,
        dead,
        idle
    }

    public NavMeshAgent agent;
    public Transform goal;
    public float stayChaseDistance; //distance at which the player is too far to chase
    public float stopChaseDistance; //distance at which the player is too close to chase // REMOVE AND MAKE ENEMY STOP FOLLOWING WHEN ATTACKING (enemyAttackAnimation.cs)
    //public bool toggle;
    public EnemyState currentState; //current enemy state
    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        PlayerController p = FindAnyObjectByType<PlayerController>();
        goal = p.gameObject.transform; //set the goal to the player initally
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.alert) //if should chase the player
        {
            agent.isStopped = false; //make sure agent is active
            agent.SetDestination(goal.position); //set the navmesh target to follow the player
        }
        else //replace with switch
        {
            //agent.SetDestination(transform.position); //remove this shit, this updates the path EVERY frame for no reason
            agent.isStopped = true; //stop following the player by telling the agent to stop
        }


        if (Vector3.Distance(transform.position, goal.position) > stayChaseDistance //if too far from the player
            || Vector3.Distance(transform.position, goal.position) < stopChaseDistance) //or if too close to player
        {
            currentState = EnemyState.idle; //stop following player
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) //if the player entered the trigger
        {
            currentState = EnemyState.alert; //follow
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") //if the player left the trigger
            && Vector3.Distance(transform.position, goal.position) > stayChaseDistance) //and it is too far away
        {
            currentState = EnemyState.idle; //stop following
        }
        /*else
        {
            toggle = true;
        }*/ //this doesnt make sense, why would we start following the player if the above statement fails?
        //we should already be following the player, and if an object leaves the trigger that isnt the player we
        //would start follwoing the player
    }
}
