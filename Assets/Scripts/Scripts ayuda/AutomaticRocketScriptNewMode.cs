using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutomaticRocketScriptNewMode : MonoBehaviour {

	public ManagerScriptNewMode managerScriptNewMode;
	public ManagerScript managerScript;
	public KartControllerNewMode[] carOrderNewMode;
	private KartController[] carOrder;
	private KartControllerNewMode targetKCNewMode;
	private KartController targetKC;
	public GameObject target;
	private float speed = 25.0f;

	// Use this for initialization
	void Start () 
	{
		managerScriptNewMode = GameObject.FindGameObjectWithTag ("Manager").GetComponent<ManagerScriptNewMode> ();
		managerScript = GameObject.FindGameObjectWithTag ("Manager").GetComponent<ManagerScript> ();
		if (managerScriptNewMode == null)
		{

			carOrder = managerScript.carOrder;
			for (int i = 0; i < carOrder.Length; i++)
			{
				if (carOrder[i].gameObject.name == this.transform.parent.name) 
				{
					if (i == 0) 
					{
						targetKC = carOrder[carOrder.Length];
						target = targetKC.gameObject;
					}
					else 
					{
						targetKC = carOrder[i-1];
						target = targetKC.gameObject;
					}
					
					break;
				}
			}
		}
		else 
		{
			carOrderNewMode = managerScriptNewMode.carOrder;
			for (int i = 0; i < carOrderNewMode.Length; i++)
			{
				if (carOrderNewMode[i].gameObject.name == this.transform.parent.name) 
				{
					if (i == 0) 
					{
						targetKCNewMode = carOrderNewMode[carOrderNewMode.Length];
						target = targetKCNewMode.gameObject;
					}
					else 
					{
						targetKCNewMode = carOrderNewMode[i-1];
						target = targetKCNewMode.gameObject;
					}
					
					break;
				}
			}
		}
			



	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.LookAt (target.transform.position);
		this.transform.Translate (Vector3.forward * speed * Time.deltaTime);
		//this.transform.position = Vector3.MoveTowards(this.transform.position,target.transform.position,speed * Time.deltaTime);
	}

	public void OnCollisionEnter(Collision other)
	{
		if (other.rigidbody != null &&(other.transform.tag == "Racer" || other.transform.tag == "Pair")) 
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
		
		
	}
}
