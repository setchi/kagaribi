using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ResultGUIColorChanger : MonoBehaviour {
	public PlayerEffect playerEffect;
	public Image panelImage;
	public List<Shadow> textDecorations;

	void Update () {
		UpdateColor(playerEffect.ParticleEndColor);
	}

	void UpdateColor(Color color) {
		//panelImage.color = color;
		textDecorations.ForEach(text => text.effectColor = color);
	}
}
