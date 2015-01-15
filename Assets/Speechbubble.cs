using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Speechbubble : MonoBehaviour {

	// singleton
	private static Speechbubble speechbubble;
	public static Speechbubble Instance() {
		if (!speechbubble) {
			speechbubble = FindObjectOfType(typeof (Speechbubble)) as Speechbubble;
			if (!speechbubble)
				Debug.LogError("There needs to be one active ModalPanelscript on a GameObject in your scene.");
		}
		
		return speechbubble;
	}
	
	public GameObject speechbubbleObject;
	public Text name;
	public Text field;
	public float fadeSpeed = 1f;
	
	private bool enabling = true;
	private bool disabling = false;
	private Image img;
	
	private static int id = 0;
	
	void Awake()
	{
		img = speechbubbleObject.GetComponent<Image>();
	}
	
	void Update()
	{
		if (enabling)
		{
			disabling = false;
			FadingIn ();
		}
		if (disabling)
		{
			FadingOut ();
		}
	}
	
	/* returns a counter. that works like an ID.
	With every new Say statement, the other one will be overwritten,
	and returns a new ID. Say-er must check if its still their id to
	close the bubble, else they cant! */
	public int Say(string name, string field)
	{
		// turn speechbubble on
		if (speechbubbleObject.activeSelf) // is it already active?
			id++;
		else
			speechbubbleObject.SetActive(true);
		
		this.name.text = name;
		this.field.text = field;
		
		return id;
	}
	
	public void CloseBubble(int id)
	{
		if (this.id == id)
			disabling = true;
	}
	
	void OnEnable()
	{
		enabling = true;
	}
	
	void FadeToSolid()
	{
		img.color = Color.Lerp (img.color, Color.white, fadeSpeed * Time.deltaTime);
		name.color = img.color;
		field.color = img.color;
		Debug.Log ("Fading to Solid!");
	}
	
	void FadeToClear()
	{
		img.color = Color.Lerp (img.color, Color.clear, fadeSpeed * Time.deltaTime);
		name.color = img.color;
		field.color = img.color;
	}
	
	void FadingIn()
	{
		FadeToSolid();
		
		if (img.color.a >= 0.95f)
		{
			enabling = false;
			img.color = Color.white;
			name.color = img.color;
			field.color = img.color;
			Debug.Log("Fading in complete");
		}
	}
	
	void FadingOut()
	{
		FadeToClear();
		
		if (img.color.a <= 0.05f)
		{
			disabling = false;
			img.color = Color.clear;
			name.color = img.color;
			field.color = img.color;
			speechbubbleObject.SetActive(false);
		}
	}
}
