using UnityEngine;
using System.Collections;


public class ChangeMaterialColorNewMode : MonoBehaviour 
{
	public Material colorInicial;
	
	float r = 0.0f;
	float g = 0.0f;
	float b = 0.0f;
	float a = 0.0f;
	
	void Start()
	{
		gameObject.renderer.material = colorInicial;
	}
	
	void Update()
	{
		//Intento de hacer el color de las paredes progresivas
		if (b >= 2.0f) 
		{
			b = Random.value;
			g = 0.0f;
			r = 0.0f;
		}
		
		else if (g >= 2.0f) 
		{
			//g = 0.0f;
			b += 0.01f;
		}
		else if (r >= 2.0f) 
		{
			//b = 0.0f;
			g += 0.01f;
		} 
		
		else 
		{
			r += 0.01f;
		}
		
		
		colorInicial.color = new Color (r,g,b);
		
	}
}


