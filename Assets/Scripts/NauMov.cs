using UnityEngine;
using System.Collections;

public class NauMov : MonoBehaviour {

    // Values that control the vehicle
    public float acceleration;
    public float rotationRate;
    public Vector3 forwardForce;
    //audio of the engine 
    public AudioSource movementAudio;
    public AudioClip forward;
    public AudioClip stop;

    // Values for taking a nice turn display
    public float turnRotationAngle;
    public float turnRotationSeekSpeed;

    // Reference variables we don't directly use
    private float rotationVelocity;
    private float groundAngleVelocity;

    new Rigidbody rigidbody;

    float dot;
    public int currentPos;
    public int currentLap;
    public int lastWP;
    public GameObject lastWaypoint;

    private bool respawn = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentLap = 1;
        currentPos = 1;
    }

    void FixedUpdate () {

<<<<<<< HEAD
        dot = Vector3.Dot(transform.up, Vector3.up); //Calculo de la distancia de la parte superior de la nave con el suelo

        if (dot < 0 && !respawn) StartCoroutine(Example()); //Si la nave ha volcado, hacer el respawn
        
        //Codigo tutorial hovercar
=======
        dot = Vector3.Dot(transform.up, Vector3.up);
>>>>>>> origin/master

        if (dot < 0 && !respawn) StartCoroutine(Example());
        
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

        if (Input.GetAxis("Horizontal") == 0) //Si no se esta pulsando las teclas de rotacion, se aumenta el anguladrag para que decrezca la velocidad de rotacion hasta que para
        {
            rigidbody.angularDrag = 0.8f;

        }
        else //Mantiene el anguladrag a 0 para que la nave siga girando mientras haya input por parte del jugador
        {
            rigidbody.angularDrag = 0;
        }
        
        // You can turn in the air or the ground
        Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis("Horizontal");

        // Correct force for deltatime and vehicle mass
        turnTorque = turnTorque * Time.deltaTime * rigidbody.mass;
        rigidbody.AddTorque(turnTorque);
        EngineAudio(); //Llamada al audio
	}

    public void EngineAudio() { //Audio dependiendo de si hay fuerza hacia delante o no
       if(forwardForce.x != 0 )
        {
            if (movementAudio.clip == stop) {
                movementAudio.clip = forward;
                movementAudio.Play();
                movementAudio.loop = true;
            }
        }
        else
        {
            if (movementAudio.clip == forward)
            {
                movementAudio.loop = false;
                movementAudio.clip = stop;
                movementAudio.Play();
            }
        }
    }

    public void volcado() //Eliminacion de la nave volcada y respawn de una nueva
    {

        GameManager aux = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        aux.m_NumberofPlayers--;
<<<<<<< HEAD
        aux.lastWaypoint = lastWaypoint; //ultimo waypoint por el que ha pasado, donde se creara la nueva nave
=======
        aux.lastWaypoint = lastWaypoint;
>>>>>>> origin/master
        aux.numberWaypoint = lastWP;
        aux.m_LapNumber = currentLap;
        Destroy(gameObject);
    }

    IEnumerator Example()
    {
        respawn = true;
        yield return new WaitForSeconds(1.5f);
        volcado();
    }
}
