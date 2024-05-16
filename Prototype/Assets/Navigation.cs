using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform goal;
    public bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            agent.destination = goal.position;
        }

        else
        {
            agent.destination = transform.position;
        }
    }
}
