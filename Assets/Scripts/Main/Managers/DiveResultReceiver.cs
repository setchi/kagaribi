using UnityEngine;
using System.Collections;

public class DiveResultReceiver : ResultReceiver {
	public ScoreManager scoreManager;
	public FadeManager fadeManager;
	public SoundEffectManager soundEffectManager;
	public ScreenEffectManager screenEffectManager;
	public PlayerLife playerLife;

	void Start() {
		fadeManager.FadeIn(1f, EaseType.linear);
	}

	public override void Perfect() {
		soundEffectManager.Correct();
		scoreManager.Correct();
		screenEffectManager.EmitCorrectEffect();
	}

	public override void Good() {
		Time.timeScale += 0.01f;

		soundEffectManager.Correct();
		scoreManager.Correct();
		screenEffectManager.EmitCorrectEffect();
	}

	public override void Miss() {
		soundEffectManager.Miss();
		screenEffectManager.EmitMissEffect();
		playerLife.Death();
	}

}
