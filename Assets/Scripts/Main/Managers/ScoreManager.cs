using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class ScoreManager : MonoBehaviour {
	[SerializeField] Text scoreText;
	Subject<int> correctStream = new Subject<int>();

	void Awake() {
		correctStream.AsObservable()
			.Scan((combo, one) => combo + one)
				.Scan((score, combo) => score + combo)
				.Select(score => score.ToString())
				.Do(score => Storage.Set("Score", score))
				.SubscribeToText(scoreText);
	}

	public void Correct() {
		correctStream.OnNext(1);
	}
}
