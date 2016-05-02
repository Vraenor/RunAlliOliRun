using UnityEngine;
using System.Collections;


public class OilSlickItemNewMode : PickupItemBase 
{/*
	protected override void OnPickupCollected(KartController kart)
	{
		//In the Player's AudioSource put the effect sounds
		
		if (kart.gameObject.GetComponent<ExamplePlayerControllerNewMode> () != null) { //Make sure that is the player's case
			kart.gameObject.GetComponent<AudioSource> ().clip =
				kart.gameObject.GetComponent<MyPowerUpNewMode> ().itemEffectsSounds [3]; // Must be PlayersSlip
			kart.gameObject.GetComponent<AudioSource> ().Play ();
		}
		
		kart.SpeedPenalty(0.25f, 1.0f, 1.0f);
		kart.Wiggle(1.0f);
		Destroy (this);
	}*/
}
