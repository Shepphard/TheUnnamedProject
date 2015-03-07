using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{
	//[HideInInspector]
	public GameObject projectile;

	public Vector3 muzzleOffset = new Vector3(0f, 0.2f, 0.7f);
	public float strength = 10f;

	
	public AudioClip sfx_laser;
	public float sfx_Volume = 0.8f;

	private float timer;
	private float maxTimer = 1.5f;


	ParticleSystem particle;
	// Use this for initialization
	void Awake()
	{
		if (audio == null)
			gameObject.AddComponent<AudioSource>();
		particle = GetComponentInChildren<ParticleSystem>();
		timer = maxTimer + 1;
	}

	void Update()
	{
		if(timer < maxTimer)
		{
			timer += Time.deltaTime;
		}
	}


	public void Shoot()
	{
		if(timer >= maxTimer)
		{
			GameObject newProjectile = (GameObject)Instantiate(projectile, transform.position + transform.TransformDirection(muzzleOffset), transform.rotation);
			newProjectile.rigidbody.AddForce(transform.forward * strength);
			particle.Play();
			// play sound slightly randomized
			float pitch = Random.Range(0.8f, 1.2f);
			audio.pitch = pitch;
			float volume = Random.Range(sfx_Volume-1, sfx_Volume+1);
			audio.PlayOneShot(sfx_laser, volume);
			timer = 0f;
		}
	}
}
