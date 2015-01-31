using UnityEngine;
using System.Collections;

public class CircleEffect : MonoBehaviour {

	void Awake() {
		var renderer = gameObject.GetComponent<SpriteRenderer>();
		
		TweenPlayer.Play(gameObject, new Tween(0.6f * Time.timeScale)
		                 .ScaleTo(gameObject, Vector3.one, Vector3.one * 4f * Time.timeScale, EaseType.linear)
		                 .FadeTo(renderer, 1, 0, EaseType.linear)
		                 .Complete(() => DestroyObject(gameObject)));
	}

	void Update() {
		transform.Translate(0, 0, -13 * Time.deltaTime * Time.timeScale);
	}
}
