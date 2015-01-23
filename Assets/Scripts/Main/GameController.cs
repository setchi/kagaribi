using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public EffectManager effectManager;
	public ScoreManager scoreManager;
	public FadeManager fadeManager;
	public SoundManager soundManager;
	public GameObject player;

	void Start() {
		fadeManager.FadeIn(1f, EaseType.linear);
	}

	public void Correct() {
		Time.timeScale += 0.01f;

		soundManager.Correct();
		scoreManager.Correct();
		effectManager.EmitCorrectEffect();
	}

	public void Miss() {
		Time.timeScale =  1;

		soundManager.Miss();
		scoreManager.Miss();
		effectManager.EmitMissEffect();
	}
}
