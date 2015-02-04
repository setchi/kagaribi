using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultSceneGUI : MonoBehaviour {
	public FadeManager fadeManager;
	public GameObject scoreObj;
	public GameObject bestObj;
	public Text scoreValue;
	public Text bestValue;

	string GetBestScore() {
		var localData = LocalStorage.Read<JsonModel.LocalData>();
		return localData != null ? localData.bestScore : "0";
	}

	void UpdateBestScore(string score) {
		var localData = LocalStorage.Read<JsonModel.LocalData>() ?? new JsonModel.LocalData();
		localData.bestScore = score;
		LocalStorage.Write<JsonModel.LocalData>(localData);
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
		fadeManager.FadeOut(0.5f, EaseType.linear, () => Application.LoadLevel("Title"));
	}
}
