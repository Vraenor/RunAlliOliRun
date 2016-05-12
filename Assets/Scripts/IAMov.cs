using UnityEngine;
using System.Collections;

public class IAMov : MonoBehaviour {

    new Rigidbody rigidbody;

    public float accel = 0.3f, inertia = 0.9f, speedLimit = 5.0f, minSpeed = 1.0f, stopTime = 1.0f, rotationDamping = 1.0f;
    private float currentSpeed = 0.0f;
    public GameObject waypoint;
    public GameObject[] waypoints;
    public int wayPointIndex = 0;
    public int functionState;

    bool acceState, slowState, smoothRotation = true, volcado;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        functionState = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
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

                Quaternion rotation = Quaternion.LookRotation(waypoint.transform.position - transform.position);
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
            currentSpeed = minSpeed;
            functionState = 0;
        }
    }
}
