using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SquareManager : AbstractSquareManager {
	public GameObject player;
	public PatternRoutines patternRoutines;

	void Start() {
		StartCoroutine(StartGenerate());
	}

	Tuple<Color, Color> GenerateColorPair() {
		int color1 = UnityEngine.Random.Range(0, 7);
		int color2;

		do {
			color2 = UnityEngine.Random.Range(0, 8);
		} while (color1 == color2 || (color1 ^ color2) == 7);

		return new Tuple<Color, Color>(
			new Color(color1 >> 2 & 1, color1 >> 1 & 1, color1 & 1),
			new Color(color2 >> 2 & 1, color2 >> 1 & 1, color2 & 1)
		);
	}

	IEnumerator StartGenerate() {
		var index = 0;
		while (true) {
			var colorPair = GenerateColorPair();
			var routineWorks = patternRoutines.Get(index)(colorPair);
			foreach (var routineWork in routineWorks) StartCoroutine(routineWork);

			yield return new WaitForSeconds(15f);

			foreach (var routineWork in routineWorks) StopCoroutine(routineWork);
			index = ++index % patternRoutines.Count;
		}
	}
	
	public override GameObject PopBackground(Vector3 pos, Quaternion rot, Color color) {
		return SquareGenerator.PopSquare(pos, rot, false, color);
	}
	
	public override GameObject PopTarget(Vector3 pos, Quaternion rot) {
		return SquareGenerator.PopSquare(pos, rot, true, Color.white);
	}
}
