using UnityEngine;

public class GameController : ResultReceiver {
	public EffectManager effectManager;
	public ScoreManager scoreManager;
	public FadeManager fadeManager;
	public SoundManager soundManager;

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
	}
}
