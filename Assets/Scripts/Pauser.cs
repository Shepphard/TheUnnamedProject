﻿using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour
{
    public KeyCode key_pause;
    public GameObject pausePanel;
    public float musicFadeSpeed = 3f;
    public GameObject equipmenuCanvas;

    private InteractionControls playerCtrl;
    private BlockCTRL blocker;
    private MusicController music;

    private bool paused = false;
    private NewEquipMenu equipmenu;
    private bool equipWasEnabled = false;

    void Awake()
    {
        playerCtrl = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<InteractionControls>();
        equipmenu = GetComponent<NewEquipMenu>();
        blocker = playerCtrl.GetComponent<BlockCTRL>();
        music = MusicController.Instance();
    }

    void Update()
    {
        PauseInput();

        music.PauseUnpauseMusic(paused, musicFadeSpeed);
    }

    void PauseInput()
    {
        if (Input.GetKeyDown(key_pause))
        {
            if (pausePanel.activeSelf)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        if (equipmenuCanvas.activeSelf)
        {
            equipWasEnabled = true;
            equipmenu.Disable();
            equipmenu.enabled = false;
        }
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        playerCtrl.enabled = false;
        paused = true;
    }

    void Unpause()
    {
        if (equipWasEnabled)
        {
            equipmenu.enabled = true;
            equipmenu.Enable();
            equipWasEnabled = false;
        }
        playerCtrl.enabled = true;
        Time.timeScale = 1;
        paused = false;
        pausePanel.SetActive(false);
    }
}