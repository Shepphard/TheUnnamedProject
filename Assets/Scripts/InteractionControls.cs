using UnityEngine;
using System.Collections;

public class InteractionControls : MonoBehaviour {
	
	public float rayLength = 5;	// length of the raycast to measure if object is close enough
	public float objectDistance = 1; // distance from camera to carried object
	public float throwStrength = 100; // how strongly you throw objects away

	public float rotationSpeed = 50; // The speed of the Rotation
	public float rotation = 0; // The rotation that has already been performed

	int interactableMask; // the layer of all the objects player can interact with
	Camera camera;
	public Transform carriedObject = null;
	MouseLook mouselook_player;
	MouseLook mouselook_camera;

	void Awake()
	{
		interactableMask = LayerMask.GetMask ("Interactable");
		camera = GetComponentInChildren<Camera> ();
		mouselook_camera = camera.GetComponent<MouseLook>();
		mouselook_player = GetComponent<MouseLook>();
	}

	void FixedUpdate()
	{
		RaycastHit hitObject;

		// on left click..
		if (Input.GetButtonDown ("PickUp"))
		{
			//..check if player hits object/npc in reach
			hitObject = hitsObject ();
			
			if (hitObject.collider != null)
			{
				// is it an NPC?..
				if (hitObject.collider.tag == "NPC")
				{
					// if yes, retrieve NPCInteraction script from the NPC
					hitObject.collider.gameObject.GetComponent<NPCInteraction>().Interaction();
				}
				//..if you hit an pickup object and have no object in your hands yo
				else if (carriedObject == null && hitObject.collider.tag == "PickUp")
				{
				
					setCarriedObject(hitObject.collider.transform);
				
				}

			}
			
		}
		
		// is player carrying an object?
		if (carriedObject != null)
		{            
			// make sure its still in the same position
			carriedObject.position = camera.transform.position+camera.transform.forward*objectDistance;

			// Interactions possible with the picked Up Object
			// calculate the amount to rotate if needed
			float currentRotationX = -1*rotationSpeed*Time.deltaTime*Input.GetAxis("Mouse X");
			float currentRotationY = rotationSpeed*Time.deltaTime*Input.GetAxis("Mouse Y");
		
			// rotate
			if (Input.GetKey(KeyCode.Mouse1))
			{
				carriedObject.RotateAround(carriedObject.position, camera.transform.up, currentRotationX);
				carriedObject.RotateAround(carriedObject.position, camera.transform.right, currentRotationY);
			}
			
			// throw away
			if (Input.GetKeyDown(KeyCode.Q))
			{
				Debug.Log("Throw Button pressed");
				mouselook_camera.setCarriesObject(false);
				mouselook_player.setCarriesObject(false);
				
				// turn gravity back on
				carriedObject.rigidbody.useGravity = true;
				
				// free the object
				carriedObject.transform.parent = null;
				
				// reset all the forces applied
				carriedObject.rigidbody.velocity = Vector3.zero;
				carriedObject.rigidbody.angularVelocity = Vector3.zero;
				
				// add force to object
				carriedObject.rigidbody.AddForce(camera.transform.forward * throwStrength);
				
				// set carriedObject to null
				carriedObject = null;
				
				// set the rotation to 0
				rotation = 0;
			}
			
			/*
			 * OLD TURNING CONTROLS using Keys C and V
			 *
			//left turn
			if (Input.GetKey(KeyCode.C)) 
			{
				//rotate around the up-Vector (positive rotation)
				carriedObject.Rotate(carriedObject.transform.up, currentRotation);
			}

			//right turn
			if (Input.GetKey(KeyCode.V)) 
			{
				//rotate around the up-Vector (negative rotation)
				carriedObject.Rotate(carriedObject.transform.up, -currentRotation);
			}*/
		}
	}
	/**
	 * Checks for an object in range
	 * returns a RaycastHit Object
	 * 
	 * no object is hit when RaycastHit.collider == null
	 */
	RaycastHit hitsObject()
	{
		Ray ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, 0));
		
		Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow);
		
		RaycastHit objectHit;
		
		if (Physics.Raycast (ray, out objectHit, rayLength, interactableMask))
		{
				Vector3 playerToMouse = objectHit.point - transform.position;
				//Debug.Log ("Object is hit"); // works
		}

		return objectHit;
	}
	
	public bool playerCarriesObject()
	{
		return carriedObject!=null;
	}
	
	public void setCarriedObject (Transform obj)
	{
		// pickup object
		carriedObject = obj;
		// yes then turn of gravity so it doesnt fall out of your hands!
		carriedObject.rigidbody.useGravity = false;
		// Position of the carriedObject once
		carriedObject.position = camera.transform.position+camera.transform.forward*objectDistance;
		//make player be the parent of the object
		carriedObject.transform.parent = camera.transform;
		
		mouselook_camera.setCarriesObject(true);
		mouselook_player.setCarriesObject(true);
	}
}
