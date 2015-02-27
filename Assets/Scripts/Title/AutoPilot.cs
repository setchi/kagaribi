using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

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

		DOTween.Kill(gameObject);
		transform.DOMove(pos, distance / 30f).SetEase(Ease.InOutQuad).SetId(gameObject);
	}

	void Enqueue(GameObject target) {
		if (targetQueue.Count == 0)
			Move(target.transform.position);
		
		targetQueue.Enqueue(target);
	}
}
