using UnityEngine;
using System.Collections;

public abstract class AbstractSquareManager : MonoBehaviour {
	public abstract GameObject PopBackground(Vector3 pos, Quaternion rot, Color color);
	public abstract GameObject PopTarget(Vector3 pos, Quaternion rot);
}
