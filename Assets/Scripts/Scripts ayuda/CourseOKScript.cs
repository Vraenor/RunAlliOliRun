using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourseOKScript : MonoBehaviour {

	public GameObject cursor; //Here we had problems detecting by tag, so we put it by a publci variable
	public CourseSelectionC managerSelection;
	private int focus; //Shows the character chosen

	//private List <GameObject> models_character_selection; //The models of the characters

	//NOTE: The order of the lists should
	//be consistent in all the scripts

	void Start()
	{
		//models_character_selection = courseSelection.models_character_selection;
		//managerSelection.enabled = false;
	}


	// Update is called once per frame
	void Update () {
	
		//Definetely move scene
		if (Input.GetKeyDown(KeyCode.A)) {
			StartCoroutine(soundAndChangeScene());
		}

		//Go back
		else if (Input.GetKeyDown(KeyCode.S)) 
		{
			focus = PlayerPrefs.GetInt("courseSelected",focus);
			cursor.SetActive(true);

			//Activate the main gameplay focus of this scene and deactivate the confirmation options
			managerSelection.enabled = true;
			this.gameObject.SetActive(false);
		}

	}


	IEnumerator soundAndChangeScene()
	{
		focus = PlayerPrefs.GetInt("courseSelected",focus);
		this.enabled = false;
		yield return new WaitForSeconds(3);
		Application.LoadLevel (3);

		/*
		print (focus);

		//Level 3 = Loading Screen de Yuk
		if (focus == 1) {
			Application.LoadLevel (3);
		}

		//Level 3 = Loading Screen de Hoarii
		if (focus == 2) {
			Application.LoadLevel (7);
		}
		*/

		
	}

}
