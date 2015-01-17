using UnityEngine;
using System.Collections;

public class LookingAt : MonoBehaviour {

	public float lookLength = 5f;

	private Camera _cam;
	private int interactableMask;
	
	void Awake()
	{
		_cam = GetComponentInChildren<Camera> ();
		interactableMask = LayerMask.GetMask ("Interactable");
	}

	void FixedUpdate()
	{
		RaycastHit hitObject;
		hitObject = hit ();
			if (hitObject.collider != null)
			{
				if (hitObject.collider.CompareTag(Tags.pickup))
				{
					// trigger animation maybe
					
				}
			}
	}
	
	RaycastHit hit()
	{
		Ray ray = _cam.ScreenPointToRay(new Vector3(_cam.pixelWidth/2, _cam.pixelHeight/2, 0));
		
		RaycastHit objectHit;
			
		if (Physics.Raycast (ray, out objectHit, lookLength, interactableMask)) {
			//Vector3 playerToMouse = objectHit.point - transform.position; //not used
		}
		
		return objectHit;
	}
}
