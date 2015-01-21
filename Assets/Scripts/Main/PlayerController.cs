using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	float controllSpeed = 1;
	Vector3 baseDir;

	bool isTouch = false;

	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			isTouch = true;
			baseDir = Input.mousePosition;

		} else if (Input.GetMouseButtonUp(0)) {
			isTouch = false;

		}

		if (!isTouch) return;

		Vector3 pos = Input.mousePosition - baseDir;
		pos.x /= Screen.width * 1.5f;
		pos.y /= Screen.width * 1.5f;

		float x = Mathf.Clamp(pos.x, -1f, 1f);
		float y = Mathf.Clamp(pos.y, -1f, 1f);

		Camera.main.transform.Translate(
			x * controllSpeed,
			y * controllSpeed,
			0
		);
	}
}
