using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SquareGenerator : MonoBehaviour {
	public GameObject squarePrefab;
	public GameController gameController;
	public PlayerController playerController;

	static int maxSquare = 3000;
	static int currentIndex = 0;
	static Square[] squares = new Square[maxSquare];

	void Awake() {
		for (var i = 0; i < maxSquare; i++) {
			var square = (GameObject)Instantiate(squarePrefab, Vector3.zero, Quaternion.identity);
			squares[i] = square.GetComponent<Square>();
			squares[i].gameController = gameController;
		}
	}

	void Start () {
		StartCoroutine(PatternChanger());
	}

	T PickAtRandom<T>(params T[] items) {
		return items[UnityEngine.Random.Range(0, items.Length)];
	}

	IEnumerator ChangeParticleColor(Color color) {
		yield return new WaitForSeconds(5f * (1 / Time.timeScale));
		playerController.SetParticleColor(Color.white, color);
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

	IEnumerator PatternChanger() {
		var PatternRoutineList = new List<Func<Tuple<Color, Color>, List<IEnumerator>>>() {

			// Multiple Cross Circle
			colorPair => {
				var routines = new List<IEnumerator>();
				var multiplicity = UnityEngine.Random.Range(2, 5);
				var rotateDir = PickAtRandom<int>(1, -1);
				
				routines.Add(Pattern.Background.Circular(10, 4.5f, 80, rotateDir, multiplicity, colorPair));
				routines.Add(Pattern.Background.Circular(10, 4.5f, 80, rotateDir * -1, multiplicity, colorPair));
				routines.Add(Pattern.Target.Circular(3f, 7.5f, 5, PickAtRandom<int>(1, -1)));
				routines.Add(ChangeParticleColor(colorPair.Item1));
				return routines;
			
			// Multiple Circle
			}, colorPair => new List<IEnumerator>() { 
				Pattern.Background.Circular(10, 4.5f, 80, PickAtRandom<int>(1, -1), UnityEngine.Random.Range(2, 6), colorPair),
				Pattern.Target.Circular(3f, 7.5f, 5, PickAtRandom<int>(1, -1)),
				ChangeParticleColor(colorPair.Item1),

			// Random Position
			}, colorPair => new List<IEnumerator>() {
				Pattern.Background.RandomPositionAndScale(-30, 30, colorPair),
				Pattern.Target.RandomPosition(3, 1.5f, 0),
				ChangeParticleColor(colorPair.Item1)
			
			// Multiple Cross Polygon
			}, colorPair => new List<IEnumerator>() { /**
				Pattern.Background.Polygon(15, 1.5f, 70, 3, colorPair),
				Pattern.Target.RandomPosition(3, 1.5f, 0),
				ChangeParticleColor(colorPair.Item1)
			
			// Cross Square
			}, colorPair => new List<IEnumerator>() { /**/
				Pattern.Background.Polygon(20, 1.5f, 70, 4, colorPair),
				Pattern.Target.RandomPosition(5, 3f, 4),
				ChangeParticleColor(colorPair.Item1)
			}
		};

		var index = 0;
		while (true) {
			var colorPair = GenerateColorPair();
			var routineWorks = PatternRoutineList[index](colorPair);
			foreach (var routine in routineWorks) StartCoroutine(routine);

			yield return new WaitForSeconds(15f);

			foreach (var routine in routineWorks) StopCoroutine(routine);
			index = ++index % PatternRoutineList.Count;
		}
	}

	public static GameObject PopBackground(Vector3 pos, Quaternion rot, Color color) {
		return PopSquare(pos, rot, false, color);
	}

	public static GameObject PopTarget(Vector3 pos, Quaternion rot) {
		return PopSquare(pos, rot, true, Color.white);
	}

	static GameObject PopSquare(Vector3 pos, Quaternion rot, bool isTarget, Color color) {
		squares[currentIndex].Pop(pos, rot, isTarget, color);
		var square = squares[currentIndex].gameObject;
		currentIndex = ++currentIndex % maxSquare;
		return square;
	}
}
