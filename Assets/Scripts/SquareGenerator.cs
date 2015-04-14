using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

public class SquareGenerator : MonoBehaviour {
	public GameObject player;
	public ResultReceiver resultReceiver;
	public PlayerEffect particleController;
	public Subject<GameObject> onPopTarget = new Subject<GameObject>();

	void Awake() {
		SquareContainer.ForEach(square => {
			square.player = player;
			square.resultReceiver = resultReceiver;
			square.Hide();
		});

		StartCoroutine(StartGenerate());
	}
	
	T PickAtRandom<T>(params T[] items) {
		return items[UnityEngine.Random.Range(0, items.Length)];
	}

	Tuple<Color, Color> GenerateRandomColorPair() {
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
		var pattern = new Pattern(this);
		var generateRoutineList = new List<Func<Tuple<Color, Color>, List<IEnumerator>>>();
		
		// Multiple Cross Circle
		generateRoutineList.Add(colorPair => {
			var routines = new List<IEnumerator>();
			var multiplicity = UnityEngine.Random.Range(2, 6);
			var rotateDir = PickAtRandom<int>(1, -1);
			
			routines.Add(pattern.background.Circular(10, 4f, 330, rotateDir, multiplicity, colorPair));
			routines.Add(pattern.background.Circular(10, 4f, 330, rotateDir * -1, multiplicity, colorPair));
			routines.Add(pattern.target.Circular(3f, 7.5f, 5, PickAtRandom<int>(1, -1)));
			routines.Add(particleController.ChangeParticleColorAfterDelay(colorPair.Item1));
			return routines;
		});
		
		// Multiple Circle
		generateRoutineList.Add(colorPair => new List<IEnumerator>() { 
			pattern.background.Circular(10, 4f, 330, PickAtRandom<int>(1, -1), UnityEngine.Random.Range(2, 6), colorPair),
			pattern.target.Circular(3f, 7.5f, 5, PickAtRandom<int>(1, -1)),
			particleController.ChangeParticleColorAfterDelay(colorPair.Item1),
		});
		
		// Random Position
		generateRoutineList.Add(colorPair => new List<IEnumerator>() {
			pattern.background.RandomPositionAndScale(-30, 30, colorPair),
			pattern.target.RandomPosition(3, 1.5f, 0),
			particleController.ChangeParticleColorAfterDelay(colorPair.Item1)
		});
		
		// Multiple Cross Polygon
		generateRoutineList.Add(colorPair => new List<IEnumerator>() {
			pattern.background.Polygon(20, 1.7f, 70, 4, colorPair),
			pattern.target.RandomPosition(5, 3f, 4),
			particleController.ChangeParticleColorAfterDelay(colorPair.Item1)
		});

		var index = 0;
		while (true) {
			var routineWorks = generateRoutineList[index](GenerateRandomColorPair());
			routineWorks.ForEach(routine => StartCoroutine(routine));

			yield return new WaitForSeconds(15f);

			routineWorks.ForEach(routine => StopCoroutine(routine));
			index = ++index % generateRoutineList.Count;
		}
	}
	
	GameObject PopSquare(Vector3 pos, Quaternion rot, bool isTarget, Color color) {
		var square = SquareContainer.GetSquare();
		square.Pop(pos, rot, isTarget, color);
		return square.gameObject;
	}
	
	public GameObject PopBackground(Vector3 pos, Quaternion rot, Color color) {
		return PopSquare(pos, rot, false, color);
	}
	
	public GameObject PopTarget(Vector3 pos, Quaternion rot) {
		var squareObj = PopSquare(pos, rot, true, Color.white);
		onPopTarget.OnNext(squareObj);
		return squareObj;
	}
}
