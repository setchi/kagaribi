using UnityEngine;

public class TiltCamera : MonoBehaviour {
	public Vector2 range = new Vector2(5f, 3f);

	Transform trans;
	Quaternion startRot;
	Vector2 rotate = Vector2.zero;

	void Start () {
		trans = transform;
		startRot = trans.localRotation;
	}

	void Update () {
		Vector3 pos = Input.mousePosition;

		float halfWidth = Screen.width * 0.5f;
		float halfHeight = Screen.height * 0.5f;
		float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
		float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
		rotate = Vector2.Lerp(rotate, new Vector2(x, y), Time.deltaTime * 5f);

		trans.localRotation = startRot * Quaternion.Euler(-rotate.y * range.y, rotate.x * range.x, 0f);
	}
}
