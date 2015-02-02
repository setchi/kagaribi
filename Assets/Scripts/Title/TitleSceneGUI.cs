using UnityEngine;
using System.Collections;

public class TitleSceneGUI : MonoBehaviour {
	public FadeManager fadeManager;

	void Start () {
		fadeManager.FadeIn(1.5f, EaseType.linear);
	}

	public void OnClickStartButton() {
		fadeManager.FadeOut(0.5f, EaseType.linear, () => Application.LoadLevel("Main"));
	}
}
