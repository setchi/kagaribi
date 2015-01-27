using UnityEngine;
using System.Collections;

public class SquareGenerator : MonoBehaviour {
	public GameObject squarePrefab;
	public ResultReceiver resultReceiver;
	public GameObject player;
	
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
	
	public static GameObject PopSquare(Vector3 pos, Quaternion rot, bool isTarget, Color color) {
		squares[currentIndex].Pop(pos, rot, isTarget, color);
		var square = squares[currentIndex].gameObject;
		currentIndex = ++currentIndex % maxSquare;
		return square;
	}
}
