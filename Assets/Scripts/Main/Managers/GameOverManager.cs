using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
	public PlayerLife playerLife;
	public FadeManager fadeManager;

	bool gameOver = false;

	void Update () {
		if (!gameOver && playerLife.isDead) {
			gameOver = true;
			fadeManager.FadeOut(1f, EaseType.linear, () => Application.LoadLevel("Result"));
		}
	}
}
