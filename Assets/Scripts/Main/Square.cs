using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {
	public bool white;

	[HideInInspector]
	public GameController gameController;
	
	bool collided;
	float size = 1.5f;

	void Update () {
		transform.Translate(0, 0, -gameController.speed * Time.deltaTime * Time.timeScale);

		var z = transform.position.z;

		if (transform.position.z < -20) {
			DestroyObject(gameObject);
		}
		
		if (white && !collided && z < 0) {
			Judge(transform.position);
			collided = true;
		}
	}

	void Judge(Vector3 pos) {
		pos -= Camera.main.transform.localPosition;

		if (Mathf.Abs(pos.x) < size && Mathf.Abs(pos.y) < size) {
			gameController.Correct();

		} else {
			gameController.Miss();
		}
	}
}
