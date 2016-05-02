using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WaypointNumberNewMode : MonoBehaviour {
	
	//Simply stores a number of waypoint, that will be useful 
	// to avoid bugs with the circuit, as
	// make the lap counter increases by only passing 
	//over the goal
	public int Waypoint_number;
	public bool turbo;
	private int i;
	public List<Transform> listaWaypoints;
	public Transform cercano;
	public Transform lejano;
	float distanciaMinimaTurbo;
	
	//Circuito
	private int circuit; 
	
	
	void Start () 
	{
		circuit = PlayerPrefs.GetInt ("courseSelected", 1);
		if (circuit == 1) 
		{
			turbo = false;
			distanciaMinimaTurbo = 40f;
		}
	}
	
	
	void Update(){
		if (circuit == 1) {
			//if (Waypoint_number == 1) Debug.Log (actual);
			if (Waypoint_number == 13) {
				cercano = listaWaypoints [Waypoint_number - 1 + 1];
				lejano = listaWaypoints [0];
			} else if (Waypoint_number == 14) {
				cercano = listaWaypoints [0];
				lejano = listaWaypoints [1];
			} else {
				cercano = listaWaypoints [Waypoint_number - 1 + 1];
				lejano = listaWaypoints [Waypoint_number - 1 + 2];
			}
			
			float distanciaCercanoLejano = Vector3.Distance (lejano.position, transform.position);
			Vector3 actualCercano = cercano.position - transform.position;
			Vector3 actualLejano = lejano.position - transform.position;
			float angleOfTurbo = Vector3.Angle (actualCercano, actualLejano);
			
			if (distanciaCercanoLejano >= distanciaMinimaTurbo) {
				if (angleOfTurbo <= 20) {
					turbo = true;
				} else
					turbo = false;
			} else
				turbo = false;
		}
	}
	
	
}
