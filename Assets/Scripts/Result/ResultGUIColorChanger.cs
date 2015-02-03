using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultGUIColorChanger : MonoBehaviour {
	public PlayerEffect playerEffect;
	public Image panelImage;
	public Outline scoreTextDecoration;
	public Outline returnTextDecoration;

	void Update () {
		UpdateColor(playerEffect.ParticleEndColor);
	}

	void UpdateColor(Color color) {
		//panelImage.color = color;
		scoreTextDecoration.effectColor = color;
		returnTextDecoration.effectColor = color;
	}
}
