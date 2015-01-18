using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public int strength;

	bool isAttacking = false;
	float timer = 0f;
	float maxTimer = 0.4f;
	
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
