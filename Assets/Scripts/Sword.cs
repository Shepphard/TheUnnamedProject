using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	bool isAttacking = false;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider c)
	{	
		if(isAttacking)
		{
			if(c.gameObject.tag == "destructable")
			{
				/*
				 * cutDown the object
				 */
				Debug.Log("break1");
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
