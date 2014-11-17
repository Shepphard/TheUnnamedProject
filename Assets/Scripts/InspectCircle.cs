using UnityEngine;
using System.Collections;

public class InspectCircle : MonoBehaviour {
	
	public InventoryBar _invBar;
	
	Color originalColor;
	
	void Awake()
	{
		originalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
		renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, _invBar.alphaInspectCircle);
	}
}
