using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// Simple speed boost pickup item.

public class DisplayChangingGravity : MonoBehaviour 
{		
	private bool inverseGravityDisplayed = false;
	private bool normalGravityDisplayed = false;

	//Controlamos el texto de cambiar la gravedad
	public Text ChangeGravityText;

	//Cargamos los datos del corredor humano y de la IA
	private KartController kart;
	private ExampleAIController IAController;

	//Creamos una variable que almacene el script de la camara que activa el cambio gravitatorio
	private GravityChanges gravityController;

	//Creamos una variable que almacene el script de la camara que activa el tambaleo de la camara previo al cambio de graveda
	private CameraShake cameraShakeController;

	private GameObject[] bloquesFinales;
	
	void Start () 
	{
		// keep a reference to the kart component
		kart = GetComponent<KartController>();

		//Obtenemos el script de gravedad de la camera y el del tambaleo, que actualmente se encuentran desactivados
		gravityController = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GravityChanges> ();
		cameraShakeController = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShake> ();

		//Hace aparecer a los bloques finales del circuito
		bloquesFinales = GameObject.FindGameObjectsWithTag ("Trees");

	}
	
	void Update () 
	{
		//En la vuelta 2 del jugador humano activamos el cambio de camara
		if ((kart.currentLap == 2) && (!inverseGravityDisplayed))
		{
			StartCoroutine(enableGravity());	
			inverseGravityDisplayed = true;
		} 

		//En la vuelta 3 del jugador humano volvemos la camara a su posicion original
		if ((kart.currentLap == 3) && (!normalGravityDisplayed))
		{
			StartCoroutine(disableGravity());	
			normalGravityDisplayed = true;
		} 
	}

		IEnumerator enableGravity()
		{
			//Mostramos el texto de cambio de gravedad
			ChangeGravityText.text = "Changing Gravity!";
			ChangeGravityText.enabled = true;
			
			//Tambaleamos la camara durante unos segundos para poner en situacion al jugador
			cameraShakeController.enabled = true;

			yield return new WaitForSeconds(2.0f);
			
			//Desactivamos tambaleo de la camara
			cameraShakeController.enabled = false;

			//Activamos el cambio de gravedad
			gravityController.enabled = true;
			ChangeGravityText.enabled = false;
			
		}

		IEnumerator disableGravity()
		{
			//Mostramos el texto de cambio de gravedad
			ChangeGravityText.text = "Changing Gravity!";
			ChangeGravityText.enabled = true;			

			//Tambaleamos la camara durante unos segundos para poner en situacion al jugador
			cameraShakeController.enabled = true;

			//Con esta sentencia activamos los bloques de final de nivel
			for (int i = 0; i < bloquesFinales.Length; i++) 
			{	//Hacemos los bloques visibles	
				bloquesFinales[i].renderer.enabled = true;
				//Metemos gravedad a los bloques
				bloquesFinales[i].rigidbody.useGravity = true;
			}

			yield return new WaitForSeconds(1.0f);
			//Desactivamos tambaleo de la camara
			cameraShakeController.enabled = false;
			
			//Reiniciamos manualmente la camara para que no se fastidie el invento
			gravityController.camera.ResetProjectionMatrix();
			
			//Desactivamos el cambio de gravedad
			gravityController.enabled = false;
			
			ChangeGravityText.enabled = false;
		}
}
