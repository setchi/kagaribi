using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ScreenEffectManager : MonoBehaviour {
	public Image missFlush;
	public PlayerEffect playerEffect;

	public void EmitMissEffect() {
		playerEffect.Miss();

		DOTween.Kill(gameObject);
		missFlush.color = Color.white * 0.7f;
		missFlush.DOFade(0, 0.3f).SetId(gameObject);
	}

	public void EmitCorrectEffect() {
	}
}
