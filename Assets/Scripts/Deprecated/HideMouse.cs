using UnityEngine;
using System.Collections;

public class HideMouse : MonoBehaviour {

	public bool showMouse = false;

	void Awake()
	{
		Screen.showCursor = showMouse;
	}
}
