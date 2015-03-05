using UnityEngine;
using System.Collections;

public class LeavesPile : MonoBehaviour 
{
	public void OnTriggerEnter(Collider c)
	{
		GetComponent<ParticleSystem>().Play();
	}

}
