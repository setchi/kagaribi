using UnityEngine;
using System.Collections;

public class DiveResultReceiver : ResultReceiver {
	public EffectManager effectManager;
	public ScoreManager scoreManager;
	public FadeManager fadeManager;
	public SoundManager soundManager;
	public PlayerLife playerLife;

	void Start() {
		fadeManager.FadeIn(1f, EaseType.linear);
	}

	public override void Correct() {
		Time.timeScale += 0.01f;

		soundManager.Correct();
		scoreManager.Correct();
		effectManager.EmitCorrectEffect();
	}

	public override void Miss() {
		Time.timeScale = 1;

		soundManager.Miss ();
		scoreManager.Miss ();
		effectManager.EmitMissEffect();
		playerLife.TakeDamage();
	}

}
