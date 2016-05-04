using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EndRaceAndDisplayResultsNewMode : MonoBehaviour {
	
	public List<Image> PortraitResults; //Images where later will be put the character's faces
	public KartControllerNewMode[] KartResults; //Direct reference of carOrder array: will be initialized later
	public ManagerScriptNewMode manager;
	public GameObject Portrait; // Parent of all the portraits: initially in the game turned off
	public GameObject RetryObject; //Letters of Retry
	public GameObject ExitObject; // Letters of exit
	public GameObject cursor;
	public AudioClip resultsMusicWinner;
	public AudioClip resultsMusicLoser;
	
	private int cursorPosition = 1; // To move the cursor in the final results
	private GameObject playerCamera;
	private AudioSource camera_music;
	
	
	// Use this for initialization
	void Start () {
		
		//We put off all those objects to not be displayed at the start of the display results screen
		RetryObject.SetActive (false);
		ExitObject.SetActive (false);
		cursor.SetActive (false);
		
		//Get the final order of karts at the end of the race
		KartResults = manager.carOrder;
		//Find the camera: the camera store the background music
		playerCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		camera_music = playerCamera.GetComponent<AudioSource> ();
		//If the player is from 1st to 4th position, winner music
		//Else, loser music
		for (int i = 0; i < KartResults.Length; i++) {
			if (KartResults [i].GetComponent<MyPowerUpNewMode> () != null)
			if (i >= 0 && i < 4) {
				camera_music.clip = resultsMusicWinner;
				break;
			}
			else
			{
				camera_music.clip = resultsMusicLoser;
				break;
			}
		}		
		//We play it delayed to let other effect of final line sound
		camera_music.PlayDelayed (1.5f);
		//Deactivate the loop property, active in the race
		camera_music.loop = false;
		
		StartCoroutine (displayResults ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (cursorPosition < 1 ) {
			cursorPosition = 1;
		}
		else if (cursorPosition >= 3) {
			cursorPosition = 2;
		}
		
		if (cursorPosition == 1) {
			cursor.transform.position = RetryObject.transform.position;
		}
		
		else if (cursorPosition == 2) {
			cursor.transform.position = ExitObject.transform.position;
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			cursorPosition --;
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			cursorPosition ++;
		}
		
		if (Input.GetKeyDown(KeyCode.A)) {
			switch (cursorPosition)
			{
			case 1:
				Application.LoadLevel (Application.loadedLevelName);
				break;
			case 2:
				Application.LoadLevel(5);
				break;
			}
		}
		
	}
	
	IEnumerator displayResults()
	{
		//Set active the parent of all the objects of result screen
		Portrait.SetActive (true);
		yield return new WaitForSeconds(2f);
		//All the objects are put on but with alpha = 0 (initially).
		//Here we put them totally visible
		for (int i = PortraitResults.Count - 1; i >= 0; i--) {
			yield return new WaitForSeconds(0.5f);
			Color opaque = PortraitResults[i].color;
			opaque.a = 1.0f;
			PortraitResults[i].color = opaque;
			PortraitResults[i].sprite = KartResults[i].PortraitResults;
		}
		
		//Finally, set active the options of the menu, to let the player
		//get out
		RetryObject.SetActive (true);
		ExitObject.SetActive (true);
		cursor.SetActive (true);
		
		
	}
}
