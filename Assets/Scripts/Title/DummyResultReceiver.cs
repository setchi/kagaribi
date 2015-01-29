using UnityEngine;
using System.Collections;

public class DummyResultReceiver : ResultReceiver {
	public PlayerEffect playerEffect;

	public override void Correct () {
		playerEffect.Correct();
	}

	public override void Miss () {

	}
}
