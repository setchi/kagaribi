using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractSquareGenerator : MonoBehaviour {
	public GameObject player;
	public GameObject squarePrefab;
	public ResultReceiver resultReceiver;
	public PlayerEffect particleController;
	
	static int maxSquare = 700;
	static int currentIndex = 0;
	static Square[] squares = new Square[maxSquare];
	
	void Awake() {
		SetupSquares();
	}
	
	void SetupSquares() {
		for (var i = 0; i < maxSquare; i++) {
			var square = (GameObject)Instantiate(squarePrefab, Vector3.zero, Quaternion.identity);
			square.transform.SetParent(gameObject.transform);
			squares[i] = square.GetComponent<Square>();
			squares[i].resultReceiver = resultReceiver;
			squares[i].player = player;
		}
	}
	
	T PickAtRandom<T>(params T[] items) {
		return items[UnityEngine.Random.Range(0, items.Length)];
	}

	protected Tuple<Color, Color> GenerateColorPair() {
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
	
	protected IEnumerator StartGenerate() {
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
			routines.Add(particleController.ChangeParticleColorAfter5sec(colorPair.Item1));
			return routines;
		});
		
		// Multiple Circle
		generateRoutineList.Add(colorPair => new List<IEnumerator>() { 
			pattern.background.Circular(10, 4f, 330, PickAtRandom<int>(1, -1), UnityEngine.Random.Range(2, 6), colorPair),
			pattern.target.Circular(3f, 7.5f, 5, PickAtRandom<int>(1, -1)),
			particleController.ChangeParticleColorAfter5sec(colorPair.Item1),
		});
		
		// Random Position
		generateRoutineList.Add(colorPair => new List<IEnumerator>() {
			pattern.background.RandomPositionAndScale(-30, 30, colorPair),
			pattern.target.RandomPosition(3, 1.5f, 0),
			particleController.ChangeParticleColorAfter5sec(colorPair.Item1)
		});
		
		// Multiple Cross Polygon
		generateRoutineList.Add(colorPair => new List<IEnumerator>() { /**
				pattern.Background.Polygon(15, 1.5f, 70, 3, colorPair),
				pattern.Target.RandomPosition(3, 1.5f, 0),
				ChangeParticleColor(colorPair.Item1)
			
			// Cross Square
			}, colorPair => new List<IEnumerator>() { /**/
			pattern.background.Polygon(20, 1.7f, 70, 4, colorPair),
			pattern.target.RandomPosition(5, 3f, 4),
			particleController.ChangeParticleColorAfter5sec(colorPair.Item1)
		});

		var index = 0;
		while (true) {
			var colorPair = GenerateColorPair();
			var routineWorks = generateRoutineList[index](colorPair);
			foreach (var routineWork in routineWorks) StartCoroutine(routineWork);
			
			yield return new WaitForSeconds(15f);
			
			foreach (var routineWork in routineWorks) StopCoroutine(routineWork);
			index = ++index % generateRoutineList.Count;
		}
	}
	
	public GameObject PopBackground(Vector3 pos, Quaternion rot, Color color) {
		return PopSquare(pos, rot, false, color);
	}

	public abstract GameObject PopTarget(Vector3 pos, Quaternion rot);
	
	
	public static GameObject PopSquare(Vector3 pos, Quaternion rot, bool isTarget, Color color) {
		squares[currentIndex].Pop(pos, rot, isTarget, color);
		var square = squares[currentIndex].gameObject;
		currentIndex = ++currentIndex % maxSquare;
		return square;
	}
}
