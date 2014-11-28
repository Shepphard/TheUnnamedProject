﻿using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {

	public Sprite icon;
	public Vector3 positionOffsetInv;
	public Vector3 carriedRotationInv;
	public Vector3 positionOffsetEquip;
	public Vector3 rotationOffsetEquip;
	public bool isEmpty = false;
	public bool isEquipment = false;
	public int belongsToEquipmentBar = 0;
	
	private AssetSwitchNew _assetS;
	private bool isInInv = false;
	
	public void setIsInInv(bool b)
	{
		if (_assetS != null)
			_assetS.setBlockSwitching(b);
		isInInv = b;
	}
	
	public bool getIsInInv()
	{
		return isInInv;
	}
	
	public void setAssetSwitcher( AssetSwitchNew a)
	{
		_assetS = a;
	}
}
