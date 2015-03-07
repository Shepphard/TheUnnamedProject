using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour 
{

	private Equipment equip;
	private InteractionControls ctrl;
	private AssetSwitchNew assetSwitcher;

	
	private Speechbubble speech;
	private string[] text;
	private bool speaking = false;

	public GameObject part1, part2, part3;

	// Use this for initialization
	void Awake() 
	{
		speech = Speechbubble.Instance();

		GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
		equip = player.GetComponent<Equipment>();
     	ctrl = player.GetComponent<InteractionControls>();

		assetSwitcher = GetComponent<AssetSwitchNew>();
	}
	
	// Update is called once per frame
	void Update() 
	{
	}

	public void Interaction()
	{
		Debug.Log ("Interact");
		bool[] equipped = {equip.wearsItem("spacetape", 1),
			ctrl.carriesItem("ship_part1"),
			ctrl.carriesItem("ship_part2"),
			ctrl.carriesItem("ship_part3")};

		if(equipped[0])
		{
			if(equipped[1])
		    {
				//repair part 1
				part1.SetActive(true);
				Transform tmp = ctrl.carriedObject;
				ctrl.clearCarriedObject();
				GameObject.Destroy(tmp.gameObject);
				ParticleSystem p = part1.GetComponentInChildren<ParticleSystem>();
				p.Play();
			}
			else if(equipped[2])
			{
				//repair part 2
				part2.SetActive(true);
				Transform tmp = ctrl.carriedObject;
				ctrl.clearCarriedObject();
				GameObject.Destroy(tmp.gameObject);
				ParticleSystem p = part2.GetComponentInChildren<ParticleSystem>();
				p.Play();
			}
			else if(equipped[3])
			{
				//repair part 3
				part3.SetActive(true);
				Transform tmp = ctrl.carriedObject;
				ctrl.clearCarriedObject();
				GameObject.Destroy(tmp.gameObject);
				ParticleSystem p = part3.GetComponentInChildren<ParticleSystem>();
				p.Play();
			}

			else if(!speaking)
			{//say: Where did I put those spaceshipparts...
				//StartCoroutine(Talk());
				Debug.Log("You need a spaceship part for this");
			}
		}
		else
		{
			//"ohno...its broken! :/
		}

	}

	IEnumerator Talk()
	{
		speaking = true;
		
		float duration = 3f;
		speech.Say("Me", "Hm...where did I put those spaceship-parts...", duration);
		
		yield return new WaitForSeconds(duration);
		
		speaking = false;
	}
}
