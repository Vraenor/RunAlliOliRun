using UnityEngine;
using System.Collections;


public abstract class PickupItemBaseNewMode : MonoBehaviour 
{
	protected abstract void OnPickupCollected(KartControllerNewMode kart);
	
	void OnTriggerEnter(Collider other)
	{
		// we need to look on the attachedRigidbody for the KartController, because the colliders are attached to a child
		// object.
		KartControllerNewMode kart = other.attachedRigidbody.GetComponent<KartControllerNewMode>();
		if(kart != null)
			OnPickupCollected(kart);
		
		// hide this item for a few seconds.
		/*
		if (other.tag != "PowerUpDetector") 
		{
			Hide();
			Invoke("Show", 5.0f);
		}
		*/

	}
	
	void Hide()
	{
		gameObject.SetActive(false);
	}
	
	void Show()
	{
		gameObject.SetActive(true);
	}
	
	void Update()
	{
		// always face the camera
		Camera cam = Camera.main;
		transform.LookAt(transform.position + cam.transform.rotation * Vector3.back, cam.transform.rotation * Vector3.up);
	}
}
