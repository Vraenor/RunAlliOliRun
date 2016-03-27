using UnityEngine;
using System.Collections;

public class ThrusterMan : MonoBehaviour {

    public float thrusterStrength;
    public float thrusterDistance;
    public Transform[] thrusters;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

	void FixedUpdate () {

        RaycastHit hit;

        foreach (Transform thruster in thrusters)
        {

            Vector3 downwardForce;
            float distancePercentage;

            if (Physics.Raycast (thruster.position, thruster.up * -1, out hit, thrusterDistance))
            {
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
