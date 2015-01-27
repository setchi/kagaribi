using UnityEngine;
using System.Collections;

public class TitleSceneUI : MonoBehaviour {
	public FadeManager fadeManager;

	// Use this for initialization
	void Start () {
		fadeManager.FadeIn(1.5f, EaseType.linear);
	}

	public void OnClickStartButton() {
		fadeManager.FadeOut(1.5f, EaseType.linear, () => Application.LoadLevel("Main"));
	}
}
