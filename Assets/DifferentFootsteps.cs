﻿using UnityEngine;
using System.Collections;

public class DifferentFootsteps : MonoBehaviour
{

    public AudioClip[] stoneSounds;
    public AudioClip stoneJump;
    public AudioClip stoneLand;
    public AudioClip[] grassSounds;
    public AudioClip grassJump;
    public AudioClip grassLand;
    public AudioClip[] woodSounds;
    public AudioClip woodJump;
    public AudioClip woodLand;
    public AudioClip[] moonSounds;
    public AudioClip moonJump;
    public AudioClip moonLand;

    private FirstPersonHeadBob headbobScript;
    private string lastSounds = "";

    void Awake()
    {
        headbobScript = GetComponent<FirstPersonHeadBob>();
    }

    void FixedUpdate()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            string materialName;

            // assigning material name to the string
            if (hit.collider.renderer == null)
            {
                Debug.Log("Walking on Terrain!");
                materialName = "Terrain";
            }
            else
            {
                materialName = hit.collider.renderer.material.name;
            }

            switch (materialName)
            {
                // grass sounds
                case "ToonGround (Instance)":
                    if (lastSounds != "grass")
                    {
                        headbobScript.SwitchFootstepsArray(grassSounds, grassLand, grassJump);
                        Debug.Log("Switched to grassSounds");
                    }
                    lastSounds = "grass";
                    break;
                // stone sounds
                case "cave (Instance)":
                case "Terrain":
                case "1026x1026_stone_crack_texture_darker (Instance)":
                    if (lastSounds != "stone")
                    {
                        headbobScript.SwitchFootstepsArray(stoneSounds, stoneLand, stoneJump);
                        Debug.Log("Switched to stoneSounds");
                    }
                    lastSounds = "stone";
                    break;
                // wood sounds
                case "log (Instance)":
                case "box (Instance)":
                    if (lastSounds != "wood")
                    {
                        headbobScript.SwitchFootstepsArray(woodSounds, woodLand, woodJump);
                        Debug.Log("Switched to woodSounds");
                    }
                    lastSounds = "wood";
                    break;
                case "moon_ground (Instance)":
                    if (lastSounds != "moon")
                    {
                        headbobScript.SwitchFootstepsArray(moonSounds, moonLand, moonJump);
                        Debug.Log("Switched to moonSounds");
                    }
                    lastSounds = "moon";
                    break;
                default:
                    if (lastSounds == materialName)
                    {
                        Debug.LogError("Other ground detected, named: " + materialName + "\nPlease add sounds for this case");
                    }
                    lastSounds = materialName;
                    break;
            }

        }
    }
}