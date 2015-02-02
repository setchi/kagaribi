using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Life : MonoBehaviour {
	Image lifeImage;
	bool isLost_ = false;
	public bool isLost { get { return isLost_; } }

	void Awake() {
		lifeImage = GetComponent<Image>();
	}

	public void Lost() {
		isLost_	 = true;
		lifeImage.color = new Color(1, 1, 1, 0.3f);
	}
}
