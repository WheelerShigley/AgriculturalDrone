using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Changing the size of the sphere
    [Range(0f,2f)]
    [SerializeField] private float waypointSize = 1f;

    // Start is called before the first frame update
    private void OnDrawGizmos(){

        // Creates the spherical waypoints
        foreach(Transform t in transform){
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypointSize);
        }

        // Creating the red line connecting the waypoints
        Gizmos.color = Color.red;
        for(int i = 0; i < transform.childCount - 1; i++){
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }

        // Connecting the last waypoint back to the first waypoint
        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }
    
    public Transform GetNextWaypoint(Transform currentWaypoint){
        if(currentWaypoint == null){
            return transform.GetChild(0);
        }

        if(currentWaypoint.GetSiblingIndex() < transform.childCount - 1){
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }else{
            return transform.GetChild(0);
        }
    }
}
