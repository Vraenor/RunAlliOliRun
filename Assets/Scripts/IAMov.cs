using UnityEngine;
using System.Collections;

public class IAMov : MonoBehaviour {

    new Rigidbody rigidbody;

    float accel = 0.8f, inertia = 0.9f, speedLimit = 10.0f, minSpeed = 1.0f, stopTime = 1.0f, rotationDamping = 0.6f;
    private float currentSpeed = 0.0f;
    public Transform waypoint;
    public Transform[] waypoints;
    public int wayPointIndex = 1;
    int functionState;
    bool acceState, slowState, smoothRotation = true;
    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        functionState = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (functionState == 0) Acce();

        if (functionState == 1) Slow();

        if (wayPointIndex >= waypoints.Length) wayPointIndex = 0;
        waypoint = waypoints[wayPointIndex];
 
    }

    void Acce()
    {

        if (acceState == false)
        {
            acceState = true;
            slowState = false;
        }

        if (waypoint)
        {
            if (smoothRotation)
            {

                Quaternion rotation = Quaternion.LookRotation(waypoint.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            }
        }
        currentSpeed = currentSpeed + accel * accel;
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        if (currentSpeed >= speedLimit) currentSpeed = speedLimit;

    }

    void Slow()
    {

        if (slowState == false)
        {
            acceState = false;
            slowState = true;
        }

        currentSpeed = currentSpeed * inertia;
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        if (currentSpeed <= minSpeed)
        {
            currentSpeed = 0.0f;
            functionState = 0;

        }
    }

    /*void FixedUpdate()
    {

        //Check if we are touching the ground
        if (Physics.Raycast(transform.position, transform.up * -1, 3f))
        {

            //We are on the ground, enable the accelerator and increase drag
            rigidbody.drag = 1;

            // Calculate forward force
            Vector3 forwardForce = transform.forward * acceleration * Input.GetAxis("Vertical");

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
            rigidbody.angularDrag = 1.8f;
        }

        // You can turn in the air or the ground
        Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis("Horizontal");

        // Correct force for deltatime and vehicle mass
        turnTorque = turnTorque * Time.deltaTime;
        rigidbody.AddTorque(turnTorque);

    }*/
}
