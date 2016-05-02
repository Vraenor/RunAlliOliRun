using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class MyPowerUpNewMode : MonoBehaviour {
	
	public ManagerScriptNewMode manager;
	public List <AudioClip> itemEffectsSounds;
	
	private bool eraUltimo = false;
	private bool eraPrimero = false;
	private KartControllerNewMode[] allKarts;
	private AudioSource effects;
	private bool carrera_acabada = false;
	private int n_of_powerup = -1;
	private Image loopingPowerUpImage;
	private Transform locationPowerUp;
	public bool ultimo = false; // Bool to pick a lightning power up
	public bool primero = false;
	private GameObject box,player,power_up;
	private Light lightning;
	private bool power_up_visible = false;
	public List<Texture> power_ups_sprites;
	private Texture power_up_chosen;
	private Loop_PowerUpNewMode powerUpLooping;
	KartControllerNewMode kart;
	private bool posible_lanzar = false;
	public List<GameObject> powerups;
	public GameObject ObstaclesList;
	private List<GameObject> ObstaclesListRunTime;
	
	void Start()
	{
		kart = this.GetComponent<KartControllerNewMode> ();
		allKarts = manager.allCars;
		effects = this.GetComponent<AudioSource> ();
		powerUpLooping = GameObject.FindGameObjectWithTag ("PowerUpLoop").GetComponent<Loop_PowerUpNewMode> ();
		loopingPowerUpImage = GameObject.FindGameObjectWithTag ("PowerUpLoop").GetComponent<Image> ();
		locationPowerUp = GameObject.FindGameObjectWithTag ("PowerUpLoop").GetComponent<Transform> ();
		loopingPowerUpImage.enabled = false;
		powerUpLooping.enabled = false;
		lightning = GameObject.FindGameObjectWithTag ("Lightning").GetComponent<Light> ();
		player = GameObject.FindGameObjectWithTag("Player");
		ObstaclesListRunTime = ObstaclesList.GetComponent<ObstaclesListNewMode> ().obstacles;
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "BoxPowerUp") {
			//To assure that you don't pick two power ups in a row
			
			if (!power_up_visible) {
				if (!carrera_acabada) {
					effects.clip = itemEffectsSounds[0]; // Must be itemRoullete
					effects.Play();
				}
				
				box = other.gameObject;
				if (ultimo) 
					eraUltimo = true;	
				if (primero)
				{
					n_of_powerup = Random.Range(0,powerups.Count);
				}
				else
					n_of_powerup = Random.Range(0,powerups.Count + 2);
				//n_of_powerup = 5;

				
				StartCoroutine(tiempo_lanzar());
				powerUpLooping.enabled = true;
				loopingPowerUpImage.enabled = true;
				Hide();
				Invoke("Show", 5.0f);
			}
			

			
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
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.J)) 
		{
			power_up = (GameObject) Instantiate(powerups[3],player.transform.position +  new Vector3(0.5f ,0.7f, player.transform.forward.z),player.transform.rotation);
			power_up.transform.parent = this.gameObject.transform;
			power_up.SetActive(true);
		}
		
		if (Input.GetKeyDown(KeyCode.D) && n_of_powerup != -1 && posible_lanzar) {
			
			switch (n_of_powerup) {
			case 0: //Normal turbo powerup
				effects.clip = itemEffectsSounds[2]; // Must be Turbo_sound
				effects.Play();
				kart.SpeedBoost();
				toggleOffPowerUpEffectAndImage();
				
				break;
				
			case 1:
				
				if (eraUltimo) {//Lightning power up
					for (int i = 0; i < allKarts.Length; i++) {
						if (allKarts[i].name != this.name) {
							allKarts[i].SpeedPenalty();
							allKarts[i].Spin(2.0f);
							lightning.enabled = true;
							StartCoroutine(Lightning());
						}
					}
					toggleOffPowerUpEffectAndImage();
					eraUltimo = false;
					
					
				}
				else
				{//Bomb power up
					effects.clip = itemEffectsSounds[1]; //Mut be item_drop
					effects.Play();
					power_up = (GameObject)Instantiate(powerups[0],player.transform.position +new Vector3(player.transform.forward.x * (-1),0.7f, player.transform.forward.z * (-3)),Quaternion.identity);
					power_up.SetActive(true);
					ObstaclesListRunTime.Add(power_up);
					toggleOffPowerUpEffectAndImage();
				}
				
				break;
			case 2:
				//Lightning powerup
				if (eraUltimo)
				{
					for (int i = 0; i < allKarts.Length; i++) {
						if (allKarts[i].name != this.name) {
							allKarts[i].SpeedPenalty();
							allKarts[i].Spin(2.0f);
							lightning.enabled = true;
							StartCoroutine(Lightning());
						}
					}
					toggleOffPowerUpEffectAndImage();
					eraUltimo = false;
					
					
				}
				else
				{
					//Oil slick power up
					effects.clip = itemEffectsSounds[1]; // Must be item_drop
					effects.Play();
					power_up = (GameObject)Instantiate(powerups[1],player.transform.position +  new Vector3(player.transform.forward.x * (-1),0.7f, player.transform.forward.z * (-3)),Quaternion.identity);
					power_up.SetActive(true);
					ObstaclesListRunTime.Add(power_up);
					toggleOffPowerUpEffectAndImage();
				}
				
				
				
				break;
			case 3:
				//Super turbo power up
				effects.clip = itemEffectsSounds[2]; // Debe ser Turbo_sound
				effects.Play();
				kart.TurboSpeedBoost();
				toggleOffPowerUpEffectAndImage();
				break;

			case 4:	//Rocket straight
				power_up = (GameObject) Instantiate(powerups[2],player.transform.position +  new Vector3(0.5f ,0.7f, player.transform.forward.z),player.transform.rotation);
				power_up.SetActive(true);
				//Debug.Break();
				toggleOffPowerUpEffectAndImage();
				break;
			case 5://automatic rocket
				power_up = (GameObject) Instantiate(powerups[3],player.transform.position +  new Vector3(0.5f ,0.7f, player.transform.forward.z),player.transform.rotation);
				power_up.transform.parent = this.gameObject.transform;
				power_up.SetActive(true);

				toggleOffPowerUpEffectAndImage();
				break;
			}
			
			
		}
		
		//Bool to not pick more power ups and to the player be controlled by the IA
		if (player.GetComponent<ExamplePlayerControllerNewMode>().enabled == false) {
			carrera_acabada = true;
		}
		
	}
	
	void toggleOffPowerUpEffectAndImage()
	{
		posible_lanzar = false;
		n_of_powerup = -1;
		power_up_visible = false;
	}
	
	void OnGUI()
	{
		//Draw the power up picked up after the power up looping
		
		if (power_up_visible && !carrera_acabada) {
			if (n_of_powerup == 3) { // Turbo speed power up. needs special location
				//We locate the power up depending on the background powerup image in the UI layer
				float x = locationPowerUp.position.x - loopingPowerUpImage.rectTransform.rect.width / 13.5f;
				float y = locationPowerUp.position.z + loopingPowerUpImage.rectTransform.rect.height / 30f;
				GUI.DrawTexture(new Rect(x, y, power_up_chosen.width/3.5f, power_up_chosen.height/3.75f), power_up_chosen);
			}

			//TODO
			/*else if (n_of_powerup == 4) { //Rocket powerup - Cuando tenga el sprite definitivo lo coloco
				//
			}
			else if (n_of_powerup == 5) { //Automatic Rocket powerup - Cuando tenga el sprite definitivo lo coloco

			*/
			else
			{ 
				float x = locationPowerUp.position.x - loopingPowerUpImage.rectTransform.rect.width / 7f;// Origen: 6f
				float y = locationPowerUp.position.z;
				GUI.DrawTexture(new Rect(x, y, power_up_chosen.width/4f, power_up_chosen.height/4f), power_up_chosen);
			}
		}
		
	}
	
	IEnumerator Lightning()
	{
		yield return new WaitForSeconds (3f);
		lightning.enabled = false;
	}
	
	IEnumerator tiempo_lanzar()
	{
		switch (n_of_powerup) {
		case 0:
			power_up_chosen = (Texture)power_ups_sprites[0];
			break;
		case 1:
			if (eraUltimo) {
				power_up_chosen = (Texture)power_ups_sprites[3];
				
			}
			else
				power_up_chosen = (Texture)power_ups_sprites[1];
			break;
		case 2:
			if (eraUltimo)
			{
				power_up_chosen = (Texture)power_ups_sprites[3];
				
			}
			else
				power_up_chosen = (Texture)power_ups_sprites[2];
			break;
		case 3:
			power_up_chosen = (Texture)power_ups_sprites[4];
			break;
		case 4:
			power_up_chosen = (Texture)power_ups_sprites[5];
			break;
		case 5:
			power_up_chosen = (Texture)power_ups_sprites[6];
			break;
		}
		
		yield return new WaitForSeconds(2.3f);
		
		posible_lanzar = true;
		powerUpLooping.enabled = false;
		loopingPowerUpImage.enabled = false;
		//The power ups images order must be: Green turbo, bomb, oil slick, lightning and red turbo
		
		
		//Set visible to make it visible
		power_up_visible = true;
		
		
	}
}
