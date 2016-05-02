using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Simple HUD, just shows a speedometer.
/// </summary>
public class HUDNewMode : MonoBehaviour 
{
	private int playerSelected;
	public List<KartControllerNewMode> vehicles;
	private KartControllerNewMode vehicle;		// the kart we're showing the speedometer for
	public Texture speedometer;			// the speedometer background texture
	public Texture speedoNeedle;		// the speedometer needle texture
	
	
	void Awake()
	{
		//Here we assign the needle to the
		//current player from the list of players
		playerSelected = PlayerPrefs.GetInt ("playerSelected");
		for (int i = 0; i < vehicles.Count; i++) {
			if (i == playerSelected - 1) {
				vehicle = vehicles[i];
				break;
			}
		}
		return;
	}
	
	void OnGUI()
	{
		// draw speedometer graphic in lower right of screen
		float x = Screen.width - 128;
		float y = 0.9f * Screen.height;
		GUI.DrawTexture(new Rect(x - 128, y - 128, 256, 128), speedometer);
		
		// maximum speed the speedometer shows
		float speedoTopSpeed = 50.0f;
		float normalizedSpeed = Mathf.Max(0.0f, vehicle.MPH) / speedoTopSpeed;
		// speedo needle angle goes from -80 to +80 as speed goes from 0 - speedoTopSpeed
		float needleAngle = Mathf.Lerp(-80, 80, normalizedSpeed);
		float needleX = x - 16; // magic numbers for positioning the needle sprite.
		float needleY = y - 8;
		GUIUtility.RotateAroundPivot (needleAngle, new Vector2(needleX, needleY));
		GUI.DrawTexture(new Rect(needleX - 64, needleY - 96, 128, 128), speedoNeedle);
	}
}
