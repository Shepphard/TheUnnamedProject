using UnityEngine;
using System.Collections;

public class AssetSwitchNew : MonoBehaviour {

	public GameObject object1;
	public GameObject object2;

	private GameObject instObject1;
	private GameObject instObject2;
	private ParticleSystem cloudParticles;
	
	void Awake()
	{
		instObject1 = (GameObject) Instantiate(object1, transform.position, transform.rotation);
		instObject2 = (GameObject) Instantiate(object2, transform.position, transform.rotation);

		instObject1.transform.parent = gameObject.transform;
		instObject2.transform.parent = gameObject.transform;
		
		instObject2.SetActive(false);
		
		cloudParticles = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			instObject2.SetActive(!instObject2.activeSelf);
			instObject1.SetActive(!instObject1.activeSelf);
			cloudParticles.Play();
		}
	}
}
