using UnityEngine;
using System.Collections;

/**
 * シーン遷移で音が途切れないように再生する(仮)
 **/
public class AudioPlayer : SingletonGameObject<AudioPlayer> {
	private static AudioPlayer instance;
	AudioSource audioSource;

	void Awake() {
		DontDestroyOnLoad(this);
		audioSource = gameObject.AddComponent<AudioSource>();
	}

	public static void Play(AudioClip audioClip) {
		Instance.audioSource.clip = audioClip;
		Instance.audioSource.Play();
	}
}
