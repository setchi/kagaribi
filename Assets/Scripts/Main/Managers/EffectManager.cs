using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EffectManager : MonoBehaviour {
	public Image missFlush;
	public PlayerEffect playerEffect;

	public void EmitMissEffect() {
		playerEffect.Miss();

		var color = missFlush.color;
		TweenPlayer.CancelAll(gameObject);
		TweenPlayer.Play(gameObject, new Tween(0.3f).ValueTo(Vector3.one * 0.5f, Vector3.zero, EaseType.linear, pos => {
			color.a = pos.x;
			missFlush.color = color;
		}));
	}

	public void EmitCorrectEffect() {
	}
}
