////////////////////////////////////////////////////
//
//  Referenced code: Mario Haberle Youtube Ch
//
////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementScript : MonoBehaviour
{
    Rigidbody ourDrone;

    void Awake(){
        ourDrone = GetComponent<Rigidbody>();


    }

    void FixedUpdate(){
        MovementUpDown();
        
        MovementForward();

        Rotation();

        SlowingSpeed();

        Swerve();

        ourDrone.AddRelativeForce(Vector3.up * upForce);

        // Returning a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis.
        ourDrone.rotation = Quaternion.Euler(
            new Vector3(tiltAmountForward, currentYRotation, tiltAmountSideways)
        );
    }

    // Going up and down
    public float upForce;
    void MovementUpDown(){

        // Checking to see if we are moving horizontal/vertical axis
        if((Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)){

            // Going up or down
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift)){
                ourDrone.velocity = ourDrone.velocity;
            }


            // Not clicking anything
            if(!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 281;
            }

            // If we are rotating and not going up or down
            if(!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))){
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 110;
            }

            // If we are only rotating
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
                upForce = 410;
            }
        }


        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f){
            upForce = 135;
        }



        // Ascending amount
        if(Input.GetKey(KeyCode.Space)){
            upForce = 450;
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f){
                upForce = 500;
            }
        }

        // Descending amount
        else if(Input.GetKey(KeyCode.LeftShift)){
            upForce = -200;
        }

        // If we are not going up or down, the gradual fall is gravity times mass
        else if(!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)){
            upForce = 98.1f;
        }
    }

    // Going forward and backwards
    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;
    void MovementForward(){
        if(Input.GetAxis("Vertical") != 0){

            // Allows the forward and backward motion
            ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);

            // Adjusts the movement tilt
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }
    }


    // Rotation
    private float wantedYRotation;
    [HideInInspector]public float currentYRotation;
    private float rotateAmountByKeys = 2.5f;
    private float rotationYVelocity;


    void Rotation(){
        
        // Decreases the rotation of the A key
        if(Input.GetKey(KeyCode.A)){
            wantedYRotation -= rotateAmountByKeys;
        }
        

        // Increases the rotation of the D key
        if(Input.GetKey(KeyCode.D)){
            wantedYRotation += rotateAmountByKeys;
        }

        // Smoothing the movement of the rotation
        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);

    }


    // Limiting the initial movement speed since it is too fast
    private Vector3 velocityToSmoothDampToZero;
    void SlowingSpeed(){
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f){
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        } 
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f){
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f){
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 5.0f, Time.deltaTime * 5f));
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f){
            ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
         
    }

    // Allows for the natural swerve of the drone when moving forward, backward, left, and right
    private float sideMovementAmount = 300.0f;
    private float tiltAmountSideways;
    private float tiltAmountVelocity;
    void Swerve(){
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f){
            ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, -20 * Input.GetAxis("Horizontal"), ref tiltAmountVelocity, 0.1f);
        }

        // If we are not pressing anything we want
        else{
            tiltAmountSideways = Mathf.SmoothDamp(tiltAmountSideways, 0, ref tiltAmountVelocity, 0.1f);
        }
    }
}
