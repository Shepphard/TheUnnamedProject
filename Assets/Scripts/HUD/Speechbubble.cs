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
	public Text txt_name;
	public Text txt_field;
	public float fadeSpeed = 1f;
	
	private bool enabling = false;
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
		
		enabling = true;
		txt_name.text = name;
		txt_field.text = field;
		
		return id;
	}
	
	public void Say(string name, string field, float duration)
	{
		int currentId = Say (name, field);
		StartCoroutine(CloseAfter(duration, currentId));
	}
	
	private IEnumerator CloseAfter(float duration, int currentId)
	{
		yield return new WaitForSeconds(duration);
		CloseBubble(currentId);
	}
	
	public void CloseBubble(int new_id)
	{
		if (id == new_id)
			disabling = true;
	}
	/*
	void OnEnable()
	{
		enabling = true;
	}*/
	
	void FadeToSolid()
	{
		img.color = Color.Lerp (img.color, Color.white, fadeSpeed * Time.deltaTime);
		txt_name.color = img.color;
		txt_field.color = img.color;
		//Debug.Log ("Fading to Solid!");
	}
	
	void FadeToClear()
	{
		img.color = Color.Lerp (img.color, Color.clear, fadeSpeed * Time.deltaTime);
		txt_name.color = img.color;
		txt_field.color = img.color;
	}
	
	void FadingIn()
	{
		FadeToSolid();
		
		if (img.color.a >= 0.95f)
		{
			enabling = false;
			img.color = Color.white;
			txt_name.color = img.color;
			txt_field.color = img.color;
			//Debug.Log("Fading in complete");
		}
	}
	
	void FadingOut()
	{
		FadeToClear();
		
		if (img.color.a <= 0.05f)
		{
			disabling = false;
			img.color = Color.clear;
			txt_name.color = img.color;
			txt_field.color = img.color;
			speechbubbleObject.SetActive(false);
		}
	}
}
