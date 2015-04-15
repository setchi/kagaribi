using UnityEngine;
using System.Collections;
using UniRx;

public class PlayerLife : MonoBehaviour {
	[SerializeField] PlayerMovement playerMovement;
	public readonly ReactiveProperty<bool> IsDead = new ReactiveProperty<bool>(false);

	void Awake() {
		IsDead.Subscribe(isDead => playerMovement.enabled = !isDead);
	}
}
