﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultGUIColorChanger : MonoBehaviour {
	public PlayerEffect playerEffect;
	public Image panelImage;
	public List<Outline> outlineList;

	void Update () {
		UpdateColor(playerEffect.ParticleEndColor);
	}

	void UpdateColor(Color color) {
		//panelImage.color = color;
		outlineList.ForEach(text => text.effectColor = color);
	}
}
