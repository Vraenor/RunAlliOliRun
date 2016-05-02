using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour 
{

	public float speed = 1.0f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localPosition += transform.forward * speed * Time.deltaTime;
	}

	public void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.GetComponent<KartController>() != null) 
		{

		}


		Destroy (this.gameObject);
	}
}
