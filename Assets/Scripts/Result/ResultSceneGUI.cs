using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DG.Tweening;

public class ResultSceneGUI : MonoBehaviour {
	public FadeManager fadeManager;
	public GameObject scoreObj;
	public GameObject bestObj;
	public Text scoreValue;
	public Text scoreText;
	public Text bestValue;
	public Text bestText;
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
		fadeManager.FadeIn(1f, DG.Tweening.Ease.Linear);
	}

	IEnumerator StartDisplayAnimation() {
		var bestScore = int.Parse(GetBestScore());

		yield return new WaitForSeconds(1f);

		var y = 80f;
		var slideTime = 0.3f;
		
		scoreObj.transform.DOLocalMove(new Vector3(0, y, 0), slideTime).SetEase(Ease.OutBack);
		scoreText.DOFade(1, slideTime);
		scoreValue.DOFade(1, slideTime);

		yield return new WaitForSeconds(0.15f);
		
		bestObj.transform.DOLocalMove(new Vector3(0, -y, 0), slideTime).SetEase(Ease.OutBack);
		bestText.DOFade(1, slideTime);
		bestValue.DOFade(1, slideTime);

		yield return new WaitForSeconds(0.4f);

		// Score count up
		var score = float.Parse(Storage.Get("Score") ?? "0");
		var countUpSmoothing = 6f;

		for (float counter = 0f; counter <= score;) {
			var stringifyScore = Mathf.RoundToInt(counter).ToString();
			scoreValue.text = stringifyScore;

			if (counter > bestScore)
				bestValue.text = stringifyScore;

			counter += (score - counter) * countUpSmoothing * Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}
	}

	public void OnClickReturnButton() {
		AudioPlayer.Play(onePointSE);
		fadeManager.FadeOut(0.5f, DG.Tweening.Ease.Linear, () => Application.LoadLevel("Title"));
	}
}
