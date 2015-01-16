using UnityEngine;
using System.Collections;

public class wallController : MonoBehaviour
{
	public NPCKnight npc;
	
	private GameObject player;
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag(Tags.player);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			npc.Interaction();
			collider.enabled = false;
		}
	}
}
