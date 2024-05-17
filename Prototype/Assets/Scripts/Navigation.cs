using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform goal;
    public float stayChaseDistance;
    public float stopChaseDistance;
    public bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            agent.SetDestination(goal.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }


        if (Vector3.Distance(transform.position, goal.position) > stayChaseDistance || Vector3.Distance(transform.position, goal.position) < stopChaseDistance)
        {
            toggle = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            toggle = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Vector3.Distance(transform.position, goal.position) > stayChaseDistance)
        {
            toggle = false;
        }
        else
        {
            toggle = true;
        }
    }
}
