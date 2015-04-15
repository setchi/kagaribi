using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] GameObject operationTarget;
	[SerializeField] float moveSpeed = 1;

	void Awake() {
		var touchDownStream = this.UpdateAsObservable()
			.Where(_ => Input.GetMouseButtonDown(0))
				.Select(_ => Input.mousePosition);

		this.UpdateAsObservable()
			.SkipUntil(touchDownStream)
				.TakeWhile(_ => !Input.GetMouseButtonUp(0))
				.RepeatSafe()
				.CombineLatest(touchDownStream, (_, startPos) => startPos)
				.Subscribe(startPos => Move(Input.mousePosition - startPos));
	}

	void Move(Vector3 velocity) {
		velocity /= Screen.width * 1.5f;

		float x = Mathf.Clamp(velocity.x, -1f, 1f);
		float y = Mathf.Clamp(velocity.y, -1f, 1f);

		operationTarget.transform.Translate(
			x * moveSpeed,
			y * moveSpeed,
			0
		);
	}
}
