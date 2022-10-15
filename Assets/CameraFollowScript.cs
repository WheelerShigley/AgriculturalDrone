////////////////////////////////////////////////////
//
//  Referenced code: Mario Haberle Youtube Ch
//
////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    // Variables
    private Transform ourDrone;
    private Vector3 velocityCameraFollow;
    public Vector3 behindPosition = new Vector3(0,2,-4); 
    public float angle;

    // Awake drone when scene starts
    void Awake(){

        // Finds a gameobjecttag that has player and manipulates that object
        ourDrone = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    
    // Adjusting the position of the main camera
    void FixedUpdate(){
        // Smoothing camera movement
        transform.position = Vector3.SmoothDamp(transform.position, ourDrone.transform.TransformPoint(behindPosition) + Vector3.up * Input.GetAxis("Vertical"), ref velocityCameraFollow, 0.1f);

        // Returning a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis.
        transform.rotation = Quaternion.Euler(new Vector3(angle, ourDrone.GetComponent<DroneMovementScript>().currentYRotation,0));
    }
}
