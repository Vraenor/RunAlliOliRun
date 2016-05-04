using UnityEngine;
using System.Collections;

public class IAMov : MonoBehaviour {

    new Rigidbody rigidbody;

    float accel = 0.8f, inertia = 0.9f, speedLimit = 10.0f, minSpeed = 1.0f, stopTime = 1.0f, rotationDamping = 0.6f;
    private float currentSpeed = 0.0f;
    public GameObject waypoint;
    public GameObject[] waypoints;
    public int wayPointIndex = 0;
    int functionState;
    bool acceState, slowState, smoothRotation = true;
    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        functionState = 0;

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        GameObject[] aux = waypoints;

        /*for (int i = 0; i <= waypoints.Length; i++)
        {
            string name = waypoints[i].name;
            name = name.Remove(0, 8);
            aux[int.Parse(name) - 1] = waypoints[i];
        }
        waypoints = aux;*/
    }

    // Update is called once per frame
    void Update()
    {

        if (functionState == 0) Acce();

        if (functionState == 1) Slow();

        if (wayPointIndex >= waypoints.Length)
        {
            wayPointIndex = 0;
        }
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
            currentSpeed = 0.0f;
            functionState = 0;

        }
    }
}
