using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	bool hit = false;
	ParticleSystem particle;

	void Awake()
	{
		particle = GetComponentInChildren<ParticleSystem>();
	}
	void Update()
	{
		if(hit && particle.isStopped)
		{
			DestroyObject(this.gameObject);
		}
	}
	void OnCollisionEnter(Collision c)
	{
		if(!hit)
		{
			particle.Play();
			hit = true;
			rigidbody.useGravity = false;
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

		if(c.gameObject.CompareTag("Target"))
		{
			c.gameObject.GetComponent<targetBoard>().TakeHit();
		}

	}

}
