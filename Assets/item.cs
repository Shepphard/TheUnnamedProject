using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {

	public Sprite icon;
	public Vector3 positionOffset;
	public Vector3 carriedRotation;
	
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
