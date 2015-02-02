using UnityEngine;
using System.Collections;

public class AutoBlink : MonoBehaviour {
	public float fastestInterval;
	public float latestInterval;
	public float maxAlpha;
	public float minAlpha;
	public SpriteRenderer spriteRenderer;
	Color color;

	void Start () {
		color = spriteRenderer.color;
		StartCoroutine(StartBlink());
	}

	IEnumerator StartBlink() {
		while (true) {
			color.a = random(minAlpha, maxAlpha);
			spriteRenderer.color = color;
			yield return new WaitForSeconds(random(fastestInterval, latestInterval));
		}
	}

	float random(float min, float max) {
		return min + Random.value * (max - min);
	}
}
