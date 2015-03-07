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
	
	public bool addItem(GameObject obj)
	{
		if (!isInvFull())
		{
			itemsList.Add(obj);
			item i_ = obj.GetComponent<item>();
			i_.setIsInInv(true);
			_invBar.addIcon((Sprite) i_.icon);
			currentItem = itemsList.IndexOf(obj);
			_invBar.activate();
			obj.SetActive(false);
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
			item i = tmp.GetComponent<item>();
			i.setIsInInv(false);
			itemsList.RemoveAt(currentItem);
			_invBar.removeIconAt(currentItem);
			tmp.SetActive(true);
		}
		if(currentItem > itemsList.Count-1)
			decrementCurrentItem();
		
		return tmp;
	}
	
	// returns null if there is no object to switch (doublechecked)
	public GameObject switchItem(GameObject newObj)
	{
		// is there an item?
		GameObject result = (GameObject) itemsList[currentItem];
		
		if (result != null)
		{
			// swich it out
			itemsList[currentItem] = newObj;
			item newItem = newObj.GetComponent<item>();
			newItem.setIsInInv(true);
			_invBar.iconList[currentItem] = (Sprite) newItem.icon;
			newObj.SetActive(false);
			
			_invBar.resetTimer();
			_invBar.triggerUpdateIcons();
			result.SetActive(true);
			result.GetComponent<item>().setIsInInv(false);
		}
		
		return result;
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
	
	// is there a current item?
	public bool checkCurrentItem()
	{
		return itemsList[currentItem] != null;
	}

    // is certain item in inventory
    public bool carriesItem(string iname)
    {
        bool result = false;
        foreach (GameObject obj in itemsList)
        {
            if (obj.GetComponent<item>().name == iname)
                result = true;
        }

        return result;
    }
}
