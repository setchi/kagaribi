using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject operationTarget;
	float controllSpeed = 1;
	ParticleSystem particleSystem;

	Color particleStartColor = Color.white;
	Color particleEndColor = Color.cyan;

	Vector3 touchPos;
	bool isTouch = false;

	void Awake () {
		particleSystem = GetComponent<ParticleSystem>();
	}

	void Update () {
		UpdateParticleColor();

		if (Input.GetMouseButtonDown(0)) {
			isTouch = true;
			touchPos = Input.mousePosition;

		} else if (Input.GetMouseButtonUp(0)) {
			isTouch = false;

		}

		if (isTouch) {
			Move(Input.mousePosition - touchPos);
		}
	}

	void Move(Vector3 velocity) {
		velocity.x /= Screen.width * 1.5f;
		velocity.y /= Screen.width * 1.5f;
		
		float x = Mathf.Clamp(velocity.x, -1f, 1f);
		float y = Mathf.Clamp(velocity.y, -1f, 1f);
		
		operationTarget.transform.Translate(
			x * controllSpeed,
			y * controllSpeed,
			0
		);
	}

	void UpdateParticleColor() {
		ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[particleSystem.particleCount];
		particleSystem.GetParticles(particleList);
		
		for (int i = 0; i < particleList.Length; ++i) {
			float lifePercentage = 1 - particleList[i].lifetime / (particleList[i].startLifetime);
			particleList[i].color = Color.Lerp(particleStartColor, particleEndColor ,lifePercentage * 1.35f);
		}   
		
		particleSystem.SetParticles(particleList, particleSystem.particleCount);
	}

	public void SetParticleColor(Color start, Color end) {
		TweenPlayer.Play(gameObject, new Tween(6f).ValueTo(Vector3.zero, Vector3.one, EaseType.linear, pos => {
			particleEndColor = Color.Lerp(particleEndColor, end, pos.x);
			particleStartColor = Color.Lerp(particleStartColor, start, pos.x);
		}));
	}
}
