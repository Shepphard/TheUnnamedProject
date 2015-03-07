using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour 
{
	//[HideInInspector]
	public GameObject projectile;

	public Vector3 muzzleOffset = new Vector3(0f, 0.2f, 0.7f);
	public float strength = 10f;

	private float timer;
	private float maxTimer = 1.5f;


	ParticleSystem particle;
	// Use this for initialization
	void Awake()
	{
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
			timer = 0f;
		}
	}
}
