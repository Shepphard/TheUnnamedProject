using UnityEngine;
using System.Collections;

public class footstepSounds : MonoBehaviour {

	public AudioClip footstep;
	public float minVol = 0.5f;
	public float maxVol = 0.8f;
	public float minPitch = 0.8f;
	public float maxPitch = 1.2f;
	
	public void PlaceFoot()
	{
		float vol = Random.Range(minVol, maxVol);
		audio.pitch = Random.Range(minPitch, maxPitch);
		audio.PlayOneShot(footstep, vol);
	}
}
