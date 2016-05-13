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
    public Vector3 wPos;
    public Quaternion wRot;
    public int currentPos;
    public int currentLap;
    public int lastWP;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        wPos.Set(413f, 10f, 397f);
        currentLap = 1;
        currentPos = 1;
    }

    void FixedUpdate () {

        //dot = Vector3.Dot(transform.up, Vector3.up);

        //if (dot < 0) StartCoroutine(Example());

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

        if (Input.GetAxis("Horizontal") == 0)
        {
            rigidbody.angularDrag = 0.8f;

        }
        else
        {
            rigidbody.angularDrag = 0;
        }
        
        // You can turn in the air or the ground
        Vector3 turnTorque = Vector3.up * rotationRate * Input.GetAxis("Horizontal");

        // Correct force for deltatime and vehicle mass
        turnTorque = turnTorque * Time.deltaTime * rigidbody.mass;
        rigidbody.AddTorque(turnTorque);
        EngineAudio();
	}

    public void EngineAudio() {
       if(forwardForce.x != 0 )
        {
            if (movementAudio.clip == stop) {
                movementAudio.clip = forward;
                Debug.Log("eooooo");
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
            Debug.Log("WOLOLO");
        }

    }

    public void volcado()
    {
        GetComponentInParent<Transform>().rotation = wRot;
        GetComponentInParent<Transform>().position = wPos;

    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(1.5f);
        volcado();
    }
}
