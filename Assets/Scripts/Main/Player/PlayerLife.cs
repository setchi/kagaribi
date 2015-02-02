using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;

public class PlayerLife : MonoBehaviour {
	public PlayerMovement playerMovement;
	public LifeGUI lifeGUI;
	public int startingLife = 5;

	int currentLife;
	float invincibleTime = 1.0f;
	bool isInvincible = false;
	bool isDead_ = false;
	public bool isDead { get { return isDead_; } }

	void Awake() {
		currentLife = startingLife;
	}

	public void TakeDamage() {
		if (isInvincible || isDead) {
			return;
		}
		StartCoroutine(StartInvincibleTime());

		currentLife--;
		lifeGUI.SetLife(currentLife);

		if (currentLife <= 0) {
			Death();
		}
	}

	void Death() {
		isDead_ = true;
		playerMovement.enabled = false;
	}

	IEnumerator StartInvincibleTime() {
		isInvincible = true;
		yield return new WaitForSeconds(invincibleTime);
		isInvincible = false;
	}
}
