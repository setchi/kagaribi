using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CircleEffect : MonoBehaviour {

	void Awake() {
		var renderer = gameObject.GetComponent<SpriteRenderer>();

		var time = 0.6f;
		gameObject.transform.DOScale(Vector3.one * 4f * Time.timeScale, time);
		renderer.DOFade(0, time).OnComplete(() => DestroyObject(gameObject));
	}

	void Update() {
		transform.Translate(0, 0, -13 * Time.deltaTime * Time.timeScale);
	}
}
