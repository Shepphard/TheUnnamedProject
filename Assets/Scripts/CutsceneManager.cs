using UnityEngine;
using System.Collections;

public class CutsceneManager : MonoBehaviour {
	
	private Animator animPlayer;
	private BlockCTRL blockPlayer;
	
	// singleton
	private static CutsceneManager cutsceneManager;
	public static CutsceneManager Instance() {
		if (!cutsceneManager) {
			cutsceneManager = FindObjectOfType(typeof (CutsceneManager)) as CutsceneManager;
			if (!cutsceneManager)
				Debug.LogError("There needs to be one active ModalPanelscript on a GameObject in your scene.");
		}
		
		return cutsceneManager;
	}
	
	void Awake()
	{
		animPlayer = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Animator>();
		blockPlayer = animPlayer.GetComponent<BlockCTRL>();
	}
	
	public void PlayScene(int scene)
	{
		blockPlayer.BlockForCutscene();
		animPlayer.SetInteger("Scene", scene);
		animPlayer.SetTrigger("Play");
	}
	
	/* stops current scene,not neccessary for all scenes */
	public void StopScene()
	{
		animPlayer.SetTrigger("Stop");
		blockPlayer.UnblockForCutscene();
	}
}
