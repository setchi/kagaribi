using UnityEngine;
using System.Collections;

public class TitleSceneGUI : MonoBehaviour {
	public FadeManager fadeManager;
	public AudioClip onePointSE;

	void Start () {
		fadeManager.FadeIn(1.5f, DG.Tweening.Ease.Linear);
	}

	public void OnClickStartButton() {
		AudioPlayer.Play(onePointSE);
		fadeManager.FadeOut(0.5f, DG.Tweening.Ease.Linear, () => Application.LoadLevel("Main"));
	}
}
