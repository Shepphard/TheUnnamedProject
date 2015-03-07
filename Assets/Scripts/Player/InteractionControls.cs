using UnityEngine;
using System.Collections;

public class InteractionControls : MonoBehaviour {
	
	/* public */
	public float rayLength = 5f;	// length of the raycast to measure if object is close enough
	public float objectDistance = 1f; // distance from camera to carried object
	public float throwStrength = 100; // how strongly you throw objects away
	public Transform carriedObject = null;
	public bool debug = true;	// triggers debug functions to be played
	public Camera InspectCamera; // a reference to the camera to inspect items
	public float rotationSpeed = 10f; // speed of rotating in inspection
	public float zoomFactor = 100f; // speed of zooming in inspection
	
	public bool enableCtrls = true;
	public bool isAttacking = false;
	
	/* private */
	int interactableMask; // the layer of all the objects player can interact with
	Camera _cam; // reference to the main camera
	Inventory _inventory; // reference to the players inventory
	Equipment _equipment; // reference to the players equipment
	Transform itemPosition; //the gameobj of the hand
	Transform inspectingItem; // the item that is being inspected right now
	BlockCTRL blockCtrl;
	CutsceneManager cutsceneMgr;
	
	Animator arm_anim;
	
	/* setting up references */
	void Awake()
	{
		interactableMask = LayerMask.GetMask ("Interactable");
		_cam = GetComponentInChildren<Camera> ();
		_inventory = GetComponent<Inventory>();
		_equipment = GetComponent<Equipment>();
		itemPosition = _cam.transform.Find("itemPosition");
		blockCtrl = GetComponent<BlockCTRL>();
		
		arm_anim = GameObject.Find("Arm").GetComponent<Animator>();
		cutsceneMgr = CutsceneManager.Instance();
	}
	
