using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {

	public Sprite icon;
	private AssetSwitchNew _assetS;
	
	private bool isInInv = false;
	
	public void setIsInInv(bool b)
	{
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
