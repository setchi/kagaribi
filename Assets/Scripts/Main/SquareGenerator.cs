using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SquareGenerator : MonoBehaviour {
	public GameObject greenSquare;
	public GameObject whiteSquare;
	public GameController gameController;
	public PlayerController playerController;

	static int maxSquare = 3000;
	static int currentIndex = 0;
	static Square[] squares = new Square[maxSquare];
	[HideInInspector]
	public static float popDepth = 200;

	void Awake() {
		for (var i = 0; i < maxSquare; i++) {
			var square = (GameObject)Instantiate(greenSquare, Vector3.zero, Quaternion.identity);
			squares[i] = square.GetComponent<Square>();
			squares[i].gameController = gameController;
		}
	}

	void Start () {
		StartCoroutine(PatternChanger());
	}

	IEnumerator ChangeParticleColor(Color color) {
		yield return new WaitForSeconds(5f * (1 / Time.timeScale));
		playerController.SetParticleColor(Color.white, color);
	}

	IEnumerator PatternChanger() {
		var alpha = 0.05f;
		var colorPairList = new List<Tuple<Color, Color>> {
			new Tuple<Color, Color>(Color.cyan, Color.magenta),
			new Tuple<Color, Color>(Color.magenta, Color.yellow),
			new Tuple<Color, Color>(Color.cyan, Color.yellow),
			new Tuple<Color, Color>(Color.green, new Color(0, 1, 0, alpha)),
			new Tuple<Color, Color>(Color.red, new Color(1, 0, 0, alpha)),
			new Tuple<Color, Color>(Color.green, Color.cyan),
			new Tuple<Color, Color>(Color.magenta, Color.red),
			new Tuple<Color, Color>(Color.cyan, Color.blue),
			new Tuple<Color, Color>(Color.magenta, Color.blue)
		};

		var PatternRoutineList = new List<Func<Tuple<Color, Color>, List<IEnumerator>>>() {

			// Multiple Cross Circle
			colorPair => {
				var routines = new List<IEnumerator>();
				var multiple = UnityEngine.Random.Range(1, 6);
				var dir = UnityEngine.Random.Range(0, 2);
				
				routines.Add(BackgroundPattern.Circular(10, 10, 80, dir == 0 ? 1 : -1, multiple, colorPair));
				routines.Add(BackgroundPattern.Circular(10, 10, 80, dir == 1 ? 1 : -1, multiple, colorPair));
				routines.Add(TargetPattern.Circular(3f, 7.5f, 5, UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1));
				routines.Add(ChangeParticleColor(colorPair.Item1));
				return routines;
			
			// Multiple Circle
			}, colorPair => new List<IEnumerator>() { 
				BackgroundPattern.Circular(10, 10, 80, UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1, UnityEngine.Random.Range(2, 6), colorPair),
				TargetPattern.Circular(3f, 7.5f, 5, UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1),
				ChangeParticleColor(colorPair.Item1),

			// Random Position
			}, colorPair => new List<IEnumerator>() {
				BackgroundPattern.RandomPositionAndScale(-30, 30, colorPair),
				TargetPattern.RandomPosition(3, 1.5f, 0),
				ChangeParticleColor(colorPair.Item1)
			
			// Multiple Cross Polygon
			}, colorPair => new List<IEnumerator>() { /**
				BackgroundPattern.Triangle(10, 10, 70, 3, colorPair),
				TargetPattern.RandomPosition(3, 1.5f, 0),
				ChangeParticleColor(colorPair.Item1)
			
			// Cross Cube
			}, colorPair => new List<IEnumerator>() { /**/
				BackgroundPattern.Triangle(20, 10, 70, 4, colorPair),
				TargetPattern.RandomPosition(6, 3f, 4),
				ChangeParticleColor(colorPair.Item1)
			}
		};

		var index = 0;
		var colorIndex = 0;
		for (;;) {
			var color = 0;
			do {
				color = UnityEngine.Random.Range(0, colorPairList.Count);
			} while (color == colorIndex);

			var routineWorks = PatternRoutineList[index](colorPairList[color]);
			foreach (var routine in routineWorks) StartCoroutine(routine);

			yield return new WaitForSeconds(15f);

			foreach (var routine in routineWorks) StopCoroutine(routine);
			index = ++index % PatternRoutineList.Count;
			colorIndex = color;
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

	public static Vector3 GenerateRandomVectorFromRange(float min, float max) {
		return new Vector3(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
	}
}
