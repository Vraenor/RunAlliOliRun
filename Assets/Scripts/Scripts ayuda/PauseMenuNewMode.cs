using UnityEngine;
using System.Collections;

public class PauseMenuNewMode : MonoBehaviour {
	
	public GameObject[] UI;//Manually, didn¡t work the tag reference
	public CameraControllerNewMode playerCamera;
	private MinimapScriptNewMode minimap;
	private GameObject player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		minimap = player.GetComponent<MinimapScriptNewMode> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		//Toggle off the Pause: the toggle on is in the manager script
		if (Input.GetKeyDown(KeyCode.Space)) {
			
			Time.timeScale = 1; //Unpause
			playerCamera.GetComponent<AudioSource> ().Play (); //Resume the background music
			//Set active again the elements of the UI layer
			for (int i = 0; i < UI.Length; i++) {
				UI[i].SetActive(true);
			}
			//Enable the minimap again
			minimap.enabled = true;
			//deactivate the pause menu
			this.gameObject.SetActive(false);
		}
		
		
		
		
		
	}
}
