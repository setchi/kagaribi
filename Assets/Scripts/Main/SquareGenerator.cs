using UnityEngine;
using System.Linq;
using System.Collections;

public class SquareGenerator : MonoBehaviour {
	public GameObject greenSquare;
	public GameObject whiteSquare;
	public GameController gameController;

	void Start () {
		StartCoroutine(GreenGenerate());
		StartCoroutine(WhiteGenerate());
	}

	IEnumerator GreenGenerate() {
		for (;;) {
			var pos = RandomVector2FromRange(-30, 30);
			var square = (GameObject)Instantiate(greenSquare, new Vector3(pos.x, pos.y, 150), Quaternion.identity);
			square.GetComponent<Square>().gameController = gameController;

			var scale = RandomVector2FromRange(0.5f, 2);
			square.transform.localScale = scale;

			yield return new WaitForSeconds(0.001f);
		}
	}

	IEnumerator WhiteGenerate() {
		int chain = 3;

		for (;;) {
			var pos = RandomVector2FromRange(-3, 3);

			foreach (var i in Enumerable.Range(0, chain)) {
				var gap = RandomVector2FromRange(-3, 3) / 100;
				var square = (GameObject)Instantiate(whiteSquare, new Vector3(pos.x + gap.x, pos.y + gap.y, 150 + i * 1.5f), Quaternion.identity);
				square.GetComponent<Square>().gameController = gameController;
			}

			yield return new WaitForSeconds(1f);
		}
	}

	Vector2 RandomVector2FromRange(float min, float max) {
		return new Vector2(Random.Range(min, max), Random.Range(min, max));
	}
}
