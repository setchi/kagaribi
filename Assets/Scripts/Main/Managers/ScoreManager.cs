using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public Text scoreText;

	void Update () {
		scoreText.text = Mathf.RoundToInt(score_).ToString();
	}

	float score_;
	public float Score {
		get { return score_; }
	}

	public void Correct() {
		score_ += 10;
		Storage.Set("Score", score_.ToString());
	}

	public void Miss() {
		score_ += -5;
		if (score_ < 0) score_ = 0;

		Storage.Set("Score", score_.ToString());
	}
}
