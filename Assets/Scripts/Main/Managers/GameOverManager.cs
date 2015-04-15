using UnityEngine;
using System.Collections;
using DG.Tweening;
using UniRx;

public class GameOverManager : MonoBehaviour {
	[SerializeField] PlayerLife playerLife;
	[SerializeField] FadeManager fadeManager;
	[SerializeField] AudioSource audioSource;

	void Awake() {
		playerLife.IsDead.Where(isDead => isDead)
			.Subscribe(_ => GameOver());
	}

	void GameOver() {
		var volume = audioSource.volume;
		DOTween.To(() => 1, value => audioSource.volume = volume * value, 0, 1f);
		fadeManager.FadeOut(1f, DG.Tweening.Ease.Linear, () => Application.LoadLevel("Result"));
	}
}
