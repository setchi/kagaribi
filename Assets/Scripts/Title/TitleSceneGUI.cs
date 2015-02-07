using UnityEngine;
using System.Collections;

public class TitleSceneGUI : MonoBehaviour {
	public FadeManager fadeManager;
	public AudioClip onePointSE;

	void Start () {
		fadeManager.FadeIn(1.5f, EaseType.linear);
	}

	public void OnClickStartButton() {
		AudioPlayer.Play(onePointSE);
		fadeManager.FadeOut(0.5f, EaseType.linear, () => Application.LoadLevel("Main"));
	}
}
