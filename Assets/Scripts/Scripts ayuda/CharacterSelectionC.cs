using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectionC : MonoBehaviour {

	//This script controls the main player input in the character selection screen
	//Can overlap with CharacterOKscript, be careful!

	private int focus = 1; //1 is an arbitrary number to initialize; will be changed later
	private float scalerY = 4.3f; //Numbers to scale the cursor correctly
	private float scalerX = 5f;
	private GameObject cursor;
	private GameObject cartel;

	//Estas listas deben hacer referencia a sus elementos en el mismo orden.
	//De lo contrario, no funcionara como es debido
	//Ejemplo: Si el primer elemento del cartel corresponde a Yuk, 
	//en las demas listas debe ser asi tambien

	public List <AudioSource> confirm_sounds; 
	public GameObject OK; //Must be a reference of the gameobject that contains the CharacterOKscript
	public List <GameObject> sprites_character_selection;
	public List <Sprite> sprites_cartels; 
	public List <GameObject> models_character_selection;

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
		switch (focus) 
		{
		case 1:
			//YUK TO TERRY
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				focus = 2;
				placeCursor();
				changeCartelAndModel();
			}

			//YUK TO DOG
			else if (Input.GetKeyDown(KeyCode.DownArrow) )
			{
			    focus = 3;
				placeCursor();
				changeCartelAndModel();
			}
			
			break;
		case 2:
			//TERRY TO YUK
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				focus = 1;
				placeCursor();
				changeCartelAndModel();
			}
			//TERRY TO HOARII
			else if (Input.GetKeyDown(KeyCode.DownArrow) )
			{
				focus = 4;
				placeCursor();
				changeCartelAndModel();
			}
			break;
		case 3:
			////DOG TO YUK
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				focus = 1;
				placeCursor();
				changeCartelAndModel();
			}
			//DOG TO HOARII
			else if (Input.GetKeyDown(KeyCode.RightArrow) )
			{
				focus = 4;
				placeCursor();
				changeCartelAndModel();
			}
			//DOG TO BEAR
			else if (Input.GetKeyDown(KeyCode.DownArrow) )
			{
				focus = 5;
				placeCursor();
				changeCartelAndModel();
			}
			break;
		case 4:
			//HOARII TO TERRY
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				focus = 2;
				placeCursor();
				changeCartelAndModel();
			}
			//HOARII TO DOG
			else if (Input.GetKeyDown(KeyCode.LeftArrow) )
			{
				focus = 3;
				placeCursor();
				changeCartelAndModel();
			}
			//HOARII TO INKMAN
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				focus = 6;
				placeCursor();
				changeCartelAndModel();
			}
			break;

		case 5:
			//BEAR TO DOG
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				focus = 3;
				placeCursor();
				changeCartelAndModel();
			}
			//BEAR TO INKMAN
			else if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				focus = 6;
				placeCursor();
				changeCartelAndModel();
			}
			//BEAR TO ALIRR
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				focus = 7;
				placeCursor();
				changeCartelAndModel();
			}

			break;

		case 6:
			//INKMAN TO HOARII
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				focus = 4;
				placeCursor();
				changeCartelAndModel();
			}

			//INKMAN TO BEAR
			else if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				focus = 5;
				placeCursor();
				changeCartelAndModel();
			}

			//BEAR TO PIGMAN
			else if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				focus = 8;
				placeCursor();
				changeCartelAndModel();
			}
			
			break;

		case 7:
			//ALIRR TO BEAR
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				focus = 5;
				placeCursor();
				changeCartelAndModel();
			}
			
			//INKMAN TO PIGMAN
			else if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				focus = 8;
				placeCursor();
				changeCartelAndModel();
			}

			break;

		case 8:
			//PIGMAN TO INKMAN
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				focus = 6;
				placeCursor();
				changeCartelAndModel();
			}
			
			//PIGMAN TO ALIRR
			else if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				focus = 7;
				placeCursor();
				changeCartelAndModel();
			}
			break;
		}
	
	}

	void placeCursor()
	{
		cursor.transform.position = sprites_character_selection[focus - 1].transform.position;
		if (modoSeleccionado == 1) {
			cursor.transform.localScale = new Vector3 (sprites_character_selection [focus - 1].transform.localScale.x * scalerX, sprites_character_selection [focus - 1].transform.localScale.y * scalerY);
		} else 
		{
			cursor.transform.localScale = new Vector3 (sprites_character_selection [focus - 1].transform.localScale.x * scalerX*2, sprites_character_selection [focus - 1].transform.localScale.y * scalerY);
		}
	}

	void changeCartelAndModel()
	{
		//Line to change the cartel. Remember, in the same order!
		cartel.GetComponent<SpriteRenderer>().sprite = sprites_cartels[focus - 1];
		//Here we change the transparency of the portraits
		for (int i = 0; i < models_character_selection.Count; i++) {
			if (i == focus-1) {


				models_character_selection[i].SetActive(true);

				Color c = sprites_character_selection[i].GetComponent<SpriteRenderer>().color;
				c.a = 1.0f;
				sprites_character_selection[i].GetComponent<SpriteRenderer>().color = c;
			}
			else
			{
				models_character_selection[i].SetActive(false);
				Color c = sprites_character_selection[i].GetComponent<SpriteRenderer>().color;
				c.a = 0.56f;
				sprites_character_selection[i].GetComponent<SpriteRenderer>().color = c;
			}
		}
	}


	IEnumerator soundAndChangeToOk()
	{
		PlayerPrefs.SetInt("playerSelected",focus);
		/*
		if (focus - 1 != 4) {
			foreach (Transform child in models_character_selection[focus-1].GetComponent<Transform>()) {
				if (child.GetComponent<SkinnedMeshRenderer>() != null) {
					SkinnedMeshRenderer materialsObject = child.GetComponent<SkinnedMeshRenderer> ();
					for (int i = 0; i < materialsObject.materials.Length; i++) {
						Color alpha = materialsObject.materials [i].color;
						alpha.a = 1.0f;
						materialsObject.materials [i].color = alpha;
					}
				}

				else
				{
					MeshRenderer materialsObjectMesh = child.GetComponent<MeshRenderer> ();
					for (int i = 0; i < materialsObjectMesh.materials.Length; i++) {
						Color alpha = materialsObjectMesh.materials [i].color;
						alpha.a = 1.0f;
						materialsObjectMesh.materials [i].color = alpha;
					}
				}

				

			}
		} 
		else {
			for (int j = 0; j < models_character_selection[focus-1].GetComponent<MeshRenderer>().materials.Length; j++) {
				Color alpha = models_character_selection[focus-1].GetComponent<MeshRenderer>().materials[j].color;
				alpha.a = 1.0f;
				models_character_selection[focus-1].GetComponent<MeshRenderer>().materials[j].color = alpha;
			}
		}

*/
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
