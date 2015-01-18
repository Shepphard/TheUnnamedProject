using UnityEngine;
using System.Collections;

public class Talking : MonoBehaviour
{
	public float commentDuration = 3.5f;

	private Speechbubble speech;
	private Inventory inv;
	private Equipment equip;
	private InteractionControls ctrl;
	
	void Awake()
	{
		speech = Speechbubble.Instance();
		inv = GetComponent<Inventory>();
		equip = GetComponent<Equipment>();
		ctrl = GetComponent<InteractionControls>();
	}
	
	public void CommentOnItem()
	{
		Debug.Log ("Player comments on item");
		string s = "Maybe I could use this ";
		if (ctrl.carriedObject != null)
		{
			item i = ctrl.carriedObject.GetComponent<item>();
			s += nameMatcher(i.itemName);
		}
		speech.Say("Me", s, commentDuration);
	}
	
	string nameMatcher(string s)
	{
		string result = "";
		switch(s)
		{
		case "branch": result = "branch... as a sword! Heck, it's sharp!"; break;
		case "smallbranch": result = "branch... as a little weapon! En garde!"; break;
		case "lid": result = "lid as.... a shield! To protect me from.. stuff"; break;
		case "pot": result = "pot... yes! As a shiny helmet! With leftover spaghetti"; break;
		default: result = "nothing, apparently"; break;
		}
		
		return result;
	}
}
