﻿using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem.UnityGUI;

public class EducationScriptSDS : MonoBehaviour {


	public int answer;
	public static GameObject[] buttons;
	public static GameObject[] centerButton;

    private GameObject uiRoot;
    private GUIControl control;

    public static bool moved;

    // Use this for initialization
    void Start () {
		answer = 0;

        uiRoot = GameObject.Find("GUIRoot");
        control = uiRoot.GetComponent<GUIControl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	// also functions as pressB
	public void NextMessage(){
		answer = 2;
		DialogueLua.SetVariable ("UserQuizAnswer", answer);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "next");
	}

	public void PressA(){
		answer = 1;
		DialogueLua.SetVariable ("UserQuizAnswer", answer);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "next");
	}

	public void PressC(){
		answer = 3;
		DialogueLua.SetVariable ("UserQuizAnswer", answer);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "next");
	}

	public void PressD(){
		answer = 4;
		DialogueLua.SetVariable ("UserQuizAnswer", answer);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "next");
	}

	public void QuizTime(){
		foreach (GameObject button in buttons) {
			button.SetActive(true);
		}

		foreach (GameObject button in centerButton) {
			button.SetActive(false);
		}
	}

	public void RemoveSideButtons(){
		foreach (GameObject button in buttons) {
			button.SetActive(false);
		}

		foreach (GameObject button in centerButton) {
			button.SetActive(true);
		}
	}

	public void GetButtons() {
		centerButton = GameObject.FindGameObjectsWithTag("TabletCenterButton");
		buttons = GameObject.FindGameObjectsWithTag("TabletSideButton");
        moved = false;

	}

    public void MoveDialogue()
    {
        uiRoot = GameObject.Find("GUIRoot");
        control = uiRoot.GetComponent<GUIControl>();

        if (!moved)
        {
            control.scaledRect.y = ScaledValue.FromNormalizedValue(-0.48f);
            control.Refresh();
            moved = true;
        }

        else
        {
            control.scaledRect.y = ScaledValue.FromNormalizedValue(0.15f);
            control.Refresh();
        }
    }
          
}
