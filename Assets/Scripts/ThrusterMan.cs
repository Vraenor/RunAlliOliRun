using UnityEngine;
using System.Collections;

public class ThrusterMan : MonoBehaviour {

    public float thrusterStrength;
    public float thrusterDistance;
    public Transform[] thrusters;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

	void FixedUpdate () {

        //Codigo del tutorial Hovercar

        RaycastHit hit;

        foreach (Transform thruster in thrusters)
        {
            Vector3 downwardForce;
            float distancePercentage;
            Vector3 aux = thruster.up * -1; //Vector que calcula la distancia con el suelo
            //Debug.DrawRay(thruster.position, aux, Color.green);
            if (Physics.Raycast(thruster.position, thruster.up * -1, out hit, thrusterDistance))
            {
                if (aux.magnitude >= 10)
                {
                    GetComponentInParent<Transform>().rotation.eulerAngles.Set(0, 160, 0);
                    //Debug.DrawRay(thruster.position, aux, Color.red);
                }

                // The thruster within thrusterDistance to the ground
                distancePercentage = 1 - (hit.distance / thrusterDistance);

                // Calculate how much force to push
                downwardForce = transform.up * thrusterStrength * distancePercentage;

                // Correct the force for the mass of the car and deltatime
                downwardForce = downwardForce * Time.deltaTime * rigidbody.mass;

                // Apply the force where the thruster is
                rigidbody.AddForceAtPosition(downwardForce, thruster.position);
            }

        }
	
	}




}
