using UnityEngine;
using System.Collections;


/// Simple speed boost pickup item.

public class SpeedBoostItemNewMode : PickupItemBase 
{
	protected override void OnPickupCollected(KartController kart)
	{
		kart.SpeedBoost();
		Destroy (this);
	}
}
