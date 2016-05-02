using UnityEngine;
using System.Collections;


/// Simple speed penalty item that also spins the vehicle around

public class SpeedPenatlyItemNewMode : PickupItemBase
{
	protected override void OnPickupCollected(KartController kart)
	{
		Debug.Log ("Entra aqui");
		if (kart.gameObject.GetComponent<ExamplePlayerControllerNewMode>() != null) { //Me aseguro de que es el jugador
			kart.gameObject.GetComponent<AudioSource> ().clip =
				kart.gameObject.GetComponent<MyPowerUpNewMode> ().itemEffectsSounds [4]; // Este clip debe ser el de bomba que me de Jorge
		}
		
		kart.SpeedPenalty();
		kart.Spin(2.0f);
		Destroy (this);
	}
}
