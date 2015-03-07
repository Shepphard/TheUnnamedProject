using UnityEngine;
using System.Collections;

public class NPCCosma : MonoBehaviour
{
    public AudioClip[] clips;
    public int interactionStatus = 0;
    public GameObject Gun;
    public GameObject Spacetape;

    public GameObject[] repairedStuff;

    private AudioSource aux;
    private Speechbubble speech;
    private string[] text;
    private bool speaking = false;

    private Equipment _equip;
    private Inventory _inv;
    private InteractionControls _ctrl;
    private MusicController musicController;

    private int lastplayedIndex = 0;

    void Awake()
    {
        speech = Speechbubble.Instance();
        aux = audio;
        InitText();

        GameObject player = GameObject.FindGameObjectWithTag(Tags.player);
        _equip = player.GetComponent<Equipment>();
        _inv = player.GetComponent<Inventory>();
        _ctrl = player.GetComponent<InteractionControls>();
        musicController = MusicController.Instance();
    }

    public void Interaction()
    {
        if (speaking)
            return;

        switch (interactionStatus)
        {
            case 0: // first (0)
                StartCoroutine(Talking(0));
                interactionStatus++;
                Invoke("Interaction", 15f);
                Invoke("SpawnToygun", 8f);
                break;
            case 1: // second (1)
                StartCoroutine(Talking(1));
                interactionStatus++;
                break;
            case 2: // collecting parts (2 or 3)
                // carries all items?
                if  (((_ctrl.carriesItem("ship_part1")) || _inv.carriesItem("ship_part1"))
                  && ((_ctrl.carriesItem("ship_part2")) || _inv.carriesItem("ship_part2"))
                  && ((_ctrl.carriesItem("ship_part3")) || _inv.carriesItem("ship_part3")))
                {
                  interactionStatus++;
                  Invoke("Interaction",0.3f);
                }
                else // no? talk stuff
                {
                    if (lastplayedIndex == 0)
                    {
                        StartCoroutine(Talking(2));
                        lastplayedIndex = 2;
                    }
                    else if (lastplayedIndex == 2)
                    {
                        StartCoroutine(Talking(3));
                        lastplayedIndex = 3;
                    }
                    else
                    {
                        StartCoroutine(Talking(2));
                        lastplayedIndex = 2;
                    }
                }
                break;
            case 3: // got all parts: get the tape (4)
                StartCoroutine(Talking(4));
                Invoke("SpawnTape", 6f);
                interactionStatus++;
                break;
            case 4: // DONE! (5)
                bool repaired = true;
                foreach (GameObject obj in repairedStuff)
                {
                    if (!obj.activeSelf)
                        repaired = false;
                }
                if (repaired)
                    StartCoroutine(Talking(5));
                else
                    StartCoroutine(Talking(4));
                break;
            default: Debug.LogError("Interaction Status is not in switch statement"); break;
        }
    }

    void SpawnTape()
    {
        Spacetape.SetActive(true);
    }

    void SpawnToygun()
    {
        Gun.SetActive(true);
    }

    IEnumerator Talking(int index)
    {
        speaking = true;
        float duration = clips[index].length;
        speech.Say("Cosma", text[index], duration);
        aux.PlayOneShot(clips[index]);
        yield return new WaitForSeconds(duration);

        speaking = false;
    }

    void InitText()
    {
        text = new string[clips.Length];
        text[0] = "My Dad told me, that on the moon the gravitation is lower. So you can jump like a bouncy ball! Oh, and take my laser gun, you never know what you might find out there.";
        text[1] = "I am missing three parts: the door, one of the landing legs and ehm... what's it called? a.. thructor? Where could they be?";
        text[2] = "Oh, do you see that?! There is smoke coming up from that platform! Maybe one of the parts fell down there";
        text[3] = "Another part seems to have fallen on top of the crater! You would have to climb up there somehow..";
        text[4] = "Awesome! We got all the parts back together! Now. Take this spacetape and try to put the parts back on the ship!";
        text[5] = "Perfect! Now, let's go! It's still a bit further to the pirate ship, where all of my friends are!";
    }
}
