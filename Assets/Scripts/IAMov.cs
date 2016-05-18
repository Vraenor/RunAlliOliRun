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
        wPos.Set(407f, 10f, 396f); //Posicion inicial de la IA por si vuelca antes de pasar por el primer waypoint
        functionState = 0; //Valor que decide si hay que acelerar o frenar
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dot = Vector3.Dot(transform.up, Vector3.up); //Vector que calcula la distancia de la parte superior de la nave con el suelo

        if (dot < 0) volcado(); //Comprobar si se ha volcado la nave al estar pegada al suelo con su aprte superior

        if (functionState == 0) Acce();

        if (functionState == 1) Slow();

        if (wayPointIndex >= waypoints.Length) wayPointIndex = 0; //Comprobacion de si ha pasado por el ultimo waypoint para resetear el contador

        waypoint = waypoints[wayPointIndex];//waypoint hacia el que debe dirigirse la nave
 
    }

    void Acce() //Funcion para aumentar la velocidad
    {

        if (acceState == false)
        {
            acceState = true;
            slowState = false;
        }

        if (waypoint) //si encuentra un waypoint
        {
            if (smoothRotation) //rotacion de la nave hacia la dirección con el siguiente waypoint
            {

                Quaternion rotation = Quaternion.LookRotation(waypoint.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            }
        }
        currentSpeed = currentSpeed + accel * accel;
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        if (currentSpeed >= speedLimit) currentSpeed = speedLimit; //si la velocidad es mayor al limite, igualarlas

    }

    void Slow() //Funcion para decelerar
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

    public void volcado() //Funcion para tratar el volcado de la nave
    {
        StartCoroutine(Example());
        GetComponentInParent<Transform>().rotation = wRot; 
        GetComponentInParent<Transform>().position = wPos;
    }

    IEnumerator Example() //Retraso
    {
        yield return new WaitForSeconds(1.5f);

    }
}
