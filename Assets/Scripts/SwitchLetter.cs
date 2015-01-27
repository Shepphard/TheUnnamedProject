using UnityEngine;
using System.Collections;

public class SwitchLetter : MonoBehaviour {

    public Sprite switchOutWith;
    public float swithTimer = 1f;

    private ParticleSystem cloud;
    private SpriteRenderer render;
    private WiggleLetter wiggleScript;

    void Awake()
    {
        cloud = GetComponentInChildren<ParticleSystem>();
        render = GetComponent<SpriteRenderer>();
    }

	void Start ()
    {
        Invoke("SwitchOut", swithTimer);
	}

    void SwitchOut()
    {
        render.sprite = switchOutWith;
        cloud.Play();
    }

    public void TurnOnWiggle()
    {
        wiggleScript.enabled = true;
    }
}
