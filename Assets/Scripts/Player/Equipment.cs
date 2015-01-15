using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

	[HideInInspector]
	public int currentBar = 0;
	[HideInInspector]
	public int currentItem = 0;
	public int amountBarItems = 2;
	public ArrayList bar0list; // head stuff
	public ArrayList bar1list; // left hand stuff
	public EquipmentBar _equipmentBar; // a reference
	public bool barSelected = false;
	public GameObject emptyObject0;
	public GameObject emptyObject1;
	
	public ArrayList currentList;
	
	private bool init = false;
	private GameObject currentEquipment0 = null;
	private GameObject currentEquipment1 = null;

	void Awake()
	{
		bar0list = new ArrayList();
		bar1list = new ArrayList();
	}
	
	void Update()
	{
		if(!init)
		{
			GameObject obj0 = Instantiate (emptyObject0) as GameObject;
			GameObject obj1 = Instantiate (emptyObject1) as GameObject;
			addItem(obj0);
			addItem(obj1);
			init = true;
		}
	}
	
	public void ActivateBar() {
		_equipmentBar.activate();
	}
	public void DeactivateBar() {
		_equipmentBar.deactivate();
	}
	public bool GetBarActivated() {
		return _equipmentBar.activated;
	}
	
	public void decrCurrent()
	{
		if (!barSelected) // scroll through bars
		{
			currentBar--;
			if (currentBar < 0)
				currentBar += amountBarItems;
		} else // scroll through items
		{
			currentItem++;
			if (currentItem>=currentList.Count)
				currentItem %= currentList.Count;
		}
	}
	
	public void incrCurrent()
	{
		if (!barSelected) // scroll through bars
		{
			currentBar++;
			if (currentBar>=amountBarItems)
				currentBar %= amountBarItems;
		} else // scroll through items
		{
			currentItem--;
			if (currentItem < 0)
				currentItem += currentList.Count;
		}
	}
	
	public void SelectCurrentBar()
	{
		barSelected = true;
		if (currentBar == 0)
			currentList = bar0list;
		else if (currentBar == 1)
			currentList = bar1list;
		
		// always take currentItem from 0 when a bar is selected
		currentItem = 0;
			
		_equipmentBar.activateBar(currentBar, currentList.Count);
	}
	
	public void addItem(GameObject obj)
	{
		item i_ = obj.GetComponent<item>();
		
		// check if it is equipment
		if (i_.isEquipment)
		{
			// put it in bar 0
			if (i_.belongsToEquipmentBar == 0)
			{
				bar0list.Add(obj);
				_equipmentBar.addIcon((Sprite) i_.icon, 0);
			}
			else if (i_.belongsToEquipmentBar == 1)
			{
				bar1list.Add(obj);
				_equipmentBar.addIcon((Sprite) i_.icon, 1);
			}
			
			/* set off */
			if (obj.collider != null)
				obj.collider.enabled = false;
			obj.SetActive(false);
		}
	}
	
	public void wearEquipment()
	{
		// head bar 0
		if (currentBar == 0)
		{
			if (currentEquipment0 != null)
				currentEquipment0.SetActive(false);
			
			// selected empty item?
			if (currentItem == 0) {
				currentEquipment0 = null;
			} else {
				GameObject obj = (GameObject)currentList[currentItem];
				currentEquipment0 = obj;
				obj.SetActive(true);
			}
		}
		// left hand bar 1
		else if (currentBar == 1)
		{
			if (currentEquipment1 != null)
				currentEquipment1.SetActive(false);
		
			// selected empty item?
			if (currentItem == 0) {
				currentEquipment1 = null;
			} else {
				GameObject obj = (GameObject)currentList[currentItem];
				currentEquipment1 = obj;
				obj.SetActive(true);
			}
		}
	}
	
	public GameObject getCurrentItem()
	{
		return (GameObject)currentList[currentItem];
	}
	
	// bar == 0 if it is a head item
	// bar == 1 if it is a left hand item
	public bool containsItem(string iName, int bar)
	{
		bool result = false;
		ArrayList list = new ArrayList();
		
		if (bar == 0)
			list = bar0list;
		else if (bar == 1)
			list = bar1list;
			
		foreach (GameObject obj in list) {
			item i = obj.GetComponent<item>();
			if (i.itemName == iName)
			{
				result = true;
				break;
			}
		}
		
		return result;
	}
	
	// bar == 0 if it is a head item
	// bar == 1 if it is a left hand item
	public bool wearsItem(string iName, int bar)
	{
		bool result = false;
		if (bar == 0 && currentEquipment0 != null)
			result = currentEquipment0.GetComponent<item>().itemName == iName;
		else if (bar == 1 && currentEquipment1 != null)
			result = currentEquipment1.GetComponent<item>().itemName == iName;
			
		return result;
	}
}
