using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourseSelectionC : MonoBehaviour {

	//This script controls the main player input in the character selection screen
	//Can overlap with CharacterOKscript, be careful!

	private int focus = 1; //1 is an arbitrary number to initialize; will be changed later
	private float scalerY = 1; //Numbers to scale the cursor correctly
	private float scalerX = 1;
	private GameObject cursor;
	private GameObject cartel;

	//Estas listas deben hacer referencia a sus elementos en el mismo orden.
	//De lo contrario, no funcionara como es debido
	//Ejemplo: Si el primer elemento del cartel corresponde a Yuk, 
	//en las demas listas debe ser asi tambien

	public List <AudioSource> confirm_sounds; 
	public GameObject OK; //Must be a reference of the gameobject that contains the CharacterOKscript
	public List <GameObject> sprites_course_selection;
	//public List <GameObject> models_course_selection;
	public List <Sprite> sprites_carteles; 

	//variable para ubicar el modo que hemos seleccionado
	private int modoSeleccionado = 1;

	// Use this for initialization
	void Start () 
	{
		//recuperamos el modo seleccionado
		modoSeleccionado = PlayerPrefs.GetInt("modeSelected",modoSeleccionado);
		cursor = GameObject.FindGameObjectWithTag ("Cursor");
		cartel = GameObject.FindGameObjectWithTag ("Cartel");
			
	}

	// Update is called once per frame
	void Update () {

		//Give the protagonism to the CharacterOKscript
		if (Input.GetKeyDown(KeyCode.A)) {

			StartCoroutine (soundAndChangeToOk());
		
		}

		//By changing the focus, we can control where can go the cursor and
		//what character can be selected by pressing the A button

		//Filtramos circuito por modo
		if (modoSeleccionado == 1) 
		{
			switch (focus) {
			case 1:

				if (Input.GetKeyDown (KeyCode.DownArrow)) {
					focus = 2;
					placeCursor ();
					changeModel ();
				}
				break;
			case 2:
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					focus = 1;
					placeCursor ();
					changeModel ();
				} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
					focus = 3;
					placeCursor ();
					changeModel ();
				}
				break;

			case 3:
				if (Input.GetKeyDown (KeyCode.UpArrow)) {
					focus = 2;
					placeCursor ();
					changeModel ();
				}
				break;
			}
		}
	
	}

	void placeCursor()
	{

		cursor.transform.position = sprites_course_selection[focus - 1].transform.position;
		cursor.transform.localScale = new Vector3(sprites_course_selection[focus - 1].transform.localScale.x * scalerX,sprites_course_selection[focus - 1].transform.localScale.y * scalerY);
	}


	void changeModel()
	{
		//Line to change the cartel. Remember, in the same order!
		cartel.GetComponent<SpriteRenderer>().sprite = sprites_carteles[focus - 1];

		//Here we change the transparency of course the portraits
		for (int i = 0; i < sprites_course_selection.Count; i++) {
			if (i == focus-1) 
			{
				//Esto por si queremos que salga el minimodelo en version 3D
				//models_course_selection[i].SetActive(true);

				Color c = sprites_course_selection[i].GetComponent<SpriteRenderer>().color;
				c.a = 1.0f;
				sprites_course_selection[i].GetComponent<SpriteRenderer>().color = c;
			}
			else
			{
				//Esto por si queremos que salga el minimodelo en version 3D
				//models_course_selection[i].SetActive(false);

				Color c = sprites_course_selection[i].GetComponent<SpriteRenderer>().color;
				c.a = 0.56f;
				sprites_course_selection[i].GetComponent<SpriteRenderer>().color = c;
			}
		}
	}
	

	IEnumerator soundAndChangeToOk()
	{

		PlayerPrefs.SetInt("courseSelected",focus);
		playSounds();
		cursor.SetActive (false);
		OK.SetActive (true);
		this.enabled = false;
		yield return new WaitForSeconds(0.01f); //A little time to let the sounds sound

	}

	void playSounds()
	{
		for (int i = 0; i < confirm_sounds.Count; i++) {
			if (i ==focus - 1) {
				confirm_sounds[i].Play();
				break;
				
			}
	}

	}




}
