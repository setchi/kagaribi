using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

public class AutoPilot : MonoBehaviour {
	[SerializeField] SquareGenerator squareGenerator;

	void Awake() {
		squareGenerator.onPopTarget.First().Subscribe(target => Move(target.transform.position));

		squareGenerator.onPopTarget.Buffer(2, 1)
			.Subscribe(b => b[0].transform.ObserveEveryValueChanged(x => x.position)
					.Where(pos => pos.z < 10)
						.First().Subscribe(_ => Move(b[1].transform.position)));
	}

	void Move(Vector3 pos) {
		var distance = pos.z;
		pos.z = 0;

		DOTween.Kill(gameObject);
		transform.DOMove(pos, distance / 30f).SetEase(Ease.InOutQuad).SetId(gameObject);
	}
}
