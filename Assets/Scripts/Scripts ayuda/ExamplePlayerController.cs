using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//To help the user of Unity to remember that is attached with a kartcontroller
[RequireComponent(typeof(KartController))]
public class ExamplePlayerController : MonoBehaviour 
{
	//public Text changeGravityText;
	public Text finalLapText;
	public List <AudioClip> enviromentalEffects;
	public ManagerScript manager;
	public AudioClip finalLapTheme;
	private bool finalLapDisplayed;
	private AudioSource camera_music;
	private int lapsNumber;
	private GameObject[] UI; //The parent object of all that is in the UI.Also included the HUD
	private GameObject powerUpImage;
	private MinimapScript minimap; //El minimapa
	private int distanceToEndRace; //3 laps = 35000
	private int finalLap; // 2 vueltas = 25000
	private CameraController playerCamera;
	private KartController kart;
	private ExampleAIController IAController;
	private GameObject UIResult;

	void Start () 
	{
		// keep a reference to the kart component
		kart = GetComponent<KartController>();

		lapsNumber = manager.numberOfLaps;
		distanceToEndRace = lapsNumber * 11667; // Calculations to assign the number of laps
		finalLap = distanceToEndRace - 10000; // The lap before the end
		UIResult = GameObject.FindGameObjectWithTag ("UIResult");
		IAController = GetComponent<ExampleAIController> ();
		playerCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraController> ();
		camera_music = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioSource> ();
		powerUpImage = GameObject.FindGameObjectWithTag ("PowerUpLoop");
		UI = GameObject.FindGameObjectsWithTag ("UI");
		minimap = this.GetComponent<MinimapScript> ();

	}
	
	void Update () 
	{
		// read the input from the controller, and send it straight to the kart controller
		kart.Thrust = Input.GetAxis("Vertical");
		kart.Steering = Input.GetAxis("Horizontal");


		//Debug.Log (kart.Steering);
        

		//To make a funny movement going forward
		if(Input.GetKeyDown(KeyCode.A))
			kart.Wiggle(2.0f);


		//If jumps, play the sound
		if (Input.GetKeyDown (KeyCode.W)) {
			kart.gameObject.GetComponent<AudioSource> ().clip =
				enviromentalEffects[0]; // Este clip debe ser boing
			kart.gameObject.GetComponent<AudioSource> ().Play ();
            
			kart.Jump (1.0f);
		}

		//If get to the final lap, display the text
		if (kart.GetDistance() > finalLap && !finalLapDisplayed) {
			StartCoroutine(displayFinalLapTextAndSound());
			finalLapDisplayed = true;
		}

		//End the race
		if (kart.GetDistance() > distanceToEndRace) {


			kart.gameObject.GetComponent<AudioSource> ().clip =
				enviromentalEffects[1]; // Must be FinalLineSound
			kart.gameObject.GetComponent<AudioSource> ().Play ();
			//Turn the camera to see player's face
			playerCamera.followOffset.z = -playerCamera.followOffset.z;

			//Now the player's kart will be moved by an IA
			IAController.enabled = true;
			IAController.targetObject = IAController.waypoints[2];
			//Deactivate all but the minimap and the powerupimage
			for (int i = 0; i < UI.Length; i++) {
				UI[i].SetActive(false);
			}
			//Deactivate the minimap and the PowerUp
			minimap.enabled = false;
			powerUpImage.SetActive(false);
			//Set active the UI Results layout
			UIResult.GetComponent<EndRaceAndDisplayResults>().enabled = true;
			//Turn off the player input
			this.enabled = false;

		}

	}

	IEnumerator displayFinalLapTextAndSound()
	{
		finalLapText.text = "Last Lap!";
		camera_music.clip = finalLapTheme;
		camera_music.Play ();
		finalLapText.enabled = true;
		yield return new WaitForSeconds(2f);
		finalLapText.enabled = false;
	}


}
