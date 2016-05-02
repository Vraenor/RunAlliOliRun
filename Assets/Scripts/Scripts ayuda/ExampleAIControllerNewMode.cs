using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(KartControllerNewMode))]
public class ExampleAIControllerNewMode : MonoBehaviour {
	// target position for the ai, move this around the track a little way in front of the kart to make the kart
	// drive around the track. You can test this out in the editor by dragging it around the track as the game
	// is playing.
	
	//Circuito en el que nos encontramos
	private int circuit; 
	
	//Variables Nuevas Control del autorazonamiento de la ia (relacionadas con la marcha atras)
	public float kartVelocity;
	public float kartThrust;
	public float distanceToObstacle;
	public bool compruebaCol;
	public bool activamarchaatras;
	
	
	//Temporizadores de comprobador de colision y marcha atras (se substituira en un futuro por distancias)
	public float tiempoMarchaAtras = 2.0f;
	public float tiempoCompruebaColision = 0.5f;
	
	
	//List of waypoints. Must be in order, taking into account that the second element won't be
	// taking into account
	public List<Transform> waypoints;
	public GameObject ObstaclesList;
	public Transform targetObject;
	public GameObject manager;
	
	private Transform obstaculo;
	private int currentWaypointNumber = 0; // Indice de waypoints generales
	private int carOrderindex = 0;
	private bool activado = false; // To check that doesn't trigger two colliders at the same time
	private KartControllerNewMode kart;
	private List<GameObject> ObstaclesListRunTime;
	private IAPowerUpNewMode IaPowerUpScript;

	private bool boolTurbo;
	private int n_of_powerup_private;
	private KartControllerNewMode[] carOrder;

	private ManagerScriptNewMode managerScript;
	private GameObject myKartPair;
	public bool usarTurbo;

	
	/*private Transform WCercano;
	private Transform WMediano;
	private Transform WLejano;
	
	public bool turbo = false;

	float distanciaMinimaTurbo = 50f;*/
	
	
	void Awake()
	{
		managerScript = manager.GetComponent<ManagerScriptNewMode> ();
		carOrder = managerScript.carOrder;

	}
	
	void Start () {
		circuit = PlayerPrefs.GetInt ("courseSelected", 1);
		//manager = GameObject.FindGameObjectWithTag ("Manager");
	

		// keep a reference to the kart component
		kart = GetComponent<KartControllerNewMode>();

		targetObject = waypoints [currentWaypointNumber]; // Set the first target as the first element of the waypoint list
		ObstaclesListRunTime = ObstaclesList.GetComponent<ObstaclesListNewMode> ().obstacles;
		IaPowerUpScript = this.GetComponent<IAPowerUpNewMode> ();
		if (this.tag != "Pair") 
		{
			boolTurbo = waypoints [0].GetComponent<WaypointNumberNewMode> ().turbo;
			myKartPair = kart.myShadow;

		}
		n_of_powerup_private = IaPowerUpScript.n_of_powerup;
	}
	
