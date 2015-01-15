using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public ParticleSystem clouds;
	
	public void PlayClouds()
	{
		clouds.Play();
	}
	
	public void DetroyClouds()
	{
		StartCoroutine("disableClouds");
	}
	
	IEnumerator disableClouds()
	{
		clouds.Stop();
		yield return new WaitForSeconds(clouds.startLifetime+1);
		Destroy (clouds.gameObject);
	}
}