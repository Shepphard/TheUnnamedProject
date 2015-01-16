using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour {

	private Speechbubble speech;
	
	void Awake()
	{
		speech = Speechbubble.Instance();
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			speech.Say("Vivien", "teststuff blarblarblar", 3f);
		}
		
		if (Input.GetKeyDown(KeyCode.Z))
		{
			speech.Say("Vivien", "ANDERER TEXT", 3f);
		}
	}
}
