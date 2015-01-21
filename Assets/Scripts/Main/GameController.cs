using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public EffectManager effectManager;
	public ScoreManager scoreManager;
	public FadeManager fadeManager;

	static int initialSpeed = 30;
	int speed_ = initialSpeed;
	public int speed {
		get { return speed_; }
	}

	void Start() {
		fadeManager.FadeIn(1f, EaseType.linear);
	}

	public void Correct() {
		scoreManager.Correct();
		effectManager.EmitCorrectEffect();
		speed_ += 1;
	}

	public void Miss() {
		scoreManager.Miss();
		effectManager.EmitMissEffect();
		speed_ = initialSpeed;
	}
}
