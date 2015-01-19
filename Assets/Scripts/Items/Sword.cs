using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public int strength;
	public AudioClip sfx_swordSlash;
	public float sfx_Volume = 0.8f;

	bool isAttacking = false;
	float timer = 0f;
	float maxTimer = 0.4f;
	
	void Awake()
	{
		if (audio == null)
			gameObject.AddComponent<AudioSource>();
	}
	
	void Update () 
	{
		if(isAttacking)
		{
			timer += Time.deltaTime;
		}

		if(timer > maxTimer)
		{
			timer = 0f;
			isAttacking = false;
		}
	}

	void OnTriggerEnter(Collider c)
	{	
		if(isAttacking)
		{
			if(c.gameObject.tag == "destructable")
			{
				c.GetComponent<SpriteSwitcher>().AttemptBreak(strength);
				isAttacking = false;
			}
		}
	}

	public void Attack()
	{
		isAttacking = true;
		
		// play sound slightly randomized
		float pitch = Random.Range(0.8f, 1.2f);
		audio.pitch = pitch;
		float volume = Random.Range(sfx_Volume-1, sfx_Volume+1);
		audio.PlayOneShot(sfx_swordSlash, volume);
	}

	public void setIsAttacking(bool b)
	{
		isAttacking = b;
	}

	public bool getIsAttacking()
	{
		return isAttacking;
	}
}
