using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Loop_PowerUp : MonoBehaviour {

	public List<Sprite> power_ups;
	private int max,i;

	// Use this for initialization
	void Start () {
	
		max = power_ups.Count;
		i = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		//Prehistorical method to loop through an image's script, but effective

		GetComponent<Image>().sprite = power_ups [i];

		i ++;

		if (i == max) {
			i =0;
		}
	}
}
