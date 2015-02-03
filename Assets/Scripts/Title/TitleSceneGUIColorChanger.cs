using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TitleSceneGUIColorChanger : MonoBehaviour {
	public PlayerEffect playerEffect;
	public Outline gameTitleTextDecoration;
	public Outline startButtonTextDecoration;
	public ParticleSystem particleSystem1;
	public ParticleSystem particleSystem2;
	
	void Update () {
		UpdateGUIColor();
		UpdateParticleColor(particleSystem1, 0.4f);
		UpdateParticleColor(particleSystem2, 0.4f);
	}

	void UpdateParticleColor(ParticleSystem particleSystem, float alpha) {
		var particleList = new ParticleSystem.Particle[particleSystem.particleCount];
		particleSystem.GetParticles(particleList);

		for (int i = 0; i < particleList.Length; ++i) {
			float lifePercentage = 1 - particleList[i].lifetime / (particleList[i].startLifetime);

			var color = Color.Lerp(
				playerEffect.ParticleEndColor,
				playerEffect.ParticleEndColor,
				lifePercentage * 1.35f
			);

			color.a = alpha * (1 - lifePercentage);
			particleList[i].color = color;
		}
		
		particleSystem.SetParticles(particleList, particleSystem.particleCount);
	}

	void UpdateGUIColor() {
		gameTitleTextDecoration.effectColor = playerEffect.ParticleEndColor;
		startButtonTextDecoration.effectColor = playerEffect.ParticleEndColor;
	}
}
