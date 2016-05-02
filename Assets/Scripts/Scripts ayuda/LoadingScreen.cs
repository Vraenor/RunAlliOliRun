using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//This script has to be put in the image of the background

public class LoadingScreen : MonoBehaviour {
    /*
	public GameObject Loading;
	public List<Sprite> LoadingScreens;

	private int player ;
	private Image background;

	//Circuito
	private int circuit;

	//variable para ubicar el modo que hemos seleccionado
	private int modoSeleccionado = 1;
	
	// Use this for initialization
	void Start () 
	{
		//recuperamos el modo seleccionado
		modoSeleccionado = PlayerPrefs.GetInt("modeSelected",modoSeleccionado);
	
		background = this.GetComponent<Image> ();
		player = PlayerPrefs.GetInt ("playerSelected", player);
		//Importamos el circuito seleccionado
		circuit = PlayerPrefs.GetInt ("courseSelected", 1);

		//background.sprite = LoadingScreens [player - 1]; // To support more than one loading screen

		//Random of loading screens, first number inclusive, second number exclusive
		background.sprite = LoadingScreens [Random.Range(0, 10)];
		StartCoroutine (ChangeToRace ());
	}

	IEnumerator ChangeToRace()
	{
		yield return new WaitForSeconds(1f);
		yield return new WaitForSeconds(7f);
		this.GetComponent<FadeMaterials> ().FadeOut ();
		Loading.GetComponent<FadeMaterials> ().FadeOut ();
		yield return new WaitForSeconds(1f);

		print (circuit);

		//Cambiaremos de pantalla dependiendo del modo seleccionado, en este caso el modo normal
		if (modoSeleccionado == 1) {
			if (circuit == 1) {
				Application.LoadLevel (4);
			} else if (circuit == 2) {
				Application.LoadLevel (7);
			} else if (circuit == 3) {
				Application.LoadLevel (8);
			}
		} 
		//Cambiaremos de pantalla de dependiendo del modo seleccionado, en este caso el modo sombra
		else 
		{
			if (circuit == 1) 
			{
				Application.LoadLevel (11);
			}
		}
	}*/
}
