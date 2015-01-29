using UnityEngine;
using System.Collections;

public class CorrectEffet : MonoBehaviour {

	void Awake() {
		var renderer = gameObject.GetComponent<SpriteRenderer>();
		
		TweenPlayer.Play(gameObject, new Tween(1f * Time.timeScale)
		                 .ScaleTo(gameObject, Vector3.one, Vector3.one * 5f * Time.timeScale, EaseType.linear)
		                 .FadeTo(renderer, 1, 0, EaseType.linear)
		                 .Complete(() => DestroyObject(gameObject)));
	}

	void Update() {
		transform.Translate(0, 0, -5 * Time.deltaTime * Time.timeScale);
	}
}
