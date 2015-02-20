using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class SquareContainer : SingletonGameObject<SquareContainer> {
	static GameObject squarePrefab = Resources.Load<GameObject>("Square");

	int maxSquare = 700;
	int currentIndex = 0;
	Square[] squares;
	
	void Awake() {
		DontDestroyOnLoad(this);
	}
	
	Square[] SetupSquares() {
		squares = Enumerable.Range(0, maxSquare).Select(i => {
			var square = Instantiate(squarePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			square.transform.SetParent(gameObject.transform);
			return square.GetComponent<Square>();
		}).ToArray();

		return squares;
	}

	static Square[] GetSquares() {
		return Instance.squares ?? Instance.SetupSquares();
	}

	public static void ForEach(Action<Square> action) {
		foreach (var square in GetSquares())
			action(square);
	}

	public static Square GetSquare() {
		var square = GetSquares()[Instance.currentIndex];
		Instance.currentIndex = ++Instance.currentIndex % Instance.maxSquare;
		return square;
	}
}
