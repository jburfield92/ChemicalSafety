﻿using UnityEngine;
<<<<<<< HEAD
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;
=======
using System.Collections;
>>>>>>> David

public class PauseMenu : MonoBehaviour
{
	public GameObject onOff;
	public GameObject itembar;
	public GameObject settings;
<<<<<<< HEAD
    private GameObject npc;
    private GUIRoot guiRoot;
    private AudioSource source;
    private GUILabel subtitles;
=======
>>>>>>> David

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
<<<<<<< HEAD
        npc = GameObject.FindGameObjectWithTag("NPC");
        source = (AudioSource)npc.GetComponent("AudioSource");
    }
    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    { 
        if (Input.GetKeyUp(KeyCode.Escape) && ((onOff.activeSelf)|| Items.canRun))
        {
            onOff.SetActive(!onOff.activeSelf);
		
			itembar.SetActive(!itembar.activeSelf);
			Items.canRun = !Items.canRun;
		}

        if (Items.canRun)
        {
            if (!source.isPlaying)
            {
                source.UnPause();
            }

            if (guiRoot != null)
            {
                guiRoot.visible = true;
            }
            else
            {
                guiRoot = DialogueManager.DisplaySettings.dialogueUI.GetComponentInChildren<GUIRoot>();
            }

            if (DialogueManager.IsConversationActive)
            {
                DialogueManager.Unpause();
            }

            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1.0f;
            }
        }
        else
        {
            if (source.isPlaying)
            {
                source.Pause();
            }

            if (guiRoot != null)
            {
                guiRoot.visible = false;
            }
            else
            {
                guiRoot = DialogueManager.DisplaySettings.dialogueUI.GetComponentInChildren<GUIRoot>();
            }

            if (DialogueManager.IsConversationActive)
            {
                DialogueManager.Pause();
            }

            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0f;
            }
=======

	}
    /// <summary> Update is called once per frame
    /// </summary>
    void Update ()
    {
		if(Input.GetKeyUp(KeyCode.Escape) && ((onOff.activeSelf)|| Items.canRun))
        {
			onOff.SetActive(!onOff.activeSelf);
		
			itembar.SetActive(!itembar.activeSelf);
			Items.canRun = ! Items.canRun;

		}

		if (Items.canRun)
        {
			Time.timeScale = 1.0f;
		}
        else
        {
			Time.timeScale = 0f;
>>>>>>> David
		}
	}

    /// <summary> handles switching our running state
    /// </summary>
	public void Running()
    {
		Items.canRun = !Items.canRun;
	}
}
