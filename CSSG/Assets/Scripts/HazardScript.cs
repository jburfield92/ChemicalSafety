﻿using PixelCrushers.DialogueSystem;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    private GameObject player;
    private GameObject laptopTable;
    private bool laptopTableNearby;
    private bool testStarted;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (laptopTable == null)
        {
            laptopTable = GameObject.Find("glassTable");
        }
        else
        {
            if (Vector3.Distance(laptopTable.transform.position, player.transform.position) < 4)
            {
                DialogueManager.Instance.SendMessage("OnSequencerMessage", "LaptopTableNearby");
            }

            if (TakeLaptopTest.UsingLaptop == true)
            {
                DialogueManager.Instance.SendMessage("OnSequencerMessage", "TestStarted");
                testStarted = true;
            }

            if (testStarted)
            {
                TakeLaptopTest.CanReturn = false;
            }
        }
    }

    void EndHazards()
    {
        SQL.SaveProgress("Hazard", (int)(100 * (float)Test.testScore / Test.maxTestScore));
        Application.LoadLevel("SDSLoadingScreen");
    }
}
