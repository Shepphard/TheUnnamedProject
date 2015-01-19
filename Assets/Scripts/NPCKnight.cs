using UnityEngine;
using System.Collections;

public class NPCKnight : MonoBehaviour
{
	public AudioClip[] clips;
	public Animator animWall;
	public int interactionStatus = -1;
	
	private AudioSource aux;
	private Speechbubble speech;
	private string[] text;
	private bool speaking = false;
	
	private Equipment _equip;
	private InteractionControls _ctrl;
	private AssetSwitchNew _assetSwitcher;
	private MusicController musicController;
	
	void Awake()
	{
		speech = Speechbubble.Instance();
		aux = audio;
		InitText();
		
		GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
		_equip = player.GetComponent<Equipment>();
		_ctrl = player.GetComponent<InteractionControls>();
		_assetSwitcher = GetComponent<AssetSwitchNew>();
		musicController = MusicController.Instance();
	}
	
	public void Interaction()
	{
		if (speaking)
			return;
	
		switch(interactionStatus)
		{
		case -1:
			// introducing himself, then switching to knight
			StartCoroutine(Talking (8));
			_assetSwitcher.Invoke("Switch", 3f);
			interactionStatus++;
			Invoke("Interaction", clips[8].length+0.1f);
			break;
		case 0:
			StartCoroutine(Talking (0));
			Invoke("riseWall", 5f);
			break;
		case 1:
			StartCoroutine(Talking(1));
			interactionStatus++;
			break;
		case 2:
			// talk stuff till all the equipment is there
			bool[] equipped = {_equip.wearsItem("helmet", 0),
							   _equip.wearsItem("shield", 1),
						       _ctrl.carriesItem("dagger"),
				               _ctrl.carriesItem("sword")};
			// does he have everything?
			bool next = (equipped[0] && equipped[1] && equipped[3]);
			if (next) {
				// you have all the equiptment, should ask for name
				StartCoroutine(Talking(6));
				interactionStatus++;
			}
			// at least one is missing
			else
			{
				int[] possible = new int[3];
				int count = 0;
				if (!equipped[0]) {
					possible[count] = 2;
					count++;
				}
				if (!equipped[1]) {
					possible[count] = 3;
					count++;
				}
				if (!equipped[3]) {
					if (!equipped[2]) {
						possible[count] = 4;
						count++;
					}
					else {
						possible[count] = 5;
						count++;
					}
				}
				
				int picked = Random.Range(0, count);
				StartCoroutine(Talking(possible[picked]));
				}
			break;
		case 3: 
			// now lets you in
			StartCoroutine(Talking(7));
			Invoke ("openWall", 2f);
			musicController.PlaySFX(1);
			break;
		default: Debug.LogError("Interaction Status is not in switch statement"); break;
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
	
	void openWall()
	{
		animWall.SetTrigger("Open");
		animWall.collider.enabled = false;
	}
	
	IEnumerator Talking(int index)
	{
		speaking = true;
		float duration = clips[index].length;
		speech.Say("Sir Parcifoul", text[index], duration);
		aux.PlayOneShot(clips[index]);
		yield return new WaitForSeconds(duration);
		
		speaking = false;
	}
	
	void InitText()
	{
		text = new string[clips.Length];
		text[8] = "Hey, I’m Sir Parcifoul and I’m the guard on this playground…. I mean, kingdom!";
		text[0] = "*Gasp* you’re not a knight! CLOSE THE GATES!!!!";
		text[1] = "Only real knights can enter our kingdom! Go get some armour!";
		text[2] = "You need a helmet!";
		text[3] = "Where’s your shield? You need to defend yourself!";
		text[4] = "Knights use swords you know? Go get one!!";
		text[5] = "Haha, that tiny thing is useless! Get a SWORD, crybaby!";
		text[6] = "That’s better! You can enter our play…. kingdom now! Oh wait, what’s your name?";
		text[7] = "Open the gates! King Lancafew awaits his youngest knight!";
	}
}