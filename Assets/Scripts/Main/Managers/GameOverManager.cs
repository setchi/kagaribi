using UnityEngine;
using System.Collections;

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
		TweenPlayer.Play(gameObject, new Tween(1f).ValueTo(Vector3.one, Vector3.zero, EaseType.linear, pos => {
			audioSource.volume = volume * pos.x;
		}));

		fadeManager.FadeOut(1f, EaseType.linear, () => Application.LoadLevel("Result"));
	}
}
