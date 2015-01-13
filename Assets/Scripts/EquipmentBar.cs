using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentBar : MonoBehaviour {

	[HideInInspector]
	public bool activated = false;
	public float activatedTime = 2.0f;
	public Vector2 scaleCurrent = new Vector2(1.2f, 1.2f);
	public Vector2 scaleOriginal = new Vector2(0.9f, 0.9f);
	public ArrayList iconlist0;
	public ArrayList iconlist1;
	
	private float timer = 0f;
	private Animator _anim;
	private BlockCTRL _blockCTRL;
	private Equipment _equip;

	void Awake()
	{
		iconlist0 = new ArrayList();
		iconlist1 = new ArrayList();
		_anim = GetComponent<Animator>();
		_equip = (Equipment) GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>();
		_blockCTRL = GameObject.FindGameObjectWithTag("Player").GetComponent<BlockCTRL>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (activated) {
			timer += Time.deltaTime;
			if (timer > activatedTime) {
				deactivate ();
			}
		}
		updateBarIconScales();
		
		if (_equip.barSelected)
			updateIconScales();
	}
	
	/* activate equipment bar */
	public void activate()
	{
		// if its already activated, just reset timer
		if (!activated)
		{
			activated = true;
			_anim.SetBool("activated", activated);
		}
		resetTimer();
		
		// block
		_blockCTRL.BlockMovement(true);
		_blockCTRL.BlockLookingaround(true, true);
		_blockCTRL.BlockInteractionCtrl(true);
	}
	
	/* deactivate equipment bar */
	public void deactivate()
	{
		//timer=0f wrong?
		// if already deactivated, leave
		if (activated)
		{
			activated = false;
			_anim.SetBool("activated", activated);
			_equip.barSelected = false;
			_anim.SetInteger("currentBar", 0);
			_anim.SetInteger("itemCount", 0);
			
			// unblock
			_blockCTRL.BlockMovement(false);
			_blockCTRL.BlockLookingaround(false, false);
			_blockCTRL.BlockInteractionCtrl(false);
		}
	}
	
	public void resetTimer() {
		timer = 0f;
	}
	
	// updates the icons from the bars themselves
	void updateBarIconScales()
	{
		for (int i=0; i<_equip.amountBarItems; i++) // amount of bar icons (currently 2)
		{
			RectTransform img = (RectTransform) transform.FindChild("icon"+i).gameObject.GetComponent<RectTransform>();
			
			if (i == _equip.currentBar)
			{
				img.localScale = new Vector3(scaleCurrent.x, scaleCurrent.y, 1);
			}
			else
			{
				img.localScale = new Vector3(scaleOriginal.x, scaleOriginal.y, 1);
			}
		}
	}
	
	// updates the icon scales on the current bar
	void updateIconScales()
	{
		for (int i=0; i<_equip.currentList.Count; i++) {
			RectTransform img = (RectTransform) transform.FindChild("Bar"+_equip.currentBar).FindChild("icon"+i).gameObject.GetComponent<RectTransform>();
			if (i == _equip.currentItem)
				img.localScale = new Vector3(scaleCurrent.x, scaleCurrent.y, 1);
			else
				img.localScale = new Vector3(scaleOriginal.x, scaleOriginal.y, 1);
		}
	}
	
	public void activateBar(int currentBar, int itemCount)
	{
		_anim.SetInteger("currentBar", currentBar);
		_anim.SetInteger("itemCount", itemCount);
	}
	public void addIcon (Sprite i, int bar)
	{
		if (bar == 0)
		{
			iconlist0.Add(i);
			Image img = (Image) transform.FindChild("Bar"+bar).FindChild("icon"+(_equip.bar0list.Count-1)).gameObject.GetComponent<Image>();
			img.sprite = i;
		}
		else if (bar == 1)
		{
			iconlist1.Add(i);
			Image img = (Image) transform.FindChild("Bar"+bar).FindChild("icon"+(_equip.bar1list.Count-1)).gameObject.GetComponent<Image>();
			img.sprite = i;
		}
	}
}