	void Update () 
	{



		/*//IDEA: usar fwd de waypoints para calcular angulo
		
		//float distanciaMinimaTurbo = Vector3.Distance (waypoints[10].position, waypoints[8].position);
		//Debug.Log (distanciaMinimaTurbo); //Con esto sabemos que esa distancia es aproximadamente 50
		
		if (i == waypoints.Count - 2) {
			WCercano = waypoints [i];
			WMediano = waypoints [i + 1];
			WLejano = waypoints [0];
		} 
		else if (i == waypoints.Count - 1) {
			WCercano = waypoints [i];
			WMediano = waypoints [0];
			WLejano = waypoints [1];
		} 
		else {
			WCercano = waypoints [i];
			WMediano = waypoints [i + 1];
			WLejano = waypoints [1+2];
		}
		
		float distanciaCercanoLejano = Vector3.Distance (WLejano.position, WCercano.position);
		Vector3 cercanoMediano = WMediano.position - WCercano.position;
		Vector3 cercanoLejano = WLejano.position - WCercano.position;
		float angleOfTurbo = Vector3.Angle (cercanoMediano, cercanoLejano);
		
		if (distanciaCercanoLejano >= distanciaMinimaTurbo) {
			if (angleOfTurbo <= 25) {
				turbo = true;
			} 
			else turbo = false;
		} 
		else turbo = false;*/
			
			
			//Controlar si pone la marcha atras etc
			kartThrust = kart.Thrust;
		kartVelocity = kart.MPH;
		
		if (compruebaCol == true)
		{
			if(kartVelocity < 1.0f)
			{
				activamarchaatras = true;
			}
			
			waitNewColision();
		} 
		
		
		//Move the kart towards the current targetObject
		if(targetObject != null)
		{
			for (int j = 0; j < ObstaclesListRunTime.Count; j++)
			{
				obstaculo = ObstaclesListRunTime[j].transform;
				
				
				//Hallamos el vector formado por el coche (origen) y el obstaculo (arbol)
				Vector3 directionToObstacle = obstaculo.position - transform.position;
				float distanceToObstacle = Vector3.Distance (obstaculo.position, transform.position);
				//Hallamos el angulo formado por el vector previo y el vector forward del coche
				float angleOfCollision = Vector3.Angle (directionToObstacle, transform.forward);
				
				if (distanceToObstacle <= 6 )
				{
					//Debug.Log("Entra aqui");
					if (angleOfCollision < 60f && angleOfCollision >= 0f) {
						kart.Thrust = 0.2f;
						kart.Steering = 1.0f;
					} 
					else if (angleOfCollision < 0f && angleOfCollision > -60f) {
						kart.Thrust = 0.2f;
						kart.Steering = -1.0f;
					}
					else
					{
						correrAVelocidadNormal();
					}
				} 
				else
				{
					correrAVelocidadNormal();
				}
				
			}
			
			
		}
		else
		{
			kart.Thrust = 0.0f;
			kart.Steering = 0.0f;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{

		if (this.tag != "Pair")
		{

			if (other.tag == "Trees") 
			{
				compruebaCol = true;
			}
			
			if (other.tag == "Waypoint" )
			{

				if (!activado) {
					
					currentWaypointNumber++;
					//If the kart has passed all the elements of the waypoints list, 
					//loop through it again
					if (currentWaypointNumber == waypoints.Count -1) {
						targetObject = waypoints[0];
						currentWaypointNumber = 0;
						
					}
					else
					{

						//Move on to the next waypoint
						targetObject = waypoints [currentWaypointNumber+1];
					}
					
					if (circuit == 1) 
					{
						boolTurbo = waypoints [currentWaypointNumber].GetComponent<WaypointNumberNewMode> ().turbo;
						if(boolTurbo) usarTurbo = true;
						else usarTurbo = false;
						n_of_powerup_private = IaPowerUpScript.n_of_powerup;
						if (n_of_powerup_private != -1)
						{
							switch (n_of_powerup_private)
							{
							case 0:
								if (usarTurbo){
									StartCoroutine(esperaTurbo());
									//kart.SpeedBoost();
									//n_of_powerup_private = -1;
								}
								break;
								
								// Si hay mas de un powerup hay que añadir casos al switch
							}
						}
					}
					
					activado = true;
					
					StartCoroutine(WaitForIncrement());
				}
			}
			
			/*if (other.tag == "Obstaculo") {
			//Para que el angulo sea hallado en el plano XY
			//Vector3 aux = new Vector3 (1, 1, 0);
			//Hallamos el vector formado por el coche (origen) y el obstaculo (arbol)
			Vector3 directionToObstacle = obstaculo.position - transform.position;
			//Hallamos el angulo formado por el vector previo y el vector forward del coche
			float angleOfCollision = Vector3.Angle (directionToObstacle, transform.forward);
			
			if (angleOfCollision < 60f && angleOfCollision >= 0f){
				//reducir velocidad
				kart.Steering = 1.0f;
			}
			else if (angleOfCollision < 0f && angleOfCollision > -60f){
				//reducir velocidad
				kart.Steering = -1.0f;
			}
			else if (angleOfCollision >= 60f || angleOfCollision <= -60f){
				//Aumenta velocidad
			}
		}*/
		}

		
		
	}
	
	//Wait a little to not trigger the same collider twice
	IEnumerator WaitForIncrement()
	{
		yield return new WaitForSeconds(0.5f);
		activado = false;
	}
	
	//InventoMaximo
	void temporizadorMarchaAtras()
	{
		
		if(tiempoMarchaAtras <= 0)
		{
			activamarchaatras = false;
			tiempoMarchaAtras = 2.0f;
		}
		//yield return new WaitForSeconds(4.0f);
		tiempoMarchaAtras -= Time.deltaTime;
		//compruebaCol = false;
	}
	
	//InventoMaximo
	void waitNewColision()
	{
		
		if(tiempoCompruebaColision <= 0)
		{
			compruebaCol = false;
			tiempoCompruebaColision = 0.5f;
		}
		//yield return new WaitForSeconds(4.0f);
		tiempoCompruebaColision -= Time.deltaTime;
		//compruebaCol = false;
	}
	
	void correrAVelocidadNormal()
	{
		// get a vector from our current position to the target
		Vector3 delta = targetObject.position - transform.position;
		// transform that vector so that it is relative to the current facing direction of the kart
		Vector3 directionToObject = transform.InverseTransformDirection(delta);
		// now compute the angle from our current facing direction to the direction we want to travel in
		float angleToTarget = Mathf.Atan2(directionToObject.x, directionToObject.z) * Mathf.Rad2Deg;
		
		// convert the angle to a -1 => 1 steering value
		kart.Steering = angleToTarget / kart.maxSteerAngle;
		
		// always try and drive at top speed, for a more realistic ai you'll want to change this when
		// approaching sharp bends and so on.
		if (activamarchaatras == true)
		{
			//compruebaCol = true;
			kart.Thrust = -1.0f;
			kart.Steering = 0.5f;
			
			temporizadorMarchaAtras();
		} 
		else 
		{
			
			kart.Thrust = 1.0f;
		}
	}
	
	bool Exists(Transform PowerUp)
	{
		if (PowerUp != null) 
			return true;
		return false;
		
	}
	
	IEnumerator esperaTurbo ()
	{
		yield return new WaitForSeconds (2.5f);
		kart.SpeedBoost ();
		n_of_powerup_private = -1;
	}
}