using UnityEngine;
using System.Collections;

public class kamerafahrt : MonoBehaviour {

	public Camera mainCam;
	private Animator anim;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			anim.SetTrigger("start");
		}
	}
	
	public void destroyCamera()
	{
		mainCam.enabled = true;
		Destroy(this);
	}
}
