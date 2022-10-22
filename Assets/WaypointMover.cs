using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    // Stores a reference to the waypoint system this object will use
    [SerializeField] private Waypoint waypoints;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float distanceThreshold = 0.1f;

    // The current waypoint target that the object is moving towards
    private Transform currentWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        // Set initial position to the first waypoint
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        // Set the next wavepoint target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);

        // Supposed to make the drone look at the waypoints *Doesn't work
        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        // Move from first waypoint to second
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // Moving towards the rest of the wavepoints
        if(Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold){
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
        }
    }
}
