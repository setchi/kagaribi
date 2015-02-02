using UnityEngine;
using System.Collections;

public class AutoPilotAssistant : AbstractSquareGenerator {
	public AutoPilot autoPilot;
	
	public override GameObject PopTarget(Vector3 pos, Quaternion rot) {
		var square = PopSquare(pos, rot, true, Color.white);
		autoPilot.Enqueue(square);
		return square;
	}
}
