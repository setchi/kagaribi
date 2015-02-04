using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultSceneGUI : MonoBehaviour {
	public FadeManager fadeManager;
	public Text scoreText;

	void Awake () {
		scoreText.text = "score: " + Storage.Get("Score");
		fadeManager.FadeIn(1f, EaseType.linear);
	}

	public void OnClickReturnButton() {
		fadeManager.FadeOut(1f, EaseType.linear, () => Application.LoadLevel("Title"));
	}
}
