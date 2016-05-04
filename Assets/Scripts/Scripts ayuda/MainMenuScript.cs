using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
    /*
	public GameObject cursor;
	public GameObject StartObject;
	public GameObject courseModeObject;
	public GameObject shadowModeObject;
	public GameObject ExitObject;
	public GameObject background;
	public GameObject music;
	public GameObject controlsTitle;
	public GameObject controls;

	//Posicion de control del cursor del jugador
	private int cursorPosition = 1;
	
	//Despliega el el boton de seleccion de modo
	private bool SeleccionDeModo = false;

	//Distingue de si jugamos al modo normal o al modo sombra
	private bool modoSombraActivado = false;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {

		//Comproamos si el jugador ha activado la seleccion de modo
		if (SeleccionDeModo == false) 
		{
			//Al pulsar arriba sin el menu de modo activado siempre es start game
			if (Input.GetKeyDown (KeyCode.UpArrow)) 
			{
				cursorPosition = 1;
			}

			//Al pulsar abajo sin el menu de modo activado siempre es exit game
			else if (Input.GetKeyDown (KeyCode.DownArrow)) {
				cursorPosition = 2;
			}

			//Controlamos si el pulsar A lo hace sobre start game o exit game
			else if (Input.GetKeyDown (KeyCode.A)) 
			{
				if(cursorPosition == 1)
				{
					//En el caso de pulsar A sobre start game activamos el seleccion de modo
					SeleccionDeModo = true;
					//movemos el curso sobre la primera opcion
					cursorPosition = 3;
					//Ponemos visible los modos ocultos
					courseModeObject.transform.localScale = new Vector3(3, 1, 1);
					shadowModeObject.transform.localScale = new Vector3(3, 1, 1);
				}
				else
				{
					//En el caso de que se pulse sobre exit game, salimos
					Application.Quit();
				}

			}

		}
		//esta variable activada indica que trabajamos sobre los botones de elegir modo
		else if (SeleccionDeModo == true) 
		{
			//Al pulsar "S" volvemos al menu original y ponemos invisible los botones de modo
			if (Input.GetKeyDown (KeyCode.S)) 
			{
				SeleccionDeModo = false;
				cursorPosition = 1;
				courseModeObject.transform.localScale = new Vector3 (0, 0, 0);
				shadowModeObject.transform.localScale = new Vector3 (0, 0, 0);
			}

			//Pulsar arriba siempre cambiamos al modo sombras
			else if (Input.GetKeyDown (KeyCode.UpArrow)) 
			{
				cursorPosition = 4;
			}

			//Al pulsar abajo siempre cambiamos al modo normal
			else if (Input.GetKeyDown (KeyCode.DownArrow)) 
			{
				cursorPosition = 3;
			}

			//Si el jugador vuelve a pulsar A cambiamos a seleccion de personaje
			else if (Input.GetKeyDown (KeyCode.A))
			{
				StartCoroutine(changeScene());
			}
		}

		//Las siguientes instrucciones mueven el cursor por la pantalla
		if (cursorPosition == 1) 
		{
			cursor.transform.position = StartObject.transform.position;
		}
		
		else if (cursorPosition == 2) {
			cursor.transform.position = ExitObject.transform.position;
		}

	
		else if (cursorPosition == 3)
		{
			cursor.transform.position = courseModeObject.transform.position;
			//Esta variable es utilizada para distinguir el modo de juego
			modoSombraActivado = false;
		}


		else if (cursorPosition == 4)
		{
			cursor.transform.position = shadowModeObject.transform.position;	
			//Esta variable es utilizada para distinguir el modo de juego
			modoSombraActivado = true;
		}



	}

	IEnumerator changeScene()
	{
		cursor.SetActive (false);
		cursor.GetComponent<FadeMaterials> ().FadeOut ();
		StartObject.GetComponent<FadeMaterials> ().FadeOut ();
		ExitObject.GetComponent<FadeMaterials> ().FadeOut ();
		background.GetComponent<FadeMaterials> ().FadeOut ();
		controls.GetComponent<FadeMaterials> ().FadeOut ();
		controlsTitle.GetComponent<FadeMaterials> ().FadeOut ();
		courseModeObject.GetComponent<FadeMaterials> ().FadeOut ();
		shadowModeObject.GetComponent<FadeMaterials> ().FadeOut ();
		yield return new WaitForSeconds (1f);

		//Dependiendo del modo de juego activado cambiaremos a una escena o otra
		if (modoSombraActivado == true) 
		{
			//Guardamos el modo elegido por el jugador, 2 = modo sombra 
			PlayerPrefs.SetInt("modeSelected",2);

			print(PlayerPrefs.GetInt ("modeSelected", 2));
			//Level 9 = CharacterSelection ShadowMode
			Application.LoadLevel (9);
		}

		else
		{
			//Guardamos el modo elegido por el jugador, 1 = modo normal
			PlayerPrefs.SetInt("modeSelected",1);
			//Level 2 = CharacterSelection Normal
			print(PlayerPrefs.GetInt ("modeSelected", 1));

			Application.LoadLevel (2);
		}

		//Dont destroy the background music
		DontDestroyOnLoad (music);
	}*/
}
