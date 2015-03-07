using UnityEngine;
using System.Collections;

public class CutsceneScript : MonoBehaviour {

    public Animator[] animators;
    public AudioSource music;
    public GameObject trigger;

    private float musicTime;

    public void StopAll()
    {
        foreach (Animator anim in animators)
        {
            if (anim.gameObject.activeSelf)
                anim.speed = 0;
        }

        musicTime = music.time;
        music.Stop();

        trigger.SetActive(true);
    }

    public void PlayAll()
    {
        foreach (Animator anim in animators)
        {
            if (anim.gameObject.activeSelf)
                anim.speed = 1;
        }

        music.time = musicTime;
        music.Play();

        Destroy(trigger);
    }
}
