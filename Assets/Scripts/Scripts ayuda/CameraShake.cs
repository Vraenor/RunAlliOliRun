using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	float rotacion;

	void Update()
	{
			//Con este script activamos el tambaleo de la camara al cruzar la meta
			rotacion= Random.Range(-55,55);
			transform.Rotate(rotacion*Time.deltaTime, rotacion*Time.deltaTime, rotacion*Time.deltaTime);
	}
}
