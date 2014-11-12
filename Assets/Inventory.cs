using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

	public int maximumItems = 5;
	public ArrayList itemsList;
	public InventoryBar _invBar;
	public int currentItem = 0;
	
	void Awake()
	{
		itemsList = new ArrayList();
	}
	
	public bool addItem(GameObject i)
	{
		if (!isInvFull())
		{
			itemsList.Add(i);
			_invBar.addIcon((Sprite) i.GetComponent<item>().icon);
			currentItem = itemsList.IndexOf(i);
			_invBar.activate();
			i.SetActive(false);
			return true;
		}
		// select an item to switch out
		_invBar.activate();
		return false;
	}
	
	public GameObject removeItem()
	{
		GameObject tmp = (GameObject) itemsList[currentItem];
	
		// is there an item on this slot?
		if (tmp != null)
		{
			itemsList.RemoveAt(currentItem);
			_invBar.removeIconAt(currentItem);
			tmp.SetActive(true);
		}
		if(currentItem > itemsList.Count-1)
			decrementCurrentItem();
		
		return tmp;
	}
	
	public GameObject switchItem(GameObject obj)
	{
		int cursor = currentItem;
		GameObject tmp = removeItem();
		
		itemsList.Insert(cursor, obj);
		_invBar.addIcon((Sprite) obj.GetComponent<item>().icon);
		currentItem = cursor;
		_invBar.activate();
		obj.SetActive(false);
		
		return tmp;
	}
	
	public void incrementCurrentItem()
	{
		if(itemsList.Count>0)
		{
			currentItem++;
			if (currentItem>=itemsList.Count)
				currentItem %= itemsList.Count;
		}
	}
	
	public void decrementCurrentItem()
	{
		currentItem--;
		if (currentItem < 0)
			currentItem += itemsList.Count;
	}
	
	public bool isInvFull()
	{
		return(itemsList.Count >= maximumItems);
	}
	
	public bool isInvEmpty()
	{
		return(itemsList.Count <= 0);
	}
}
