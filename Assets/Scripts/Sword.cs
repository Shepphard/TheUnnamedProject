using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	bool isAttacking = false;

	float timer = 0f;
	float maxTimer = 1f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
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
		Debug.Log("trigger");
		if(isAttacking)
		{
			if(c.gameObject.tag == "destructable")
			{
				c.GetComponent<SpriteSwitcher>().Switch();
				Debug.Log("break1");
				isAttacking = false;
			}
		}
	}

	public void Attack()
	{
		Debug.Log("attack");

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
