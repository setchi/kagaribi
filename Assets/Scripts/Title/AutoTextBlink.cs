using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class AutoTextBlink : MonoBehaviour {
	Text text;

	void Start () {
		text = GetComponent<Text>();
		Blink();
	}

	void Blink() {
		var textColor = text.color;

		DOTween.To(() => 0f, value => {
			textColor.a = Mathf.PingPong(value * 2 + 1, 1);
			text.color = textColor;
		}, 1f, 1.5f).SetEase(Ease.OutSine).OnComplete(Blink);
	}
}
