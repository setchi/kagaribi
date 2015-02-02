using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LifeGUI : MonoBehaviour {
	public PlayerLife playerLife;
	public GameObject lifePrefab;
	public GameObject lifeGroup;

	List<Life> lifeObjects;

	void Awake () {
		SetupLifeObjects();
	}

	void SetupLifeObjects() {
		lifeObjects = Enumerable.Range(0, playerLife.startingLife)
			.Select(i => {
				var health = (GameObject)Instantiate(lifePrefab, Vector3.zero, Quaternion.identity);
				health.transform.SetParent(lifeGroup.transform);
				return health.GetComponent<Life>();
			}).ToList();
	}

	public void SetLife(int health) {
		foreach (var elem in lifeObjects
		         .Where((life, index) => index <= playerLife.startingLife - health - 1 && !life.isLost)) {
			elem.Lost();
		}
	}
}
