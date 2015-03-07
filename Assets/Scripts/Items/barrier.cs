using UnityEngine;
using System.Collections;

public class barrier : MonoBehaviour 
{
	public targetBoard[] targets;


	private BoxCollider c;
	public ParticleSystem p;
	public bool passable = false;
	
	// Use this for initialization
	void Awake()
	{
		c = GetComponent<BoxCollider>();
		p = GetComponentInChildren<ParticleSystem>();
		if(!p)
		{
			Debug.LogError("no particlesystem");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		passable = true;
		foreach(targetBoard t in targets)
		{
			if(t.lifeCount > 0)
			{
				passable = false;
			}
		}
		if(!passable)
		{
			p.enableEmission = true;
			c.enabled = true;
		}
		if(passable)
		{	
			p.enableEmission = false;
			c.enabled = false;
		}
	}
}
