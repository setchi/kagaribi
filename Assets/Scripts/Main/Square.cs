using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	bool isCorrectJudged = false;
	bool stopping = true;
	float size = 1.5f;
	bool isTarget;

	[HideInInspector]
	public ResultReceiver resultReceiver;

	[HideInInspector]
	public GameObject player;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		Hide();
	}

	void Update () {
		if (stopping) return;

		transform.Translate(0, 0, -30 * Time.deltaTime * Time.timeScale);

		var z = transform.position.z;
		if (z < -20) {
			Hide();
		}
		
		if (isTarget && !isCorrectJudged && z < 0) {

			if (IsCorrect(transform.position)) {
				resultReceiver.Correct();
				
			} else {
				resultReceiver.Miss();
			}

			isCorrectJudged = true;
		}
	}

	bool IsCorrect(Vector3 pos) {
		pos -= player.transform.position;
		return Mathf.Abs(pos.x) < size && Mathf.Abs(pos.y) < size;
	}

	public void Pop(Vector3 pos, Quaternion rot, bool isTarget, Color color) {
		Show();

		this.isTarget = isTarget;
		spriteRenderer.color = color;

		transform.position = pos;
		transform.rotation = rot;
		transform.localScale = Vector3.one;
	}

	public void Show() {
		stopping = false;
		spriteRenderer.enabled = true;
	}

	public void Hide() {
		stopping = true;
		spriteRenderer.enabled = false;
	}
}
