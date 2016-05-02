using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class KartControllerNewMode : MonoBehaviour
{
	public int currentWaypoint; // The current Waypoint where the kart is
	public int currentLap; //The current lap of the car
	public Transform lastWaypoint; // The last waypoint the car passed
	
	private static int WAYPOINT_VALUE = 100;
	private static int LAP_VALUE = 10000;
	private int lastWaypointNumber;
	
	//Get the number of the total waypoints of the circuit
	private int totalOfWaypoints; // Get the number of the initial last waypoint
	
	
	public Sprite Portrait; //The portrait of the kart
	public Sprite PortraitResults; //The portrait of the character in Results
	public GameObject myShadow; //The shadow will protect the racer
	
	public float topSpeedMPH = 35.0f;			// vehicle's top speed in miles per hour
	public float accelTime = 1.0f;				// time in seconds the vehicle takes to go from stationary to top speed
	public float traction = 0.4f;				// 0-1 value that determines how much traction the vehicle has on the road
	public float decelerationSpeed = 0.5f;		// 0-1 value that determines how quickly the vehicle comes to a rest when thrust is released5
	
	public Transform body;						// the kart body object
	
	public Transform wheelFL;					// location of front left wheel
	public Transform wheelFR;					// location of front right wheel
	public Transform wheelBL;					// location of rear left wheel
	public Transform wheelBR;					// location of rear right wheel
	
	public float wheelRadiusFront = 0.5f;		// radius of the front wheels in meters
	public float wheelRadiusBack = 0.6f;		// radius of the rear wheel in meters
	public float maxSteerAngle = 30.0f;			// this is maximum angle the wheels will turn
	
	public float steerSpeed = 0.5f;				// speed at which the vehicle turns
	public float offRoadDrag = 2.0f;			// drag mulitplier to apply when driving off-road
	public float airDrag = 0.5f;				// drag mulitplier to apply when the vehicle is not on the ground
	
	// this controls how much (if at all) the body of the kart is rotated when steering. it's totally physically incorrect
	// but can give a nice visual effect, making the kart seem to slide around the corners more.
	public float visualOversteerAmount = 0.2f;
	
	public float Thrust
	{
		get { return thrust; }
		set { thrust = value; }
	}
	
	public float Steering
	{
		get { return steer; }
		set { steer = value; }
	}
	
	public float MPH
	{
		get { return currentMPH; }
	}
	
	public bool IsGrounded
	{
		get { return isGrounded; }
	}
	
	
	public bool IsOffRoad
	{
		get { return isOffRoad; }
	}
	
	
	public bool IsOverturned
	{
		get { return isOverturned; }
	}
	
	
	public void Spin(float time)
	{
		spinTime = time;
		spinTimer = 0.0f;
	}
	
	
	public void Wiggle(float time)
	{
		wiggleTime = time;
		wiggleTimer = 0.0f;
	}
	
	
	public void Jump(float height)
	{
		// Make the vehicle jump up
		if(isGrounded)
		{
			float gravity = 9.81f;
			float verticalSpeed = Mathf.Sqrt(2.0f * height * gravity);
			Vector3 vel = GetComponent<Rigidbody>().velocity;
			vel.y += verticalSpeed;
			GetComponent<Rigidbody>().velocity = vel;
		}
	}
	
	
	public void SpeedBoost(float boostTopSpeedMPH, float boostAccelTime, float boostTime, float fadeTime)
	{
		boostMPH = boostTopSpeedMPH;
		boostAccel = boostAccelTime;
		boostAmount = 1.0f;
		boostAmountVel = 0.0f;
		boostTimer = boostTime;
		boostFadeTime = fadeTime;
		penaltyAmount = 0.0f;
	}
	
	
	public void SpeedBoost()
	{
		SpeedBoost(1.6f * topSpeedMPH, 0.25f * accelTime, 1.0f, 1.0f);
	}
	
	public void TurboSpeedBoost()
	{
		SpeedBoost (2f * topSpeedMPH, 0.35f * accelTime, 2.0f, 1.2f);
	}
	
	
	public void SpeedPenalty(float amount, float penaltyTime, float fadeTime)
	{
		penaltyAmount = amount;
		penaltyTimer = penaltyTime;
		penaltyFadeTime = fadeTime;
		penaltyAmountVel = 0.0f;
		boostAmount = 0.0f;
	}
	
	
	public void SpeedPenalty()
	{
		SpeedPenalty (0.4f, 0.5f, 1.0f);
	}
	
	#region Implementation
	
	private float thrust = 0.0f;			// our current thrust value
	private float steer = 0.0f;				// our current steering value
	
	private float currentMPH = 0.0f;		// our current speed in miles-per-hour
	
	// a couple of useful constants
	private const float metresToMiles = 1.0f / 1609.344f;
	private const float secondsToHours = 3600.0f;
	
	private bool isGrounded = true;			// are we on the ground?
	private bool isOffRoad = false;			// are we driving off road?
	private bool isOverturned = false;		// is the kart upside-down?
	
	private float engineThrust;				// current thrust value including any penalties
	
	private float visualOversteerVel;
	
	private WheelCollider[] wheelColliders;
	private GameObject steeringFL;
	private GameObject steeringFR;
	
	private float spinTime;
	private float spinTimer;
	
	private float wiggleTime;
	private float wiggleMaxAngle = 15.0f;	// maximum angle a wiggle rotates the vehicle by
	private float wiggleTimer;
	
	private float boostMPH;
	private float boostAccel;
	private float boostAmount;
	private float boostAmountVel;
	private float boostTimer;
	private float boostFadeTime;
	
	private float penaltyAmount;
	private float penaltyAmountVel;
	private float penaltyTimer;
	private float penaltyFadeTime;
	private float penaltyDrag = 10.0f;		// drag multiplier used to slow the vehicle during a penalty
	
	public void Initialize() 
	{
		currentWaypoint = 0;
		currentLap = 0;
		
		
	}
	
	void Start () 
	{
		
		//Get the total of waypoints of the circuit
		WaypointNumberNewMode prueba_script = lastWaypoint.GetComponentInParent<WaypointNumberNewMode>();
		totalOfWaypoints = prueba_script.Waypoint_number;
		
		//Get the total of waypoints of the circuit
		//WaypointNumber prueba_script = lastWaypoint.GetComponentInParent<WaypointNumber>();
		//totalOfWaypoints = prueba_script.Waypoint_number;
		
		// create some colliders around the wheels.
		wheelColliders = new WheelCollider[4];
		wheelColliders[0] = CreateWheelCollider(wheelFL.position, wheelRadiusFront);
		wheelColliders[1] = CreateWheelCollider(wheelFR.position, wheelRadiusFront);
		wheelColliders[2] = CreateWheelCollider(wheelBL.position, wheelRadiusBack);
		wheelColliders[3] = CreateWheelCollider(wheelBR.position, wheelRadiusBack);

		for (int i = 0; i < wheelColliders.Length; i++) 
		{
			wheelColliders[i].transform.parent = this.gameObject.transform;
		}
		
		// create some extra transforms so we can rotate the front wheels more easily for steering
		steeringFL = new GameObject("SteeringFL");
		steeringFR = new GameObject("SteeringFR");
		steeringFL.transform.position = wheelFL.position;
		steeringFR.transform.position = wheelFR.position;
		steeringFL.transform.rotation = wheelFL.rotation;
		steeringFR.transform.rotation = wheelFR.rotation;
		steeringFL.transform.parent = wheelFL.parent;
		steeringFR.transform.parent = wheelFR.parent;
		wheelFL.parent = steeringFL.transform;
		wheelFR.parent = steeringFR.transform;
		
		// set an artificially low center of gravity to aid in stability.
		Vector3 frontAxleCenter = 0.5f * (wheelFL.localPosition + wheelFR.localPosition);
		Vector3 rearAxleCenter = 0.5f * (wheelBL.localPosition + wheelBR.localPosition);
		Vector3 vehicleCenter = 0.5f * (frontAxleCenter + rearAxleCenter);
		float avgWheelRadius = 0.5f * (wheelRadiusFront + wheelRadiusBack);
		GetComponent<Rigidbody>().centerOfMass = vehicleCenter - 0.8f*avgWheelRadius*Vector3.up;
		
		topSpeedMPH = (int) Random.Range (topSpeedMPH - 5, topSpeedMPH + 5);
		
	}
	
	public void OnTriggerEnter(Collider other) {
		string otherTag = other.gameObject.tag;
		if (otherTag == "Waypoint") 
		{ //Is a waypoint
			WaypointNumberNewMode waypoint_script = other.gameObject.GetComponent<WaypointNumberNewMode> ();
			WaypointNumberNewMode lastwaypoint_script = lastWaypoint.GetComponentInParent<WaypointNumberNewMode> ();
			currentWaypoint = waypoint_script.Waypoint_number;
			lastWaypointNumber = lastwaypoint_script.Waypoint_number;
			
			if (currentWaypoint == 1 && lastWaypointNumber == totalOfWaypoints) // completed a lap, so increase currentLap;
				currentLap++;
			
			lastWaypoint = other.transform;
		}
		
		if (otherTag == "RampaTurbo") 
		{ //Is a waypoint
			SpeedBoost (2f * topSpeedMPH, 0.35f * accelTime, 2.0f, 1.2f);
		}
	}
	
	//Function to calculate the distance done of the kart, and in function of this calculate its position in the race
	public float GetDistance() {
		return (transform.position - lastWaypoint.position).magnitude  + currentWaypoint * WAYPOINT_VALUE + currentLap * LAP_VALUE;
	}
	
	
	private WheelCollider CreateWheelCollider(Vector3 position, float radius)
	{
		GameObject wheel = new GameObject("WheelCollision");
		wheel.transform.parent = body;
		wheel.transform.position = position;
		wheel.transform.localRotation = Quaternion.identity;
		
		WheelCollider collider = wheel.AddComponent<WheelCollider>();
		collider.radius = radius;
		collider.suspensionDistance = 0.1f;
		
		// we calculate our own sideways friction and slippage, so we don't need the wheel collider to do it too.
		WheelFrictionCurve sideFriction = collider.sidewaysFriction;
		sideFriction.stiffness = 0.01f;
		collider.sidewaysFriction = sideFriction;
		
		return collider;
	}
	
	void FixedUpdate()
	{
		// calculate our current velocity in local space (i.e. so z is forward, x is sideways etc)
		Vector3 relVel = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		// our current speed is the forward part of the velocity - note this will be negative if we are reversing.
		currentMPH = relVel.z * metresToMiles * secondsToHours;
		
		engineThrust = thrust;
		
		// cast a ray to check if we are grounded or not
		Vector3 frontWheelBottom = 0.5f*(wheelFR.position + wheelFL.position) - new Vector3(0,0.5f,1.0f) * wheelRadiusFront;
		RaycastHit hit;
		isGrounded = Physics.Raycast(frontWheelBottom, -Vector3.up, out hit, wheelRadiusFront);
		
		// check if the ground beneath us is tagged as 'off road'
		if(isGrounded)
			isOffRoad = hit.collider.gameObject.CompareTag("OffRoad");
		
		// check if the vehicle has overturned. we don't do anything with this, but a controller script could use it
		// to reset a vehicle that has been overturned for a certain amount of time for example.
		isOverturned = transform.up.y < 0.0f;
		
		// reduce the thrust if we are currently suffering a penalty.
		engineThrust *= (1.0f - penaltyAmount*penaltyAmount);
		
		// only apply thrust if the wheels are touching the ground
		if(isGrounded)
			ApplyThrust();
		ApplyDrag();
		ApplySteering();
		
		// update boost, penalty, spin effects etc 
		ApplyEffects();
		
		// calculate the angle that the wheels should have rolled since the last frame given our current speed
		float wheelRotationFront = (relVel.z / wheelRadiusFront) * Time.deltaTime * Mathf.Rad2Deg;
		float wheelRotationRear = (relVel.z / wheelRadiusBack) * Time.deltaTime * Mathf.Rad2Deg;
		// now rotate each wheel
		wheelFL.Rotate(wheelRotationFront, 0.0f, 0.0f);
		wheelFR.Rotate(wheelRotationFront, 0.0f, 0.0f);
		wheelBL.Rotate(wheelRotationRear, 0.0f, 0.0f);
		wheelBR.Rotate(wheelRotationRear, 0.0f, 0.0f);
	}
	
	private void ApplyDrag()
	{
		// get our velocity relative to our local orientation (i.e. forward motion is along z-axis etc)
		Vector3 relVel = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
		
		Vector3 drag = Vector3.zero;
		
		// calculate our drag coeeficients based on the current handling parameters
		
		// strength of drag force resisting the vehicle's forward motion
		float forwardDrag = Mathf.Lerp(0.1f, 0.5f, traction);
		// strength of drag force resisting the vehicle's sideways motion
		float lateralDrag = Mathf.Lerp(1.0f, 5.0f, traction);
		// strength of drag that slows the vehicle down when thrust is not pressed (basically just affects deceleration time)
		float engineDrag = Mathf.Lerp(0.0f, 5.0f, decelerationSpeed);
		
		// calculate drag in forward direction
		// engine drag slows the vehicle down when the accelerator is not being pressed
		drag.z = relVel.z * (forwardDrag + ((1.0f - Mathf.Abs(engineThrust)) * engineDrag));
		// add some additional drag when driving off road
		if(isOffRoad)
			drag.z += relVel.z * offRoadDrag;
		
		// lateral (sideways drag) slows the vehicle in the direction perpendicular to that in which it is facing.
		drag.x = relVel.x * lateralDrag;
		
		// when the vehicle is not grounded, reduce the drag force.
		if(!isGrounded)
			drag *= airDrag;
		
		// if we are currently suffering a penalty, then increase the drag to slow the car down.
		drag = Vector3.Lerp(drag, penaltyDrag * drag, penaltyAmount);
		
		// transform the drag force back into world space
		drag = transform.TransformDirection(drag);
		
		// apply the drag by reducing our current velocity directly
		Vector3 vel = GetComponent<Rigidbody>().velocity;
		vel -= drag * Time.deltaTime;
		GetComponent<Rigidbody>().velocity = vel;
	}
	
	private void ApplyThrust()
	{
		// calc our current top speed and acceleration values, taking into account any active boost.
		float topSpeed = Mathf.Lerp(topSpeedMPH, boostMPH, boostAmount);
		float accelerationTime = Mathf.Lerp(accelTime, boostAccel, boostAmount);
		accelerationTime = Mathf.Max(0.01f, accelerationTime);
		
		float topSpeedMetresPerSec = topSpeed / (metresToMiles * secondsToHours);
		// limit the speed the vehicle can move at in reverse
		float topSpeedReverse = 0.2f * topSpeed;
		// calculate our acceleration value in m/s^2
		float accel = topSpeedMetresPerSec / accelerationTime;
		
		// if we're at or over the top speed, then don't accelerate any more
		if(currentMPH >= topSpeed || currentMPH <= -topSpeedReverse)
			accel = 0.0f;
		
		// calculate our final acceleration vector
		Vector3 thrustDir = transform.forward;
		Vector3 accelVec = accel * thrustDir * engineThrust;
		
		// add our acceleration to our current velocity
		Vector3 vel = GetComponent<Rigidbody>().velocity;
		vel += accelVec * Time.deltaTime;
		GetComponent<Rigidbody>().velocity = vel;
		
		// apply the brakes automatically when the throttle is off to stop the vehicle rolling by itself.
		float brakeTorque = 0.0f;
		const float maxBrakeTorque = 20.0f;
		// modify the braking amount based on the current speed so we come to a gentle stop.
		if(engineThrust == 0.0f && currentMPH < 10.0f)
			brakeTorque = maxBrakeTorque * decelerationSpeed * (10.0f - currentMPH);
		foreach(WheelCollider wheel in wheelColliders)
			wheel.brakeTorque = brakeTorque;
	}
	
	private void ApplySteering()
	{
		float steerAngle = steer * maxSteerAngle;
		
		// rotate the front wheels
		steeringFL.transform.localRotation = Quaternion.Euler(0, steerAngle, 0);
		steeringFR.transform.localRotation = Quaternion.Euler(0, steerAngle, 0);
		
		// only turn the vehicle when we're on the ground and moving
		if(isGrounded && GetComponent<Rigidbody>().velocity.sqrMagnitude > 0.1f)
		{
			// reverse the steering direction when the vehicle is moving backwards
			Vector3 relVel = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
			steerAngle *= Mathf.Sign(relVel.z);
			
			// rotate the vehicle
			Quaternion steerRot = Quaternion.Euler(0, steerAngle * Time.deltaTime * (1.0f + 2.0f*steerSpeed), 0);
			GetComponent<Rigidbody>().MoveRotation(transform.rotation * steerRot);
			
			
			// also rotate the body a little for visual effect
			float currentOversteerAngle = body.localRotation.eulerAngles.y;
			float oversteerAngle = Mathf.SmoothDampAngle(currentOversteerAngle, visualOversteerAmount * steerAngle, ref visualOversteerVel, 0.5f);
			body.localRotation = Quaternion.Euler(0, oversteerAngle, 0);
		}
	}
	
	private float WiggleCurve(float t)
	{
		// basically a sine wave that smoothly fades out over 0-1
		float numWiggles = 3.0f;
		float wave = Mathf.Sin(t * numWiggles * 2.0f*Mathf.PI);
		float amplitude = (1.0f + 2.0f*t*t*t - 3.0f*t*t);
		return wave * amplitude;
	}
	
	private void UpdateSpin()
	{
		if(spinTime > 0.0f)
		{
			spinTimer += Time.deltaTime;
			// calculate how far through the spin we are.
			float t = spinTimer / spinTime;
			if(t >= 1.0f)
			{
				// spin has finished
				t = 0.0f;
				spinTime = 0.0f;
			}
			
			Vector3 rot = body.localRotation.eulerAngles;
			// simple easing curve to slow the rotation down towards the end.
			float easing = 1.0f - (1.0f-t)*(1.0f-t);
			// rotate the vehicle by some fraction of 720 degrees
			rot.y = 720.0f * easing;
			body.localRotation = Quaternion.Euler(rot);
		}
	}
	
	private void UpdateWiggle()
	{
		if(wiggleTime > 0.0f)
		{
			wiggleTimer += Time.deltaTime;
			// calculate how far through the wiggle we are.
			float t = wiggleTimer / wiggleTime;
			if(t >= 1.0f)
			{
				// wiggle has finished
				t = 0.0f;
				wiggleTime = 0.0f;
			}
			
			Vector3 rot = body.localRotation.eulerAngles;
			// rotation is given by the wiggle curve (basically a sine wave that fades out over 0-1)
			rot.y = wiggleMaxAngle * WiggleCurve(t);
			body.localRotation = Quaternion.Euler(rot);
		}
	}
	
	private void UpdateBoost()
	{
		boostTimer -= Time.deltaTime;
		if(boostTimer < 0)
		{
			// fade the boost out after the boostTimer has run out
			boostAmount = Mathf.SmoothDamp(boostAmount, 0.0f, ref boostAmountVel, boostFadeTime);
		}
	}
	
	private void UpdatePenalty()
	{
		penaltyTimer -= Time.deltaTime;
		if(penaltyTimer < 0)
		{
			// fade the penalty out after the penaltyTimer has run out
			penaltyAmount = Mathf.SmoothDamp(penaltyAmount, 0.0f, ref penaltyAmountVel, penaltyFadeTime);
		}
	}
	
	private void ApplyEffects()
	{
		// update all the different effects
		UpdateWiggle();
		UpdateSpin();
		UpdateBoost();
		UpdatePenalty();
	}
	
	#endregion
}
