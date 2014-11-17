using UnityEngine;
using System.Collections;

//TODO if Verschachteltungen neu überdenken (mehrere buttons gleichzeitig unterbinden)
//put in update because of physics (causes lagg?)
//bug manchmal wenn man rausgenommen hat kann man nich gleich das nächste rausnehmen
//rotation is at the bottom

public class InteractionControls : MonoBehaviour {
	
	public float rayLength = 5f;	// length of the raycast to measure if object is close enough
	public float objectDistance = 1f; // distance from camera to carried object
	public float throwStrength = 100; // how strongly you throw objects away
	public Transform carriedObject = null;
	public bool debug = true;
	public Camera InspectCamera;
	public float rotationSpeed = 10f;
	public float zoomFactor = 100f;

	int interactableMask; // the layer of all the objects player can interact with
	Camera camera;
	Inventory _inventory;
	MouseLook mouselook_player;
	MouseLook mouselook_camera;
	Transform itemPosition; //the gameobj of the hand
	Transform inspectingItem;
	

	void Awake()
	{
		interactableMask = LayerMask.GetMask ("Interactable");
		camera = GetComponentInChildren<Camera> ();
		mouselook_camera = camera.GetComponent<MouseLook>();
		mouselook_player = GetComponent<MouseLook>();
		_inventory = GetComponent<Inventory>();
		itemPosition = camera.transform.Find("itemPosition");
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Mouse1) && !_inventory._invBar.inspecting)
		{
			// carrying something?
			if (carriedObject != null)
			{
				// theres space  in the inventory..
				if(!_inventory.isInvFull())
				{
					// put it in your inventory and activate the invbar
					_inventory.addItem(carriedObject.gameObject);
					clearCarriedObject();
				}
				// its full..
				else
				{
					// invbar is activated
					if (_inventory._invBar.activated)
					{
						// switch it out with current item	
						// is there an item on the cursor?
						if (_inventory.checkCurrentItem())
						{
							// then switch it
							GameObject oldObj = carriedObject.gameObject;
							clearCarriedObject();
							GameObject newObj = _inventory.switchItem(oldObj);
							setCarriedObject(newObj.transform);
						}
					}
					// invbar not activated
					else
					{
						//then just activate it, do nothing else
						_inventory._invBar.activate();
					}
				}
			}
			// your hand is free
			else
			{
				//inventory is not empty..
				if (!_inventory.isInvEmpty())
				{
					// and is activated..
					if (_inventory._invBar.activated)
					{
						// remove current item and place it in your hand
						setCarriedObject(_inventory.removeItem().transform);
					}
					else
					{
						//activate it, nothing else
						_inventory._invBar.activate();
					}
					}
				}
		}
		
		// inspect an item
		if (Input.GetKeyUp(KeyCode.E))
		{
			if (_inventory._invBar.activated && !_inventory.isInvEmpty())
			{
				if (!_inventory._invBar.inspecting)
				{
					setInspectingObject();
				}
				else
				{
					clearInspectingObj();
				}
			}
		}
		
		// scrollwheel is activated
		float scrollwheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (scrollwheelInput != 0)
		{
			if (!_inventory._invBar.inspecting)
			{
				// if inv_bar is already activated..
				if (_inventory._invBar.activated)
				{
					scrollInv(scrollwheelInput);
				}
				else //activate the invbar
				{
					_inventory._invBar.activate();
				}
			}
			else
			{
				scrollInv(scrollwheelInput);
				clearInspectingObj();
				setInspectingObject();
			}
		}
		
		if (_inventory._invBar.inspecting)
		{
			float currentRotationX = -1*rotationSpeed*Time.deltaTime*Input.GetAxis("Mouse X");
			float currentRotationY = rotationSpeed*Time.deltaTime*Input.GetAxis("Mouse Y");
			float currentZoom = -1*zoomFactor*Time.deltaTime*Input.GetAxis("Mouse Y");
		
			// zoom
			if (Input.GetKey(KeyCode.Mouse0))
			{
				inspectingItem.Translate(InspectCamera.transform.forward * currentZoom, Space.World);
				Vector3 tmp = inspectingItem.position;
				inspectingItem.position = new Vector3(tmp.x, tmp.y, Mathf.Clamp(tmp.z, 2, 6));
			}
			if (Input.GetKey(KeyCode.Mouse1))
			{
				inspectingItem.RotateAround(inspectingItem.position, InspectCamera.transform.up, currentRotationX);
				inspectingItem.RotateAround(inspectingItem.position, InspectCamera.transform.right, currentRotationY);
			}
		}
		
		//debug itemoffsetcalculation
		if(debug && carriedObject != null)
		{
			item i = carriedObject.GetComponent<item>();
			
			// works
			carriedObject.position = itemPosition.position + camera.transform.right*i.positionOffset.x + 
				camera.transform.up * i.positionOffset.y +
					camera.transform.forward * i.positionOffset.z;
			carriedObject.rotation = itemPosition.rotation;
			carriedObject.RotateAround(carriedObject.position, camera.transform.up, i.carriedRotation.y);
			carriedObject.RotateAround(carriedObject.position, camera.transform.right, i.carriedRotation.x);
			carriedObject.RotateAround(carriedObject.position, camera.transform.forward, i.carriedRotation.z);
		}
		
	}// update

	void FixedUpdate()
	{
		RaycastHit hitObject;

		// on left click..
		if (Input.GetButtonDown ("PickUp")  && !_inventory._invBar.inspecting)
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
		else if (Input.GetKeyDown(KeyCode.Q) && !_inventory._invBar.inspecting)
		{
			// does player carry an object?
			if (carriedObject != null)
			{
				// throw it out
				
				// add force to object
				carriedObject.rigidbody.AddForce(camera.transform.forward * throwStrength);
				
				//set carried object to null
				clearCarriedObject();
			}
		}
		
//		// is player carrying an object?
//		if (carriedObject != null)
//		{            
//			// make sure its still in the same position
//			Vector3 offset = carriedObject.GetComponent<item>().positionOffset;
//			
//			carriedObject.position = camera.transform.position+offset;
//		}
		
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
		carriedObject.rigidbody.isKinematic = true;
		carriedObject.rigidbody.detectCollisions =false;
		
//		// Position of the carriedObject once
		item i = carriedObject.GetComponent<item>();
		
		// works
		carriedObject.position = itemPosition.position + camera.transform.right*i.positionOffset.x + 
																	camera.transform.up * i.positionOffset.y +
																	camera.transform.forward * i.positionOffset.z;
		carriedObject.rotation = itemPosition.rotation;
		carriedObject.RotateAround(carriedObject.position, camera.transform.up, i.carriedRotation.y);
		carriedObject.RotateAround(carriedObject.position, camera.transform.right, i.carriedRotation.x);
		carriedObject.RotateAround(carriedObject.position, camera.transform.forward, i.carriedRotation.z);
		
		// make player be the parent of the object
		carriedObject.transform.parent = camera.transform;
	}
	
	// use this to set carriedobject to null
	void clearCarriedObject()
	{
		// turn gravity back on
		carriedObject.rigidbody.useGravity = true;
		carriedObject.rigidbody.isKinematic = false;
		carriedObject.rigidbody.detectCollisions =true;
		
		// reset all the forces applied
		carriedObject.rigidbody.velocity = Vector3.zero;
		carriedObject.rigidbody.angularVelocity = Vector3.zero;

		// set free from camera
		carriedObject.transform.parent = null;
		
		// set carriedObject to null
		carriedObject = null;
	}
	
	void setInspectingObject()
	{
		_inventory._invBar.activateInspecting();
		mouselook_camera.setMouselookOn(false);
		mouselook_player.setMouselookOn(false);
		
		GameObject obj = (GameObject) Instantiate ((Object)_inventory.itemsList[_inventory.currentItem]);
		obj.SetActive(true);
		obj.rigidbody.useGravity = false;
		obj.rigidbody.isKinematic = true;
		obj.rigidbody.detectCollisions =false;
		inspectingItem = obj.transform;
		inspectingItem.parent = InspectCamera.transform;
		inspectingItem.gameObject.layer = LayerMask.NameToLayer("Inspect");
		inspectingItem.position = InspectCamera.transform.position+InspectCamera.transform.forward * objectDistance;
		
	}
	
	void clearInspectingObj()
	{
		Destroy (inspectingItem.gameObject);
		_inventory._invBar.deactivateInspecting();
		mouselook_camera.setMouselookOn(true);
		mouselook_player.setMouselookOn(true);
	}
	
	void scrollInv(float scrollwheelInput)
	{
		// scroll through the items
		if(scrollwheelInput>0)
			_inventory.decrementCurrentItem();
		else
			_inventory.incrementCurrentItem();
		_inventory._invBar.resetTimer();
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