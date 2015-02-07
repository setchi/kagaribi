using UnityEngine;
using System.Collections;

public abstract class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour {
	static T instance_;
	protected static T Instance {
		get {
			if (instance_ == null) {
				instance_ = FindObjectOfType<T>();
			}

			return instance_ ?? new GameObject(typeof(T).FullName).AddComponent<T>();
		}
	}
}
