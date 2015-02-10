using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoPilot : MonoBehaviour {
	public SquareGenerator squareGenerator;
	Queue<GameObject> targetQueue = new Queue<GameObject>();

	void Awake() {
		squareGenerator.onPopTarget += Enqueue;
	}

	void Update() {
		if (targetQueue.Count > 0) {
			var targetPos = targetQueue.Peek().transform.position;
			
			if (targetPos.z < 10) {
				targetQueue.Dequeue();

				var nextTargetPos = targetQueue.Peek().transform.position;
				Move(nextTargetPos);
			}
		}
	}
	
	void Move(Vector3 pos) {
		var distance = pos.z;
		pos.z = 0;
		
		TweenPlayer.CancelAll(gameObject);
		TweenPlayer.Play(gameObject, new Tween(distance / 30).MoveTo(gameObject, pos, EaseType.easeInOutQuad));
	}

	void Enqueue(GameObject square) {
		if (targetQueue.Count == 0)
			Move(square.transform.position);
		
		targetQueue.Enqueue(square);
	}
}
