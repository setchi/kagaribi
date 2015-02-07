using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ResultSceneGUI : MonoBehaviour {
	public FadeManager fadeManager;
	public GameObject scoreObj;
	public GameObject bestObj;
	public Text scoreValue;
	public Text bestValue;
	public AudioClip onePointSE;
	
	void Retry(float waitTime, Action action) { StartCoroutine(StartRetry(waitTime, action)); }
	IEnumerator StartRetry(float waitTime, Action action) {
		yield return new WaitForSeconds(waitTime);
		action();
	}

	string GetBestScore() {
		return LocalData.Read().bestScore ?? "0";
	}

	void UpdateBestScore(string score) {
		var localData = LocalData.Read();
		localData.bestScore = score;
		LocalData.Write(localData);

		// ランキング登録
		API.ScoreRegistIfNewRecord(localData.playerInfo.id, score, checkRecord => {
		}, www => Retry(1f, () => UpdateBestScore(score)));
	}

	void Awake () {
		StartCoroutine(StartDisplayAnimation());
		bestValue.text = GetBestScore();

		var currentScore = Storage.Get("Score") ?? "0";
		if (int.Parse(GetBestScore()) < int.Parse(currentScore)) {
			// new record
			UpdateBestScore(currentScore);
		}
		fadeManager.FadeIn(1f, EaseType.linear);
	}

	IEnumerator StartDisplayAnimation() {
		var bestScore = int.Parse(GetBestScore());

		yield return new WaitForSeconds(1f);

		var y = 80f;
		var slideTime = 0.3f;
		TweenPlayer.Play(gameObject, new Tween(slideTime).ValueTo(
			scoreObj.transform.localPosition,
			new Vector3(0, y, 0), EaseType.easeOutBack,
			pos => scoreObj.transform.localPosition = pos
		));

		yield return new WaitForSeconds(0.15f);
		
		TweenPlayer.Play(gameObject, new Tween(slideTime).ValueTo(
			bestObj.transform.localPosition,
			new Vector3(0, -y, 0), EaseType.easeOutBack,
			pos => bestObj.transform.localPosition = pos
		));

		yield return new WaitForSeconds(0.4f);


		// Score count up
		var currentScore = float.Parse(Storage.Get("Score") ?? "0");
		var countUpSmoothing = 6f;

		for (float score = 0f; score <= currentScore;) {
			var scoreText = Mathf.RoundToInt(score).ToString();
			scoreValue.text = scoreText;

			if (score > bestScore)
				bestValue.text = scoreText;

			score += (currentScore - score) * countUpSmoothing * Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}
	}

	public void OnClickReturnButton() {
		AudioPlayer.Play(onePointSE);
		fadeManager.FadeOut(0.5f, EaseType.linear, () => Application.LoadLevel("Title"));
	}
}
