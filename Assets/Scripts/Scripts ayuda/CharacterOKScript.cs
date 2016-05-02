using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterOKScript : MonoBehaviour {

	public GameObject cursor; //Here we had problems detecting by tag, so we put it by a publci variable
	public CharacterSelectionC managerSelection;

	private List <GameObject> models_character_selection; //The models of the characters

	private int focus; //Shows the character chosen

	private Transform contenedorPersonaje;
	private Animator animadorPersonaje;

	//private Transform contenedorTerry;
	//private Animator animadorTerry;

	//NOTE: The order of the lists should
	//be consistent in all the scripts
	//variable para ubicar el modo que hemos seleccionado
	private int modoSeleccionado = 1;
	
	// Use this for initialization
	void Start () 
	{
		//recuperamos el modo seleccionado
		modoSeleccionado = PlayerPrefs.GetInt("modeSelected",modoSeleccionado);
		models_character_selection = managerSelection.models_character_selection;
		focus = PlayerPrefs.GetInt("playerSelected",focus);
		/*
		focus = PlayerPrefs.GetInt("playerSelected",focus);

		//Primero accedemos al cajetin del modelo
		contenedorPersonaje = models_character_selection[focus-1].GetComponentInChildren<Transform>();
		animadorPersonaje = contenedorPersonaje.GetComponentInChildren<Animator>();
		animadorPersonaje.SetBool ("elegido", true);
		//animadorTerry.SetBool("terryElegido", true);
		*/
	}


	// Update is called once per frame
	void Update () 
	{
		focus = PlayerPrefs.GetInt("playerSelected",focus);
		
		//Primero accedemos al cajetin del modelo
		contenedorPersonaje = models_character_selection[focus-1].GetComponentInChildren<Transform>();
		animadorPersonaje = contenedorPersonaje.GetComponentInChildren<Animator>();
		animadorPersonaje.SetBool ("elegido", true);

		//Definetely move scene
		if (Input.GetKeyDown(KeyCode.A)) {
			StartCoroutine(soundAndChangeScene());
		}

		//Go back
		else if (Input.GetKeyDown(KeyCode.S)) {
			focus = PlayerPrefs.GetInt("playerSelected",focus);
			animadorPersonaje.SetBool ("elegido", false);

			//Deseleccionamos personajes
			//anim.SetBool("terryElegido", false);

			/*

			// If the character is not bear...
			if (focus - 1 != 4) {
				//... look in all the childs to Skinned Mesh Renderer (or Mesh Renderer)and put them transparent again
				foreach(Transform child in models_character_selection[focus-1].GetComponent<Transform>())
				{
						if (child.GetComponent<SkinnedMeshRenderer>() != null) {
							SkinnedMeshRenderer materialsObject = child.GetComponent<SkinnedMeshRenderer> ();
						//Loop to change the alpha of all materials.
						//Should use the Transparent/diffuse shader to be effective
							for (int i = 0; i < materialsObject.materials.Length; i++) {
								Color alpha = materialsObject.materials [i].color;
								alpha.a = 0.56f;
								materialsObject.materials [i].color = alpha;
							}
						}
						
						else
						{
						//There are characters that have Mesh Renderer instead of Skinned Mesh:
						//here we cover both cases
							MeshRenderer materialsObjectMesh = child.GetComponent<MeshRenderer> ();
							for (int i = 0; i < materialsObjectMesh.materials.Length; i++) {
								Color alpha = materialsObjectMesh.materials [i].color;
								alpha.a = 0.56f;
								materialsObjectMesh.materials [i].color = alpha;
							}
						}
				}
			}
			//This is bear: the current gameobject has all the materials, so
			// we have to act differently with this character
			else {
				for (int j = 0; j < models_character_selection[focus-1].GetComponent<MeshRenderer>().materials.Length; j++) {
					Color alpha = models_character_selection[focus-1].GetComponent<MeshRenderer>().materials[j].color;
					alpha.a = 0.56f;
					models_character_selection[focus-1].GetComponent<MeshRenderer>().materials[j].color = alpha;
				}
			}
			*/

			cursor.SetActive(true);

			//Activate the main gameplay focus of this scene and deactivate the confirmation options
			managerSelection.enabled = true;
			this.gameObject.SetActive(false);
		}

	}


	IEnumerator soundAndChangeScene()
	{
		focus = PlayerPrefs.GetInt("playerSelected",focus);
		/*
		//Here is the same loop that in the Update(), but now we make the
		//characters non-transparents

		for (int i = 0; i < models_character_selection.Count; i++) {
			if (i != 4) { //No es Bear
				foreach(Transform child in models_character_selection[i].GetComponent<Transform>())
				{
					if (child.GetComponent<SkinnedMeshRenderer>() != null) {
						SkinnedMeshRenderer materialsObject = child.GetComponent<SkinnedMeshRenderer> ();
						for (int j = 0; j < materialsObject.materials.Length; j++) {
							Color alpha = materialsObject.materials [j].color;
							alpha.a = 1.0f; //In this line is the difference
							materialsObject.materials [j].color = alpha;
						}
					}
					
					else
					{
						MeshRenderer materialsObjectMesh = child.GetComponent<MeshRenderer> ();
						for (int j = 0; j < materialsObjectMesh.materials.Length; j++) {
							Color alpha = materialsObjectMesh.materials [j].color;
							alpha.a = 1.0f;
							materialsObjectMesh.materials [j].color = alpha;
						}
					}
				}
			}
			else
			{
				for (int k = 0; k < models_character_selection[i].GetComponent<MeshRenderer>().materials.Length; k++) {
					models_character_selection[i].GetComponent<MeshRenderer>().materials[k].shader = Shader.Find("Diffuse");;//.shader = "Diffuse";
					Color alpha = models_character_selection[i].GetComponent<MeshRenderer>().materials[k].color;
					alpha.a = 1.0f;
					models_character_selection[i].GetComponent<MeshRenderer>().materials[k].color = alpha;
				}
			}


		}
*/
		this.enabled = false;
		yield return new WaitForSeconds(1);
		//Level 3 = Loading Screen

		//Cambiaremos de escena dependiendo el modo
		if (modoSeleccionado == 1) {
			Application.LoadLevel (6);
		}

		//Si esta seleccionado modo sombra vamos al seleccion de escena modosombra
		else
		{
			Application.LoadLevel (10);
		}
	}

}
