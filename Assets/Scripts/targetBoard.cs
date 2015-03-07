using UnityEngine;
using System.Collections;

public class targetBoard : MonoBehaviour 
{
	public int lifeCount = 3;

	public Transform fixatorT;


	// Use this for initialization
	void Awake() 
	{
		rigidbody.centerOfMass = new Vector3(0f, 0.2f, 0f);

	}

	public void TakeHit()
	{
		lifeCount--;
		Debug.Log ("Hit; Life: " + lifeCount);
		if(lifeCount < 1)
		{
			JointLimits j = hingeJoint.limits;
			j.max = 90f;
			j.min = 80f;
			hingeJoint.limits = j;
			hingeJoint.useSpring = false;
		}
	}
}
