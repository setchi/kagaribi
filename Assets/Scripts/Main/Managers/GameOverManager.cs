using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameOverManager : MonoBehaviour {
	public PlayerLife playerLife;
	public FadeManager fadeManager;
	public AudioSource audioSource;

	bool gameOver = false;

	void Update () {
		if (!gameOver && playerLife.isDead) {
			GameOver();
		}
	}

	void GameOver() {
		gameOver = true;

		var volume = audioSource.volume;
		DOTween.To(() => 1, value => audioSource.volume = volume * value, 0, 1f);
		fadeManager.FadeOut(1f, DG.Tweening.Ease.Linear, () => Application.LoadLevel("Result"));
	}
}
