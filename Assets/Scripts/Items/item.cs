using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class item : MonoBehaviour {

	public Sprite icon;
	public string item_type;
	public Vector3 positionOffsetInv;
	public Vector3 carriedRotationInv;
	public Vector3 positionOffsetEquip;
	public Vector3 rotationOffsetEquip;
	public float scaleFactorWhenCarried = 0.7f;
	public float scaleFactorWhenEquipped = 0.7f;
	public bool isEmpty = false;
	public bool isEquipment = false;
	public int belongsToEquipmentBar = 0;
	public string itemName; 
	
	public bool TriggerLookatHand = false;
	static public Dictionary<string, bool> TriggerDict = new Dictionary<string, bool>();
	
	private AssetSwitchNew _assetS;
	private bool isInInv = false;
	
	public void setIsInInv(bool b)
	{
		if (_assetS != null)
			_assetS.setBlockSwitching(b);
		isInInv = b;
		
		// Makes sure that only one dagger of allthe branches gets swapped
		if (!TriggerDict.ContainsKey(itemName))
			TriggerDict.Add(itemName, false);
	}
	
	public bool getIsInInv()
	{
		return isInInv;
	}
	
	public void setAssetSwitcher( AssetSwitchNew a)
	{
		_assetS = a;
	}
	
	public void Switch()
	{
		_assetS.Switch();
	}
}
