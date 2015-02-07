using UnityEngine;
using System.Collections;
/**
 * シーン遷移で音が途切れないように再生する(仮)
 **/
public class AudioPlayer : MonoBehaviour {
	private static AudioPlayer instance;
	AudioSource audioSource;
	
	// Singleton
	private AudioPlayer () {}
	
	static AudioPlayer Instance {
		get {
			if(instance == null) {
				instance = FindObjectOfType<AudioPlayer>();
				
				if (instance == null) {
					GameObject go = new GameObject("AudioPlayerSingleton");
					instance = go.AddComponent<AudioPlayer>();
				}
			}
			return instance;
		}
	}

	void Awake() {
		DontDestroyOnLoad(this);
		audioSource = gameObject.AddComponent<AudioSource>();
	}

	public static void Play(AudioClip audioClip) {
		Instance.audioSource.clip = audioClip;
		Instance.audioSource.Play();
	}
}
