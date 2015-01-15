using UnityEngine;
using System.Collections;

public class wallController : MonoBehaviour
{
	public Animator animWall;

	private GameObject player;
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			animWall.SetTrigger("Play");
			collider.enabled = false;
		}
	}
}
