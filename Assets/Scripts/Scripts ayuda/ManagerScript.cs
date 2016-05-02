using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ManagerScript : MonoBehaviour {

	public GameObject pauseMenu;
	public int numberOfLaps = 3;
	public int position_player = 0;
	public Image PortraitPlayer;
	public Text playerPosition;
	public Text Sufix;
	public CameraController playerCamera;

	public List<GameObject> players;
	public List<GameObject> IA;
	public List<Image> AllPortraits;
	public GameObject[] carObjects; 
	public KartController[] allCars; 
	public KartController[] carOrder; 


	private GameObject[] UI;
	private Text laps;
	private int playerSelected;
	private int maximum_players = 8; //Numero de corredores
	private int lapsNumber = 1;
	private Dictionary<KartController,int> dict_order_car;
	private GameObject player_selected;
	private GameObject numbers;



	// Use this for initialization
	public void Initialize () {

		UI = GameObject.FindGameObjectsWithTag ("UI");

		playerSelected = PlayerPrefs.GetInt ("playerSelected");

		if (playerSelected == null) playerSelected = 1;
		for (int i = 0; i < maximum_players; i++) {
			if (i == playerSelected - 1) {
				players[i].SetActive(true);
				freezeAndPrepareTheKartsForStart(players[i]);
				assignCurrentPlayer(players[i]);
				IA[i].SetActive(false);

				
			}
			else
			{

				IA[i].SetActive(true);
				freezeAndPrepareTheKartsForStart(IA[i]);

			}
		}

		laps = GameObject.FindGameObjectWithTag ("NumberOfLaps").GetComponent<Text> ();
		laps.text = "LAP 1";


		// set up the car objects
		allCars = new KartController[carObjects.Length];

		carObjects [0] = player_selected;

		//The loop below assign the race's kart
		//depending of the IA selected previously
		int k = 1;
		for (int i = 0; i < carObjects.Length; i++) {
			if (IA[i].activeSelf) {
				carObjects[k] = IA[i];
				k++;
			}
		}

		for (int i = 0; i < carObjects.Length; i++) {
			allCars[i] = carObjects[i].GetComponent<KartController>();
		}


		dict_order_car = new Dictionary<KartController,int > ()
		{
			{allCars[0],0},
			{allCars[1],0},
			{allCars[2],0},
			{allCars[3],0},
			{allCars[4],0},
			{allCars[5],0},
			{allCars[6],0},
			{allCars[7],0},
		};



	}

	public void StartMovement()
	{
		for (int i = 0; i < carObjects.Length; i++) {
			carObjects[i].GetComponent<KartController>().enabled = true;
			carObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		}

		for (int i = 0; i < AllPortraits.Count; i++) {
			Color c = AllPortraits[i].color;
			c.a = 255;
			AllPortraits[i].color= c;
		}

		numbers = GameObject.FindGameObjectWithTag("Numbers");
		Color num = numbers.GetComponent<Image> ().color;
		num.a = 255;
		numbers.GetComponent<Image> ().color = num;




	}


	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Space)) {
			Time.timeScale = 0;	
			playerCamera.GetComponent<AudioSource> ().Pause ();
			for (int i = 0; i < UI.Length; i++) {
				UI[i].SetActive(false);
			}
			player_selected.GetComponent<MinimapScript>().enabled = false;
			pauseMenu.SetActive (true);
		}




	//	lapsNumber = GameObject.FindGameObjectWithTag ("Player").GetComponent<KartController> ().currentLap / 2;
		lapsNumber = GameObject.FindGameObjectWithTag ("Player").GetComponent<KartController> ().currentLap;
		if (lapsNumber == 0) {
			lapsNumber = 1;
		}
		laps.text = "LAP " + lapsNumber.ToString ();


			foreach (KartController car in allCars) {
				
				dict_order_car[car] = (int)car.GetDistance();

			}
			
			var items = from pair in dict_order_car
				orderby pair.Value descending
					select pair;
			
			int j = 0;

			foreach (KeyValuePair<KartController, int> pair in items) {

				 //Para que de los 8 jugadores me cuente solo a los 4 primeros
				carOrder[j] = pair.Key;
				if (j >= 0 && j < 4) 
									
					AllPortraits[j].sprite = carOrder[j].Portrait ; 
							
						
				if (carOrder[j] == player_selected.GetComponent<KartController>()) 
				{
					position_player = j;

				}


				if (j == 0) 
				{
					if (carOrder[j].GetComponent<IAPowerUp>() != null) {
						carOrder[j].GetComponent<IAPowerUp>().primero = true;
					}
					else
						carOrder[j].GetComponent<MyPowerUp>().primero = true;
				}

				else
				{
					if (carOrder[j].GetComponent<IAPowerUp>() != null) {
						carOrder[j].GetComponent<IAPowerUp>().primero = false;
					}
					else
						carOrder[j].GetComponent<MyPowerUp>().primero = false;
				}



				if (j == maximum_players - 1 || j == maximum_players - 2) {
					if (carOrder[j].GetComponent<IAPowerUp>() != null) {
						carOrder[j].GetComponent<IAPowerUp>().ultimo = true;
					}
					else
						carOrder[j].GetComponent<MyPowerUp>().ultimo = true;
				}
			    else
				{
					if (carOrder[j].GetComponent<IAPowerUp>() != null) {
						carOrder[j].GetComponent<IAPowerUp>().ultimo = false;
					}
					else
					carOrder[j].GetComponent<MyPowerUp>().ultimo = false;
				}

				j++;
			}
			j = 0;

		switch (position_player) {
		case 0:
			playerPosition.text = "1";
			Sufix.text = "st";
			break;
		case 1:
			playerPosition.text = "2";
			Sufix.text = "nd";
			break;
		case 2:
			playerPosition.text = "3";
			Sufix.text = "rd";
			break;
		case 3:
			playerPosition.text = "4";
			Sufix.text = "th";
			break;
		case 4:
			playerPosition.text = "5";
			Sufix.text = "th";
			break;
		case 5:
			playerPosition.text = "6";
			Sufix.text = "th";
			break;
		case 6:
			playerPosition.text = "7";
			Sufix.text = "th";
			break;
		case 7:
			playerPosition.text = "8";
			Sufix.text = "th";
			break;
		}
	

	}

	void freezeAndPrepareTheKartsForStart(GameObject kart)
	{
		kart.GetComponent<KartController>().enabled = false;
		kart.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

	void assignCurrentPlayer(GameObject player)
	{
		playerCamera.target = player.transform;
		player.tag = "Player";
		player_selected = player; // This is to set who player im controlling
	}
}
