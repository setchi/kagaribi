using UnityEngine;
using System.Collections;

public class PlayerEffect : MonoBehaviour {
	public ParticleSystem particleSystem;
	public GameObject circleEffect;
	
	Color particleStartColor_ = Color.white;
	Color particleEndColor_ = Color.cyan;
	public Color ParticleStartColor { get { return particleStartColor_; } }
	public Color ParticleEndColor { get { return particleEndColor_; } }

	void Update () {
		UpdateParticleColor();
	}
	
	void UpdateParticleColor() {
		var particleList = new ParticleSystem.Particle[particleSystem.particleCount];
		particleSystem.GetParticles(particleList);

		for (int i = 0; i < particleList.Length; ++i) {
			float lifePercentage = 1 - particleList[i].lifetime / (particleList[i].startLifetime);
			particleList[i].color = Color.Lerp(particleStartColor_, particleEndColor_, lifePercentage * 1.35f);
		}

		particleSystem.SetParticles(particleList, particleSystem.particleCount);
	}
	
	void ChangeParticleColor(float animationTime, Color startColor, Color endColor) {
		var cachedEndColor = particleEndColor_;
		var cachedStartColor = particleStartColor_;

		TweenPlayer.Play(gameObject, new Tween(animationTime).ValueTo(Vector3.zero, Vector3.one, EaseType.linear, pos => {
			particleEndColor_ = Color.Lerp(cachedEndColor, endColor, pos.x);
			particleStartColor_ = Color.Lerp(cachedStartColor, startColor, pos.x);
		}));
	}
	
	public IEnumerator ChangeParticleColorAfterDelay(Color color) {
		yield return new WaitForSeconds(5f / Time.timeScale);
		ChangeParticleColor(3f, Color.white, color);
	}
	
	public void Miss() {
		Instantiate(circleEffect, gameObject.transform.position, Quaternion.identity);
	}
}
