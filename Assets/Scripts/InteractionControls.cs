using UnityEngine;
using System.Collections;

//TODO if Verschachteltungen neu überdenken (mehrere buttons gleichzeitig unterbinden)
//put in update because of physics (causes lagg?)
//bug manchmal wenn man rausgenommen hat kannmannich gleihc dasnächste rausnehmen
//rotation is at the bottom

public class InteractionControls : MonoBehaviour {
	
	public float rayLength = 5;	// length of the raycast to measure if object is close enough
	public float objectDistance = 1; // distance from camera to carried object
	public float throwStrength = 100; // how strongly you throw objects away
	public Transform carriedObject = null;

	int interactableMask; // the layer of all the objects player can interact with
	Camera camera;
	Inventory _inventory;
	MouseLook mouselook_player;
	MouseLook mouselook_camera;

	void Awake()
	{
		interactableMask = LayerMask.GetMask ("Interactable");
		camera = GetComponentInChildren<Camera> ();
		mouselook_camera = camera.GetComponent<MouseLook>();
		mouselook_player = GetComponent<MouseLook>();
		_inventory = GetComponent<Inventory>();
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

			// throw away
			if (Input.GetKeyDown(KeyCode.Q))
			{
				// add force to object
				carriedObject.rigidbody.AddForce(camera.transform.forward * throwStrength);
				
				//set carried object to null
				clearCarriedObject();
			}
			
			// right click
			if (Input.GetKeyUp(KeyCode.Mouse1))
			{
				// is inventory activated?
				if (_inventory._invBar.activated)
				{
					if(_inventory.isInvFull())
					{
						// FIX DA SWITCH
						//setCarriedObject(_inventory.switchItem(carriedObject.gameObject).transform);
					}
					else
					{
						_inventory.addItem(carriedObject.gameObject);
						clearCarriedObject();
					}
				}
				else 
				{
					_inventory.addItem(carriedObject.gameObject);
					clearCarriedObject();
				}
			}
		}
		// not holding an object
		else if (Input.GetKeyUp(KeyCode.Mouse1) && carriedObject == null && !_inventory.isInvEmpty())
		{
			// is the bar activated?
			if (_inventory._invBar.activated)
				// then take the current item in your hand
				setCarriedObject(_inventory.removeItem().transform);
		}
		
		// scrollwheel is activated
		float scrollwheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (scrollwheelInput != 0)
		{
			// if inv_bar is already activated..
			if (_inventory._invBar.activated)
			{
				// scroll through the items
				if(scrollwheelInput>0)
					_inventory.decrementCurrentItem();
				else
					_inventory.incrementCurrentItem();
				_inventory._invBar.resetTimer();
			}
			else //activate the invbar
			{
				_inventory._invBar.activate();
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
	
	public bool playerCarriesObject()
	{
		return carriedObject!=null;
	}
	
	// use this to set the carriedobject
	public void setCarriedObject (Transform obj)
	{
		// pickup object
		carriedObject = obj;
		// yes then turn of gravity so it doesnt fall out of your hands!
		carriedObject.rigidbody.useGravity = false;
		// Position of the carriedObject once
		carriedObject.position = camera.transform.position+camera.transform.forward*objectDistance;
		// make player be the parent of the object
		carriedObject.transform.parent = camera.transform;
		
		mouselook_camera.setCarriesObject(true);
		mouselook_player.setCarriesObject(true);
	}
	
	// use this to set carriedobject to null
	void clearCarriedObject()
	{
		mouselook_camera.setCarriesObject(false);
		mouselook_player.setCarriesObject(false);
		
		// reset all the forces applied
		carriedObject.rigidbody.velocity = Vector3.zero;
		carriedObject.rigidbody.angularVelocity = Vector3.zero;
		
		// turn gravity back on
		carriedObject.rigidbody.useGravity = true;
		
		// set free from camera
		carriedObject.transform.parent = null;
		// set carriedObject to null
		carriedObject = null;
	}
}

//			// Interactions possible with the picked Up Object
//			// calculate the amount to rotate if needed
//			float currentRotationX = -1*rotationSpeed*Time.deltaTime*Input.GetAxis("Mouse X");
//			float currentRotationY = rotationSpeed*Time.deltaTime*Input.GetAxis("Mouse Y");
//		
//			// rotate
//			if (Input.GetKey(KeyCode.Mouse1))
//			{
//				carriedObject.RotateAround(carriedObject.position, camera.transform.up, currentRotationX);
//				carriedObject.RotateAround(carriedObject.position, camera.transform.right, currentRotationY);
//			}