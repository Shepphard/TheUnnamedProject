using UnityEngine;
using System.Collections;

public class SpriteSwitcher : MonoBehaviour 
{
	public Sprite sprite0;
	public Sprite sprite1;
	public int strengthNeeded;

	bool active0 = true;
	SpriteRenderer _render;

	ParticleSystem particles;

	Collider c;

	// Use this for initialization
	void Awake()
	{
		_render = GetComponent<SpriteRenderer>();
		_render.sprite = sprite0;
		particles = GetComponentInChildren<ParticleSystem>();
		c = this.collider;
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(active0)
		{
			_render.sprite = sprite0;
		}
		else
		{
			_render.sprite = sprite1;
		}

		if(c != null)
			c.isTrigger = !active0;
	}

	public void Switch()
	{
		if(particles != null && active0)
			particles.Play();
		active0 = false;
		foreach (Transform child in transform)
		{
			child.GetComponent<SpriteSwitcher>().Switch();
		}
	}
	
	public void AttemptBreak(int strength)
	{
		if (strength >= strengthNeeded)
		{
			Switch ();
			Debug.Log("Break!");
		}
		else
		{
			Debug.Log("TOO WEAK");
		}
	}
}
