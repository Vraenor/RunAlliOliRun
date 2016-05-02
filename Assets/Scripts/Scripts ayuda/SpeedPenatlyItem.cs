using UnityEngine;
using System.Collections;


/// Simple speed penalty item that also spins the vehicle around

public class SpeedPenatlyItem : PickupItemBase 
{
	protected override void OnPickupCollected(KartController kart)
	{

		if (kart.gameObject.GetComponent<ExamplePlayerController>() != null) { //Me aseguro de que es el jugador
			kart.gameObject.GetComponent<AudioSource> ().clip =
			kart.gameObject.GetComponent<MyPowerUp> ().itemEffectsSounds [4]; // Este clip debe ser el de bomba que me de Jorge
		}

		kart.SpeedPenalty();
		kart.Spin(2.0f);
		Destroy (this);
	}
}
