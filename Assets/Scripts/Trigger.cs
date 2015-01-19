using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{
	public string triggerName;
	
	private Speechbubble speech;
	
	private bool triggered = false;
	
	void Awake()
	{
		speech = Speechbubble.Instance();
	}
	
	public void Triggered()
	{
		if (triggered)
			return;
	
		switch(triggerName)
		{
		case "pot":
			speech.Say("Me", "Oh, it's a pot! This could be my very knighty helmet!", 2f);
			GetComponent<item>().Invoke("Switch", 1.3f);
			break;
		case "lid":
			speech.Say ("Me", "Sweet, this is the perfect shield!!!", 2f);
			GetComponent<item>().Invoke ("Switch", 1.1f);
			break;
		default: Debug.LogError("This trigger has no function"); break;
		}
		
		triggered = true;
	}
	
	
}
