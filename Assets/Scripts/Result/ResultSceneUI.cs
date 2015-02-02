using UnityEngine;
using System.Collections;

public class ResultSceneUI : MonoBehaviour {
	public FadeManager fadeManager;

	void Awake () {
		fadeManager.FadeIn(1f, EaseType.linear);
	}

	public void OnClickReturnButton() {
		fadeManager.FadeOut(1f, EaseType.linear, () => Application.LoadLevel("Title"));
	}
}
