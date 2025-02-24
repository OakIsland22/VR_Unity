using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_NPC : MonoBehaviour
{
    public List<Transform> waypoints; 
    public float speed = 5f; 
    public bool loop = true; 

    private int currentWaypointIndex = 0; 

    void Update()
    {
        if (waypoints.Count == 0) return; 

        
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;

        
            if (currentWaypointIndex >= waypoints.Count)
            {
                if (loop)
                {
                    currentWaypointIndex = 0; 
                }
                else
                {
                    enabled = false;
                }
            }
        }
    }

}
