using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SquareGenerator : AbstractSquareGenerator {
	
	public override GameObject PopTarget(Vector3 pos, Quaternion rot) {
		return PopSquare(pos, rot, true, Color.white);
	}
}
