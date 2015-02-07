using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour {
	public AudioClip correctSE;
	public AudioClip missSE;

	public void Correct() {
		AudioSource.PlayClipAtPoint(correctSE, Vector3.zero);
	}

	public void Miss() {
		AudioSource.PlayClipAtPoint(missSE, Vector3.zero);
	}
}
