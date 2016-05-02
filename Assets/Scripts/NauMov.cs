﻿using UnityEngine;
using System.Collections;

public class NauMov : MonoBehaviour {

    // Values that control the vehicle
    public float acceleration;
    public float rotationRate;
    public Vector3 forwardForce;

    // Values for taking a nice turn display
    public float turnRotationAngle;
    public float turnRotationSeekSpeed;

    // Reference variables we don't directly use
    private float rotationVelocity;
    private float groundAngleVelocity;

    public float angularAux;

    new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate () {
	
        //Check if we are touching the ground
        if (Physics.Raycast (transform.position, transform.up * -1, 3f)) {

            //We are on the ground, enable the accelerator and increase drag
            rigidbody.drag = 1;

            // Calculate forward force
             forwardForce = transform.forward * acceleration * Input.GetAxis("Vertical");

            // Correct the force for the deltatime and vehicle mass
            forwardForce = forwardForce * Time.deltaTime * rigidbody.mass;

            rigidbody.AddForce(forwardForce);
        }

        else
        {
            // We are not on the ground and don't want to just halt in mid-air; reduce drag
            rigidbody.drag = 0;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            rigidbody.angularDrag = 0;
        }
        else
        {
            rigidbody.angularDrag = angularAux;
        }
        
        // You can turn in the air or the ground
        Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis("Horizontal");

        // Correct force for deltatime and vehicle mass
        turnTorque = turnTorque * Time.deltaTime; // * rigidbody.mass;
        rigidbody.AddTorque(turnTorque);

        /*
       // Fake rotate the car when you are turning
        Vector3 newRotation = transform.eulerAngles;
        newRotation.z = Mathf.SmoothDampAngle(newRotation.z, Input.GetAxis("Horizontal") * -turnRotationAngle, ref rotationVelocity, turnRotationSeekSpeed);
        transform.eulerAngles = newRotation;*/
	}
}
