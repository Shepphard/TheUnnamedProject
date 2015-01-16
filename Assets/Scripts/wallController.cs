using UnityEngine;
using System.Collections;

public class wallController : MonoBehaviour
{
	public Animator animWall;

	private GameObject player;
	private MusicController music;
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
		music = MusicController.Instance();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			animWall.SetTrigger("Play");
			music.PlaySFX(0);
			collider.enabled = false;
		}
	}
}
