using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IAPowerUp : MonoBehaviour {

	public ManagerScript manager;
	public bool ultimo = false; // Bool to let the IA to pick a lightning power up
	public bool primero = false;
	public List<GameObject> powerups;
	public GameObject ObstaclesList;

	private KartController[] allKarts; //The carts of this race
	public int n_of_powerup = -1; //Int that will control the power_up picked
	private Image loopingPowerUpImage; //The loop of the power ups: always looping, but active/inactive
	private GameObject box,IA,power_up;
	private Light lightning; // Light to simulate a lightning
	private Texture power_up_chosen;
	private Loop_PowerUp powerUpLooping;
	private KartController kart;
	private List<GameObject> ObstaclesListRunTime;


	
	void Start()
	{

		kart = this.GetComponent<KartController> ();
		allKarts = manager.allCars;
		lightning = GameObject.FindGameObjectWithTag ("Lightning").GetComponent<Light> ();
		ObstaclesListRunTime = ObstaclesList.GetComponent<ObstaclesList> ().obstacles;
				
	}
	
	void OnTriggerEnter(Collider other)
	{
		//If the kart hits a power up...
		if (other.tag == "BoxPowerUp"){ //&& this.transform.GetChild(1).tag != "PowerUpDetector") {
			box = other.gameObject;
			//Assign a random power up.
			//The Turbo speed is not included: if it was wanted, the line would be:
			//n_of_powerup = Random.Range(0,powerups.Count + 2);
				
			//n_of_powerup = 0;
			//TODO //DESCOMENTAR LA LINEA DE ABAJOif (ultimo) 

			if (primero)
			{
				n_of_powerup = Random.Range(0,powerups.Count -1);
			}
			else
				n_of_powerup = Random.Range(0,powerups.Count + 1);
		//	n_of_powerup = Random.Range(0,powerups.Count + 1);
			StartCoroutine(launchPowerUpAutomatically());

			
			Hide();
			Invoke("Show", 5.0f);
		}
	}
	
	void Hide()
	{
		box.SetActive(false);
	}
	
	void Show()
	{
		box.SetActive(true);
	}
	

	
	IEnumerator launchPowerUpAutomatically()
	{
		yield return new WaitForSeconds(4f);
		
		switch (n_of_powerup)
		{
		case 0: //Normal turbo power up
			//kart.SpeedBoost();

			break;
			
		case 1:
			//Bomb power up
			IA = this.gameObject;

			power_up = (GameObject)Instantiate(powerups[0],IA.transform.position +new Vector3(IA.transform.forward.x * (-1),0.7f, IA.transform.forward.z * (-5)),Quaternion.identity);
			power_up.SetActive(true);
			ObstaclesListRunTime.Add(power_up);

			break;
		case 2:
			
			if (ultimo) 
			{
				//Lightning power up
				for (int i = 0; i < allKarts.Length; i++) {
					if (allKarts[i].name != this.name) {
						allKarts[i].SpeedPenalty();
						allKarts[i].Spin(2.0f);
						//This script active the light to simulate a lightning
						lightning.enabled = true;
						//Countdown to deactivate the light
						StartCoroutine(Lightning());

					}
				}
				break;
			}
			else
			{
				//Oil slick power up
				IA = this.gameObject;
				
				power_up = (GameObject)Instantiate(powerups[1],IA.transform.position +  new Vector3(IA.transform.forward.x * (-1),0.7f, IA.transform.forward.z * (-5)),Quaternion.identity);
				power_up.SetActive(true);
				ObstaclesListRunTime.Add(power_up);

				break;
			}
		case 3: //Rocket straight
			IA = this.gameObject;
			
			power_up = (GameObject)Instantiate(powerups[2],IA.transform.position +  new Vector3(0.5f ,0.7f, IA.transform.forward.z),IA.transform.rotation);
			power_up.SetActive(true);
			break;
		case 4: //Rocket straight
			IA = this.gameObject;
			
			power_up = (GameObject)Instantiate(powerups[3],IA.transform.position +  new Vector3(0.5f ,0.7f, IA.transform.forward.z),IA.transform.rotation);
			power_up.transform.parent = IA.transform;
			power_up.SetActive(true);
			break;
		
		}

		
	}

	IEnumerator Lightning()
	{
		yield return new WaitForSeconds (3f);
		lightning.enabled = false;
	}

}
