using UnityEngine;
using System.Collections;

public class AssetSwitchNew : MonoBehaviour {

	public GameObject object1;
	public GameObject object2;
	public KeyCode switchKey;

	private GameObject instObject1;
	private GameObject instObject2;
	private ParticleSystem cloudParticles;
	private InteractionControls _controls;
	private bool blockSwitching = false;
	
	void Awake()
	{
		instObject1 = (GameObject) Instantiate(object1, transform.position, transform.rotation);
		instObject2 = (GameObject) Instantiate(object2, transform.position, transform.rotation);
		
		instObject1.GetComponent<item>().setAssetSwitcher(this);
		instObject2.GetComponent<item>().setAssetSwitcher(this);
		//instObject1.transform.parent = gameObject.transform;
		//instObject2.transform.parent = gameObject.transform;
		
		instObject2.SetActive(false);
		
		cloudParticles = GetComponentInChildren<ParticleSystem>();
		
		_controls = (InteractionControls) GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionControls>();
	}
	
	/*
	 * REFACTOR PLEASE
	 */
	// Update is called once per frame
	void Update ()
	{
		// triggered and both items have to be outside of the inv
		if (Input.GetKeyDown(switchKey) && !blockSwitching)
		{
			if (instObject1.activeSelf)
			{
				cloudParticles.transform.position = instObject1.transform.position;
				cloudParticles.Play();
				
				instObject2.transform.position = instObject1.transform.position;
				instObject2.transform.rotation = instObject1.transform.rotation;
			
				if (_controls.carriedObject == instObject1.transform)
				{
					_controls.setCarriedObject(instObject2.transform);
				}
				instObject2.transform.parent = instObject1.transform.parent;
				
				instObject1.rigidbody.useGravity = true;
				
				instObject1.SetActive(false);
				instObject2.SetActive(true);
				
			}
			else
			{
				cloudParticles.transform.position = instObject2.transform.position;
				cloudParticles.Play();
				
				instObject1.transform.position = instObject2.transform.position;
				instObject1.transform.rotation = instObject2.transform.rotation;
				
				if (_controls.carriedObject == instObject2.transform)
				{
					_controls.setCarriedObject(instObject1.transform);
				}
				instObject1.transform.parent = instObject2.transform.parent;
				
				instObject2.rigidbody.useGravity = true;
				
				instObject2.SetActive(false);
				instObject1.SetActive(true);
			}
		}
	}
	
	
	public void setBlockSwitching(bool b)
	{
		blockSwitching = b;
	}
}
