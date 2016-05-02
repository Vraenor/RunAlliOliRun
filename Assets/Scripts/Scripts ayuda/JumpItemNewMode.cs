using UnityEngine;
using System.Collections;


// Jump pickup.
// Makes the kart do a little jump

public class JumpItemNewMode : PickupItemBase 
{
	protected override void OnPickupCollected(KartController kart)
	{
		kart.Jump(1.0f);
	}
}
