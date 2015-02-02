using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public GameObject operationTarget;

	public float moveSpeed = 1;

	Vector3 touchPos;
	bool isTouch = false;

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			isTouch = true;
			touchPos = Input.mousePosition;

		} else if (Input.GetMouseButtonUp(0)) {
			isTouch = false;

		}

		if (isTouch) {
			Move(Input.mousePosition - touchPos);
		}
	}

	void Move(Vector3 velocity) {
		velocity.x /= Screen.width * 1.5f;
		velocity.y /= Screen.width * 1.5f;
		
		float x = Mathf.Clamp(velocity.x, -1f, 1f);
		float y = Mathf.Clamp(velocity.y, -1f, 1f);
		
		operationTarget.transform.Translate(
			x * moveSpeed,
			y * moveSpeed,
			0
		);
	}
}
