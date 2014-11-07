using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour {
	
	// Use this for initialization
	void Awake () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void Interaction()
	{
		if (!audio.isPlaying)
			audio.Play();
	} 
	
	void OnTriggerEnter()
	{
		
	}
}
