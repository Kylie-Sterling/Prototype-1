using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
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
        idle,
        attacking
    }

    public NavMeshAgent agent;
    public float stayChaseDistance; //distance at which the player is too far to chase
    public List<Vector3> patrolWaypoints; //list of patrol waypoints
    private int currentWaypointIndex = 0; //index of currently selected waypoint
    public EnemyState currentState = EnemyState.patrolling; //current enemy state
    private PlayerController playerController; //basically a player ref

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != EnemyState.attacking) //if we aren't attacking
        {
            if (currentState == EnemyState.alert) //if should chase the player
            {
                agent.isStopped = false; //make sure agent is active
                agent.SetDestination(playerController.transform.position); //set the navmesh target to follow the player
            }
            if (currentState == EnemyState.patrolling) //if should be patrolling
            {
                agent.isStopped = false; //allow agent to move

                if (agent.remainingDistance <= 0.05f) //if reached destination
                {
                    currentWaypointIndex = mod(currentWaypointIndex + 1, patrolWaypoints.Count); //increment index and wrap it
                    agent.SetDestination(patrolWaypoints[currentWaypointIndex]); //target the next waypoint
                }
                else
                { //todo: maybe remove this as it updates path every frame
                    agent.SetDestination(patrolWaypoints[currentWaypointIndex]); //target next waypoint
                }
            }
        

            if (Vector3.Distance(transform.position, playerController.transform.position) > stayChaseDistance) //if too far from player
            {
                currentState = EnemyState.patrolling; //stop following player
            }
        }

        if (currentState == EnemyState.attacking) //if we are attacking
        {
            agent.isStopped = true; //stop agent
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") //if the player entered the trigger
        {
            currentState = EnemyState.alert; //follow
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") //if the player left the trigger
            && Vector3.Distance(transform.position, playerController.transform.position) > stayChaseDistance) //and it is too far away
        {
            currentState = EnemyState.patrolling; //stop following
        }
    }

    private int mod(int x, int m) //modulus function
    {
        return (x%m + m)%m; //negative friendly function
    }
}
