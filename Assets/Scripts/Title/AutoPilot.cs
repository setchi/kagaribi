using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

public class AutoPilot : MonoBehaviour {
	[SerializeField] SquareGenerator squareGenerator;

	void Awake() {
		var targetQueue = new Queue<GameObject>();

		squareGenerator.onPopTarget.Subscribe(targetQueue.Enqueue);
		squareGenerator.onPopTarget.First().Subscribe(target => Move(target.transform.position));

		this.UpdateAsObservable().Select(_ => targetQueue).Where(queue => queue.Count > 0)
			.Where(queue => queue.Peek().transform.position.z < 10)
				.Do(queue => queue.Dequeue())
				.Subscribe(queue => Move(queue.Peek().transform.position));
	}
	
	void Move(Vector3 pos) {
		var distance = pos.z;
		pos.z = 0;

		DOTween.Kill(gameObject);
		transform.DOMove(pos, distance / 30f).SetEase(Ease.InOutQuad).SetId(gameObject);
	}
}
