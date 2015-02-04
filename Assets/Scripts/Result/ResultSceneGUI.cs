using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultSceneGUI : MonoBehaviour {
	public FadeManager fadeManager;
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
		var currentScore = Storage.Get("Score") ?? "0";

		if (int.Parse(GetBestScore()) < int.Parse(currentScore)) {
			// new record
			UpdateBestScore(currentScore);
		}

		bestValue.text = GetBestScore();
		scoreValue.text = currentScore;

		fadeManager.FadeIn(1f, EaseType.linear);
	}

	public void OnClickReturnButton() {
		fadeManager.FadeOut(1f, EaseType.linear, () => Application.LoadLevel("Title"));
	}
}
