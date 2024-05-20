using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentWaypoint = 1;
    public bool atBottom;
    public float moveSpeed;
    public bool moving;
    public bool oneTimeUse;
    public BoxCollider box;
    private void Update()
    {
        if(moving)
        {
            MoveToWaypoint();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ele");
        moving = true;
    }
    void MoveToWaypoint()
    {
        float distance;
        if (atBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, moveSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, waypoints[0].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[1].position, moveSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, waypoints[1].position);
        }

        if (distance <= 0.1f)
        {
            atBottom = !atBottom;
            moving = false;
        }
        if(oneTimeUse)
        {
            Destroy(box);
        }
    }
}

