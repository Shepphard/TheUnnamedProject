using UnityEngine;
using System.Collections;

public class AI_gun : MonoBehaviour 
{

	private Gun gun;
	private Transform playerT;
	private Rigidbody playerR;


	public float maxDistance = 20f; //maximum target scan distance

	private float timer = 0f;
	public float maxTimer = 10f;	//Time between shots
	// Use this for initialization
	void Start () 
	{
		gun = GetComponent<Gun>();
		GameObject g = GameObject.FindGameObjectWithTag("Player");
		playerT = g.transform;
		playerR = g.rigidbody;
		timer = maxTimer+1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(timer <= maxTimer)
		{
			timer += Time.deltaTime;
		}
		//is the player in Range?
		float d = Vector3.Distance(transform.position, playerT.position);
		if(d <= maxDistance)
		{
			
			//look at the player
			transform.LookAt(playerT);

			//shoot Ray to check if the player is visible
			RaycastHit hit;
			Physics.Raycast(transform.position, transform.forward, out hit);
			if(hit.collider.CompareTag("Player") && timer > maxTimer)
			{
				Debug.Log(d/maxDistance);
				transform.LookAt(playerT.position + (playerR.velocity * d/maxDistance * Random.Range(0.5f, 2f)));
				timer = 0f;
				gun.Shoot();
			}
		}
	}
}
