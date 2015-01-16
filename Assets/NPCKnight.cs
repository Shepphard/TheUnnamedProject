using UnityEngine;
using System.Collections;

public class NPCKnight : MonoBehaviour
{
	public AudioClip[] clips;
	public Animator animWall;
	public int interactionStatus = 0;
	
	private AudioSource aux;
	private Speechbubble speech;
	private string[] text;
	private bool speaking = false;
	
	void Awake()
	{
		speech = Speechbubble.Instance();
		aux = audio;
		InitText();
	}
	
	public void Interaction()
	{
		if (speaking)
			return;
	
		switch(interactionStatus)
		{
		case 0:
			StartCoroutine(Talking (0));
			Invoke("riseWall", 7f);
			break;
		case 1:
			StartCoroutine(Talking(1));
			interactionStatus++;
			break;
		case 2:
			// talk stuff till all the equipment is there
		default: StartCoroutine(Talking (7)); break;
		}
	}
	
	/* functions just for help */
	
	void riseWall()
	{
		MusicController.Instance().PlaySFX(0);
		animWall.SetTrigger("Play");
		interactionStatus++;
		Invoke ("Interaction", 11f);
	}
	
	IEnumerator Talking(int index)
	{
		speaking = true;
		float duration = clips[index].length;
		speech.Say("Knight Raphi", text[index], duration);
		aux.PlayOneShot(clips[index]);
		yield return new WaitForSeconds(duration);
		
		speaking = false;
	}
	
	void InitText()
	{
		text = new string[clips.Length];
		text[0] = "Willkommen buddy, darfst hier leider net rein, sowwy. Jungs, Tore hoch!";
		text[1] = "Nur richtige Ritter haben Zutritt! Also rüste dich aus wie einer!";
		text[2] = "Du brauchst noch nen Helm!";
		text[3] = "Du brauchst ein Schild!";
		text[4] = "Du brauchst ein Schwert";
		text[5] = "Sagmal, mit so einem kleinen Schwert kommst du nirgends rein!";
		text[6] = "Willkommen, wie ist denn ihr Name?";
		text[7] = "Treten Sie ein, Sir bla!";
	}
}