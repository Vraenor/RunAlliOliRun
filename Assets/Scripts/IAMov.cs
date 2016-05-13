using UnityEngine;
using System.Collections;

public class IAMov : MonoBehaviour {

    new Rigidbody rigidbody;

    public float accel = 0.3f, inertia = 0.9f, speedLimit = 5.0f, minSpeed = 3.0f, stopTime = 1.0f, rotationDamping = 0.9f;
    public float currentSpeed = 0.0f;
    public GameObject waypoint;
    public GameObject[] waypoints;
    public int wayPointIndex = 0;
    public int functionState;

    bool acceState, slowState, smoothRotation = true;

    public int lastWP;
    public Vector3 wPos;
    public Quaternion wRot;
    float dot;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        wPos.Set(407f, 10f, 396f);
        functionState = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

                dot = Vector3.Dot(transform.up, Vector3.up);

        if (dot < 0) volcado();

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

    public void volcado()
    {
        StartCoroutine(Example());
        GetComponentInParent<Transform>().rotation = wRot;
        GetComponentInParent<Transform>().position = wPos;
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(5f);

    }
}