	/* update function
	 *
	 * dealing with main input functions
	 */
	void Update()
	{
		if(carriedObject != null)
		{
			if(carriedObject.GetComponent<item>().item_type == "weapon")
			{
				isAttacking = carriedObject.GetComponent<Sword>().getIsAttacking();
				if(isAttacking)
				{
				}
			}
			
		}
		
		/* on right click */
		if (Input.GetKeyUp(KeyCode.Mouse1) && !_inventory._invBar.inspecting && enableCtrls)
		{
			
			// carrying something?
			if (carriedObject != null)
			{
				bool isEquipment = carriedObject.GetComponent<item>().isEquipment; //is it equipment?
				
				if (!isEquipment)
				{
					// theres space  in the inventory..
					if(!_inventory.isInvFull())
					{
						// put it in your inventory and activate the invbar
						_inventory.addItem(carriedObject.gameObject);
						clearCarriedObject();
					}
					// its full..
					else {
						// invbar is activated
						if (_inventory._invBar.activated) {
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
						else {
							//then just activate it, do nothing else
							_inventory._invBar.activate();
						}
					}
				}
				// PUT INTO EQUIPMENT
				else
				{
					positionEquipmentObject(carriedObject);
					_equipment.addItem(carriedObject.gameObject);
					carriedObject = null;
				}
				
			}
			// your hand is free
			else {
				//inventory is not empty..
				if (!_inventory.isInvEmpty())
				{
					// and is activated..
					if (_inventory._invBar.activated) {
						// remove current item and place it in your hand
						setCarriedObject(_inventory.removeItem().transform);
					} else {
						//activate it, nothing else
						_inventory._invBar.activate();
					}
				}
			}
		}
		
		/* Inspect an item */
		if (Input.GetKeyUp(KeyCode.E))
		{
			// is the invbar up and not empty?
			if (_inventory._invBar.activated && !_inventory.isInvEmpty())
			{
				// then toggle inspector
				if (!_inventory._invBar.inspecting)
					setInspectingObject();
				else
					clearInspectingObj();
			}
		}
		
		// equipment bar activation
		if (Input.GetKeyUp(KeyCode.B))
		{
			if (!_inventory._invBar.activated)
				_equipment.ActivateBar();
		}
		
		
		/* Scrollwheel Input */
		float scrollwheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (scrollwheelInput != 0)
		{
			/* INVENTORY */
			// not inspecting, just scroll thru
			if (!_inventory._invBar.inspecting)
			{
				// if inv_bar is already activated..
				if (_inventory._invBar.activated)
					scrollInv(scrollwheelInput);
				else if (!_equipment.GetBarActivated()) //activate the invbar when equipment bar is not activated
					_inventory._invBar.activate();
			}
			// else scroll thru items with inspector on
			else
			{
				scrollInv(scrollwheelInput);
				clearInspectingObj();
				setInspectingObject();
			}
			
			/* EQUIPMENT */
			if (_equipment.GetBarActivated())
			{
				scrollEquipment(scrollwheelInput, _equipment.barSelected);
			}
		}
		
		/* LEFT CLICK FOR EQUIPMENT BAR */
		if (Input.GetButtonDown ("PickUp"))
		{
			if(carriedObject != null && carriedObject.GetComponent<item>().item_type == "weapon" && enableCtrls)
			{
				float rnd = Random.value;
				
				AnimatorStateInfo state = arm_anim.GetCurrentAnimatorStateInfo(0);
				bool attacking = state.IsName("Slash") || state.IsName("Slash2");
				
				if (!attacking)
				{
					if(rnd > 0.5f)
						arm_anim.SetTrigger("Slash");
					else
						arm_anim.SetTrigger("Slash2");
					
					carriedObject.GetComponent<Sword>().Attack();
				}
			}

			if(carriedObject != null && carriedObject.GetComponent<item>().item_type == "gun" && enableCtrls)
			{
				carriedObject.GetComponent<Gun>().Shoot();
			}
			
			else if (_equipment.GetBarActivated())
			{
				if (!_equipment.barSelected)
					_equipment.SelectCurrentBar();
				else
				{
					
					_equipment.wearEquipment();
					_equipment.DeactivateBar();
				}
			}
		}
		
		/* is it inspecting? then go move it around */
		if (_inventory._invBar.inspecting)
		{
			inspectingMovement();
		}
		
		/* DEBUG placing item in hands */
		if(debug && carriedObject != null)
		{
			item i = carriedObject.GetComponent<item>();
			carriedObject.position = itemPosition.position + _cam.transform.right*i.positionOffsetInv.x + 
				_cam.transform.up * i.positionOffsetInv.y +
					_cam.transform.forward * i.positionOffsetInv.z;
			carriedObject.rotation = itemPosition.rotation;
			carriedObject.RotateAround(carriedObject.position, _cam.transform.up, i.carriedRotationInv.y);
			carriedObject.RotateAround(carriedObject.position, _cam.transform.right, i.carriedRotationInv.x);
			carriedObject.RotateAround(carriedObject.position, _cam.transform.forward, i.carriedRotationInv.z);
			
		}
		
	}// update
	
	/* FIXED UPDATE
	 * 
	 * deals with physics update, raycasting
	 */
	void FixedUpdate()
	{
		RaycastHit hitObject;
		
		/* Raycast on left click */
		if (Input.GetButtonDown ("PickUp")  && !_inventory._invBar.inspecting && enableCtrls)
		{
			//..check if player hits object/npc in reach
			hitObject = hitsObject ();
			
			if (hitObject.collider != null)
			{
				// is it an NPC?..
                if (hitObject.collider.tag == "NPC")
                {
                    // if yes, retrieve NPCInteraction script from the NPC
                    if (Application.loadedLevel == 5)
                        hitObject.collider.gameObject.GetComponent<NPCCosma>().Interaction();
                    else 
                        hitObject.collider.gameObject.GetComponent<NPCKnight>().Interaction();
                }
                //is it a Spaceship?..
                else if (hitObject.collider.tag == "Spaceship")
                    // if yes, retrieve Spaceshipscript and interact
                    hitObject.collider.gameObject.GetComponent<Spaceship>().Interaction();

                //..if you hit an pickup object and have no object in your hands yo
                else if (carriedObject == null && hitObject.collider.tag == "PickUp")
                    setCarriedObject(hitObject.collider.transform);

                else if (hitObject.collider.CompareTag(Tags.trigger))
                {
                    hitObject.collider.GetComponent<Trigger>().Triggered();
                }
			}
		}
		/* throw object */
		else if (Input.GetKeyDown(KeyCode.Q) && !_inventory._invBar.inspecting && enableCtrls)
		{
			// does player carry an object? then throw
			if (carriedObject != null) {
				Transform tmp = carriedObject;
				clearCarriedObject();
				tmp.rigidbody.AddForce(_cam.transform.forward * throwStrength);
			}
		}
	}
	
	/*
	 * PUBLIC FUNCTIONS
	 */
	
	/* SetCarriedObject
	 *
	 * Is always used to "put something in your hand"
	 * It "disables" the rigidbody and places
	 * the object using the itemPosition GameObj
	 * and the camera.
	 */
	public void setCarriedObject (Transform obj)
	{
		// pickup object
		carriedObject = obj;
		// deal with the rigidbody
		carriedObject.rigidbody.useGravity = false;
		carriedObject.rigidbody.isKinematic = true;
		//carriedObject.rigidbody.detectCollisions =false;
		
		carriedObject.collider.isTrigger = true;
		
		// Position of the carriedObject once
		item i = carriedObject.GetComponent<item>();
		
		GameObject handle = GameObject.Find("animation_handle");
		
		//set position
		carriedObject.position = handle.transform.position + 
			_cam.transform.right*i.positionOffsetInv.x + 
				_cam.transform.up * i.positionOffsetInv.y +
				_cam.transform.forward * i.positionOffsetInv.z;
		
		//apply rotation
		carriedObject.rotation = _cam.transform.rotation;
		carriedObject.RotateAround(carriedObject.position, _cam.transform.up, i.carriedRotationInv.y);
		carriedObject.RotateAround(carriedObject.position, _cam.transform.right, i.carriedRotationInv.x);
		carriedObject.RotateAround(carriedObject.position, _cam.transform.forward, i.carriedRotationInv.z);
		
		// apply scale
		carriedObject.localScale *= i.scaleFactorWhenCarried;
		
		// parent it to the animation_handle if its an inventory item
		if(!i.isEquipment)
			carriedObject.SetParent(handle.transform);
		else
			carriedObject.SetParent(GameObject.Find("equipment").transform);
			
		/* deal with triggering stuff */
		if (i.TriggerLookatHand)
		{
			// only trigger one of a kind!
			bool result;
			item.TriggerDict.TryGetValue(i.itemName, out result);
			if (!result)
			{
				cutsceneMgr.PlayScene(Cutscenes.lookat_hand);
				i.TriggerLookatHand = false;
				item.TriggerDict.Remove(i.itemName);
				item.TriggerDict.Add(i.itemName, true);
			}
		}
	} // setcarriedobject
	
	/*
	 * PRIVATE FUNCTIONS
	 */
	
	/**
	 * Checks for an object in range
	 * returns a RaycastHit Object
	 * 
	 * no object is hit when RaycastHit.collider == null
	 */
	RaycastHit hitsObject()
	{
		Ray ray = _cam.ScreenPointToRay(new Vector3(_cam.pixelWidth/2, _cam.pixelHeight/2, 0));
		
		Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow);
		
		RaycastHit objectHit;
		
		if (Physics.Raycast (ray, out objectHit, rayLength, interactableMask)) {
			//Vector3 playerToMouse = objectHit.point - transform.position; //not used
		}
		
		return objectHit;
	}
	
