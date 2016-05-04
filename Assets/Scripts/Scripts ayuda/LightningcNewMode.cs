using UnityEngine;
using System.Collections;

public class LightningcNewMode : MonoBehaviour {
	
	
	private float minTime = 0.5f;
	private float thresh = 0.5f;
	private float lastTime = 0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((Time.time - lastTime) > minTime)
			if (Random.value > thresh)
				this.GetComponent<Light>().enabled = true;
		else
			this.GetComponent<Light>().enabled = false;
		lastTime = Time.time;
	}
}
