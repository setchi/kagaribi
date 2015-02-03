using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoTextBlink : MonoBehaviour {
	Text text;

	void Start () {
		text = GetComponent<Text>();
		Blink();
	}

	void Blink() {
		var textColor = text.color;

		TweenPlayer.Play(gameObject, new Tween(1.5f).ValueTo(Vector3.zero, Vector3.one, EaseType.easeOutSine, pos => {
			textColor.a = Mathf.PingPong(pos.x * 2 + 1, 1);
			text.color = textColor;

		}).Complete(Blink));
	}
}