	/* ClearCarriedObject
	 * 
	 * Use always this to "put an item out of your hands"
	 * deals with the rigidbody, parenting
	 */
	public void clearCarriedObject()
	{
		item i = carriedObject.GetComponent<item>();
		
		// turn gravity back on
		carriedObject.rigidbody.useGravity = true;
		carriedObject.rigidbody.isKinematic = false;
		carriedObject.rigidbody.detectCollisions =true;
		carriedObject.collider.isTrigger = false;
		// reset all the forces applied
		carriedObject.rigidbody.velocity = Vector3.zero;
		carriedObject.rigidbody.angularVelocity = Vector3.zero;
		
		// reset scale
		carriedObject.localScale /= i.scaleFactorWhenCarried;
		
		// set free from camera
		carriedObject.transform.parent = null;
		
		// set carriedObject to null
		carriedObject = null;
	}
	
	/* SetInspetcingObject()
	 *
	 * Use this to put the currentselected item
	 * in the inspector
	 */
	void setInspectingObject()
	{
		// activate inspecting mode
		_inventory._invBar.activateInspecting();
		// turn off the camera movement
		
		blockCtrl.BlockLookingaround(true, true);
		
		// instantiate the obj to inspect to the inspectcamera and deal with rigidbody and so on
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
	
	/* ClearInspectingObj
	 *
	 * Use this to get out of inspecting mode
	 */
	void clearInspectingObj()
	{
		Destroy (inspectingItem.gameObject);
		_inventory._invBar.deactivateInspecting();
		
		blockCtrl.BlockLookingaround(false, false);
	}
	
	/* Scroll Inventory
	 *
	 * Uses Scrollwheel Input and scrolls through
	 * the inventory
	 */
	void scrollInv(float scrollwheelInput)
	{
		// scroll through the items
		if(scrollwheelInput>0)
			_inventory.decrementCurrentItem();
		else
			_inventory.incrementCurrentItem();
		_inventory._invBar.resetTimer();
	}
	
	void scrollEquipment(float scrollwheelInput, bool barSelected)
	{
		if(scrollwheelInput>0)
			_equipment.decrCurrent();
		else
			_equipment.incrCurrent();
		_equipment._equipmentBar.resetTimer();
	}
	
	/* InspectingMovement
	 *
	 * Movement of the item to be inspected
	 * using left and right holding mousebuttons
	 */
	void inspectingMovement()
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
	
	/*
	 * Use this to position equipment items once
	 * before adding to equipment
	 */
	void positionEquipmentObject(Transform obj)
	{
		item i = obj.GetComponent<item>();
		
		obj.localRotation = Quaternion.Euler(i.rotationOffsetEquip.x, i.rotationOffsetEquip.y, i.rotationOffsetEquip.z);
		obj.localPosition = i.positionOffsetEquip;
		
		obj.localScale *= i.scaleFactorWhenEquipped;
	}
	
	public bool carriesItem (string iName)
	{
		if (carriedObject != null)
		{
			item i = carriedObject.GetComponent<item>();
			return i.itemName == iName;
		}
		else
			return false;
	}
	
	/* only call from animation! */
	public void SwitchCarriedItem()
	{
		item i = carriedObject.GetComponent<item>();
		i.Switch();
	}
}