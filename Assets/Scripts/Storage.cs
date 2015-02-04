using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Storage : MonoBehaviour {
	Dictionary<string, string> dictionary = new Dictionary<string, string>();

	static Storage instance_;
	static Storage Instance {
		get {
			if (instance_ == null) {
				var obj = new GameObject("Storage");
				instance_ = obj.AddComponent<Storage>();
			}

			return instance_;
		}
	}

	void Awake() {
		DontDestroyOnLoad(this);
	}

	public static string Get(string key) {
		if (Has(key)) {
			return Instance.dictionary[key];
		}
		return null;
	}

	public static void Set(string key, string value) {
		if (Has(key)) {
			Instance.dictionary[key] = value;
			return;
		}

		Instance.dictionary.Add(key, value);
	}

	public static bool Has(string key) {
		return Instance.dictionary.ContainsKey(key);
	}
}
