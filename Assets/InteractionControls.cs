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
	Transform carriedObject = null;

	void Awake()
	{
		interactableMask = LayerMask.GetMask ("Interactable");
		camera = GetComponentInChildren<Camera> ();
	}

	void FixedUpdate()
	{
		RaycastHit hitObject;

		// on left click..
		if (Input.GetButtonDown ("PickUp"))
		{
			//..and player does not carry an object..
			if (carriedObject == null)
			{
				//..check if player hits object in reach
				hitObject = hitsObject ();
				// if an object is hit
				if (hitObject.collider != null)
				{
					// pick up object
					carriedObject = hitObject.collider.transform;
				}
			}
			//..DOES carry an object, throw it away
			else
			{
				// turn gravity back on
				carriedObject.rigidbody.useGravity = true;

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
		}

		// is player carrying an object?
		if (carriedObject != null)
		{
			// yes then turn of gravity so it doesnt fall out of your hands!
			carriedObject.rigidbody.useGravity = false;
            
			// Position of the carriedObject
			carriedObject.position = camera.transform.position+camera.transform.forward*objectDistance;

			// Interactions possible with the picked Up Object
			// calculate the amount to rotate if needed
			float currentRotation = rotationSpeed*Time.deltaTime;
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
			}
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
}
