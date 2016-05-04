using UnityEngine;
using System.Collections;

//Sample script for other purposes of the game:
//make thee aparition trailer of Hoarii

public class WaitAndInstantiate : MonoBehaviour {

	public GameObject hoarii;
	private ExampleAIController ai;
	public float wait_Time;
	public Transform spawn_Position,destination;


	// Use this for initialization
	void Start () {
	
		ai = hoarii.GetComponent<ExampleAIController> ();
		StartCoroutine (WaitForSpawn ());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator WaitForSpawn ()
	{

		yield return new WaitForSeconds(wait_Time);
		Instantiate (hoarii, spawn_Position.position, Quaternion.identity);
		ai.waypoints [0] = destination;
		hoarii.SetActive (true);
	}
}
