using UnityEngine;
using System.Collections;

public class GameManagerScriptNewMode : MonoBehaviour {
	
	private int photoNumber = 0;
	
	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad (this);
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		if (Input.GetKeyDown(KeyCode.P)) {
			
			TakeScreenshot(photoNumber);
			photoNumber ++;
		}
		
	}
	
	void TakeScreenshot(int photoNumber)
	{
		//Debug.Log (Application.persistentDataPath);
		Application.CaptureScreenshot (Application.persistentDataPath + "/" + Application.loadedLevelName + photoNumber + ".png");
		//Application.CaptureScreenshot ("Assets/Screenshots/" + Application.loadedLevelName + photoNumber + ".png");
	}
}
