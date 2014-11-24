using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCInteraction : MonoBehaviour 
{

	public GameObject dialogueBox;
	
	public float activatedTime = 5.0f;	//time textwindow will stay open(if longer than audioclip)

	private int interactCount;	//counts the "level" of interaction with the player (set discretely not just counting up)
	private bool activated;		//is the textwindow open?
	private float timer = 0f;	//timer to close textwindow

	private float audioEndtime;	//time the audioclip should end

	private Text _dialogueText;	
	private Animator _animator;
	private Inventory _inventory;

	// Use this for initialization
	void Awake () 
	{
		interactCount = 0;
		activated = false;
		_dialogueText = dialogueBox.GetComponentInChildren<Text>();
		_dialogueText.text = "";

		_animator = dialogueBox.GetComponentInParent<Animator>();
		_inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//is the textwindow open?
		if(activated)
		{
			//increase timer to close textwindow
			timer += Time.deltaTime;
		}

		//is the audioclip and the timer over?
		if(!audio.isPlaying && timer > activatedTime)
		{
			//close the textwindow
			activated = false;
			_animator.SetBool("activated", activated);
		}

		//is the audios current time beyond the targettime?
		if(audio.time >= audioEndtime)
			audio.Stop();
	}
	
	public void Interaction()
	{
		//if there is no audio playing (you can only interact when no clip is playing)
		if (!audio.isPlaying)
		{	
			//reset the timer and open the textwindow 
			resetTimer();
			activated = true;
			_animator.SetBool("activated", activated);

			//which "level" of interaction does the player have?
			switch(interactCount)
			{
				//first interaction
				case 0:
					//set the dialogueText
					_dialogueText.text = "STOP!!! \nOnly Knights can enter our kingdom!";
					//set the start and end of the audioclip (in seconds from start of audio)
					playFromTo(0f, 5f);
					//set the next level of interaction
					interactCount = 1;
					break;

				case 1:
					_dialogueText.text = "You're not a Knight!\nCome back when you've got some equipment...";
					playFromTo(6f, 10f);
					interactCount = 2;
					break;
				case 2:
					// does the player have the equipment?
					if(!_inventory.isInvEmpty())
					 {
					  	//...play audioclip and set interactCount to 3
						_dialogueText.text = "Alright! That's better.\nBefore you enter our kingdom, \ncould you please tell me your name?";
						playFromTo(30f, 38f);
						interactCount  = 3;
						break;
						}
					else
					{
						//...choose a random clip from the 4 below and don't change the interactCount
						int rndClip = (int)(Random.value * 4);
						switch(rndClip)
						{
							case 0:
								_dialogueText.text = "You still look like a peasant! ";
								playFromTo(12f, 14f);
								break;
								
							case 1:
								_dialogueText.text = "Did you lose something? ";
								playFromTo(16f, 18f);
								break;
								
							case 2:
								_dialogueText.text = "Haven't you forgotten something? ";
								playFromTo(20f, 23f);
								break;
								
							case 3:
								
								_dialogueText.text = "Oh, come on! It can't be that hard to find some weapons!!";
								playFromTo(24f, 29f);
								break;
						}
						break;	
					}

				case 3:
					_dialogueText.text = "Welcome to our Kingdom Sir Bla!!";
					playFromTo(40f, 43f);
					break;
			
			}
		}
	} 
	
	void OnTriggerEnter()
	{
		
	}

	//reset the textwindow-timer
	void resetTimer()
	{
		timer = 0f;
	}

	//play the audioclip from specified start (from) to end(to) in seconds
	void playFromTo(float from, float to)
	{
		audio.time = from;
		audioEndtime = to;
		audio.Play();
	}
	
}
