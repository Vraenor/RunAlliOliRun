using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StartRaceNewMode : MonoBehaviour {
	
	public GameObject Manager;
	public GameObject LoopingPowerUp;
	public List <Sprite> TrafficLightsOrder;
	public AudioSource camera_music;
	public AudioClip backgroundmusic;
	private GameObject music;
	
	
	private Image trafficLights;
	
	
	// Use this for initialization
	void Start () {
		
		//Found the sound preserved from the main menu
		music = GameObject.Find ("Music");
		if (music) {
			//Destroy it: we don't want it to sound anymore
			Destroy(music);
		}
		
		//LoopingPowerUp.SetActive (false);
		StartCoroutine (startingTheRace ());
		trafficLights = this.GetComponent<Image> ();
		
	}
	
	IEnumerator startingTheRace()
	{
		//Place the cars and select them from all the options
		//This functions do that
		Manager.GetComponent<ManagerScriptNewMode> ().Initialize ();
		
		//Sequence to play the traffic lights , light the traffic ligths...
		yield return new WaitForSeconds(2f);
		camera_music.Play ();
		trafficLights.sprite = TrafficLightsOrder [0];
		
		yield return new WaitForSeconds(1f);
		
		trafficLights.sprite = TrafficLightsOrder [1];
		
		
		yield return new WaitForSeconds(1.5f);
		
		trafficLights.sprite = TrafficLightsOrder [2];
		
		
		yield return new WaitForSeconds(1.5f);
		
		trafficLights.sprite = TrafficLightsOrder [3];
		//Rescale the image of the traffic lights
		Vector3 newScale = new Vector3 (0.7f,this.transform.localScale.y,this.transform.localScale.z);
		this.transform.localScale = newScale;
		camera_music.clip = backgroundmusic;
		camera_music.Play ();
		camera_music.loop = true;
		camera_music.volume = 0.24f;
		
		//And start the race!
		Manager.GetComponent<ManagerScriptNewMode> ().StartMovement ();
		//Activate the manager to control the positions and all the things
		Manager.SetActive (true);
		//The looping power up is active, but hidden until  the kart hits one
		// power up. See MyPowerUp power up
		LoopingPowerUp.SetActive (true);
		
		
		
	}
}
