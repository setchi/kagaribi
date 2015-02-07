using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public Text scoreText;
	int combo = 0;
	float score_;
	public int Score { get { return Mathf.RoundToInt(score_); } }

	public void Correct() {
		combo++;
		score_ += combo;

		var stringifyScore = Score.ToString();
		Storage.Set("Score", stringifyScore);
		scoreText.text = stringifyScore;
	}
}
