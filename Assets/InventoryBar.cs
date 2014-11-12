using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryBar : MonoBehaviour {

	[HideInInspector]
	public bool activated = false;
	public ArrayList iconList;
	public Vector2 scaleCurrent = new Vector2(0.7f, 0.7f);
	public Vector2 scaleOriginal = new Vector2(0.5f, 0.5f);
	public float activatedTime = 2.0f;

	private Inventory _inventory;
	private float timer = 0f;
	private Animator _anim;
	
	void Awake()
	{
		iconList = new ArrayList();
		_inventory = (Inventory) GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		_anim = GetComponent<Animator>();
	}
	
	void Update()
	{
		if (activated) {
			timer += Time.deltaTime;
			if (timer > activatedTime)
			{
				deactivate ();
			}
		}
		updateIconScales();
	}
	
	public void addIcon(Sprite i)
	{
		iconList.Add (i);
		updateIcons();
	}
	
	public void removeIconAt(int i)
	{
		iconList.RemoveAt(i);
		updateIcons();
	}
	
	public void activate()
	{
		// if its already activated, just reset timer
		if (!activated)
		{
			activated = true;
			_anim.SetBool("activated", activated);
		}
		resetTimer();
	}
	
	public void deactivate()
	{
		//timer=0f wrong?
		// if already deactivated, leave
		if (activated)
		{
			activated = false;
			_anim.SetBool("activated", activated);
		}
	}
	
	public void resetTimer()
	{
		timer = 0f;
	}
	
	void updateIcons()
	{
		for (int i=0; i<_inventory.maximumItems; i++)
		{
			Image img = (Image) transform.FindChild("icon"+i).gameObject.GetComponent<Image>();
			
			if (i < iconList.Count)
			{
				Sprite s = (Sprite) iconList[i];
				if (s!=null) {
					img.sprite = s;
					img.enabled = true;
				}
				else
					img.enabled = false;
			}
			else {
				img.enabled = false;
			}
		}
	}
	
	void updateIconScales()
	{
		for (int i=0; i<iconList.Count; i++)
		{
			RectTransform img = (RectTransform) transform.FindChild("icon"+i).gameObject.GetComponent<RectTransform>();
			
			if (i == _inventory.currentItem)
			{
				img.localScale = new Vector3(scaleCurrent.x, scaleCurrent.y, 1);
			}
			else
			{
				img.localScale = new Vector3(scaleOriginal.x, scaleOriginal.y, 1);
			}
		}
	}
}
