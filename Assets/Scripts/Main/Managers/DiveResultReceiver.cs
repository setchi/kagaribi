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

	public override void Perfect() {
		soundManager.Correct();
		scoreManager.Correct();
		effectManager.EmitCorrectEffect();
	}

	public override void Good() {
		Time.timeScale += 0.02f;

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
