using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class SquareContainer : MonoBehaviour {
	int maxSquare = 700;
	int currentIndex = 0;
	Square[] squares;

	static GameObject squarePrefab = Resources.Load<GameObject>("Square");
	static SquareContainer instance_;
	static SquareContainer Instance {
		get {
			if (instance_ == null) {
				var obj = new GameObject("SquareContainer");
				instance_ = obj.AddComponent<SquareContainer>();
				instance_.SetupSquares();
			}
			return instance_;
		}
	}
	
	void Awake() {
		DontDestroyOnLoad(this);
	}
	
	void SetupSquares() {
		squares = Enumerable.Range(0, maxSquare).Select(i => {
			var square = (GameObject)Instantiate(squarePrefab, Vector3.zero, Quaternion.identity);
			square.transform.SetParent(gameObject.transform);
			return square.GetComponent<Square>();
		}).ToArray();
	}

	public static void ForEach(Action<Square> action) {
		foreach (var square in Instance.squares)
			action(square);
	}

	public static Square GetSquare() {
		var square = Instance.squares[Instance.currentIndex];
		Instance.currentIndex = ++Instance.currentIndex % Instance.maxSquare;
		return square;
	}
}
