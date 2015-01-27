using UnityEngine;
using System.Collections;

public class specht : MonoBehaviour {

	public void Play()
	{
		if (!audio.isPlaying)
			audio.Play();
	}
}
