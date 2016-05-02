using UnityEngine;
using System.Collections;

public class RocketScriptNewMode : MonoBehaviour
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



	public void OnCollisionEnter(Collision other)
	{
		if (other.rigidbody != null && (other.transform.tag == "Racer" || other.transform.tag == "Pair")) 
		{

			if (other.gameObject.GetComponent<KartControllerNewMode>() != null) 
			{
				other.gameObject.GetComponent<KartControllerNewMode>().SpeedPenalty();
				other.gameObject.GetComponent<KartControllerNewMode>().Spin(2.0f);
			}
			
			else 
			{
				other.gameObject.GetComponent<KartController>().SpeedPenalty();
				other.gameObject.GetComponent<KartController>().Spin(2.0f);
			}
						
			
			Destroy (this.gameObject);
		} 
		else
			Destroy (this.gameObject);


	}


}
