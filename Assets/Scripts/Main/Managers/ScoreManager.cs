using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public Text scoreText;
	int combo = 0;
	float score_;
	public int Score { get { return Mathf.RoundToInt(score_); } }

	void Awake() {
		SetScore("0");
	}

	public void Correct() {
		combo++;
		score_ += combo;

		SetScore(Score.ToString());
	}

	void SetScore(string score) {
		Storage.Set("Score", score);
		scoreText.text = score;
	}
}
