using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour {
	public PlayerLife playerLife;
	public GameObject healthPrefab;
	public GameObject healthGroup;

	List<Life> lifeObjects;

	void Awake () {
		SetupLifeObjects();
	}

	void SetupLifeObjects() {
		lifeObjects = Enumerable.Range(0, playerLife.startingHealth)
			.Select(i => {
				var health = (GameObject)Instantiate(healthPrefab, Vector3.zero, Quaternion.identity);
				health.transform.SetParent(healthGroup.transform);
				return health.GetComponent<Life>();
			}).ToList();
	}

	public void SetLife(int health) {
		foreach (var elem in lifeObjects
		         .Where((life, index) => index <= playerLife.startingHealth - health - 1 && !life.isLost)) {
			elem.Lost();
		}
	}
}
