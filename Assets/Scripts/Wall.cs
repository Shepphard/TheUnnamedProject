using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	
	public ParticleSystem clouds;
	
	private CutsceneManager mgr;
	
	public void PlayClouds()
	{
		clouds.Play();
		mgr = CutsceneManager.Instance();
		mgr.PlayScene(Cutscenes.wall_rising);
		BlockCTRL blocker = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<BlockCTRL>();
		blocker.BlockLookingaround(false, false);
	}
	
	public void DetroyClouds()
	{
		StartCoroutine("disableClouds");
		mgr.StopScene();
	}
	
	IEnumerator disableClouds()
	{
		clouds.Stop();
		yield return new WaitForSeconds(clouds.startLifetime+1);
		Destroy (clouds.gameObject);
	}
}