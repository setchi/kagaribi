using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour {
	public PlayerMovement playerMovement;
	bool isDead_ = false;
	public bool isDead { get { return isDead_; } }

	public void Death() {
		isDead_ = true;
		playerMovement.enabled = false;
	}
}
