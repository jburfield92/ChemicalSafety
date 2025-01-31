﻿using UnityEngine;
using PixelCrushers.DialogueSystem;

public class TutorialScripts : MonoBehaviour
{
    private GameObject player;

    // first room bools and objects
    private bool fireExtinguisherPickedUp;
    private bool fireExtinguisherNearBy;
    private bool fireExtinguished;
    private bool fireBarrelNearby;
    private GameObject fireExtinguisher;
    private GameObject fireBarrel;

    private bool testStarted;
    private bool testCompleted;
    private int testScore;
    private int testTotalScore = 3;

    /// <summary> Use this for initialization
    /// </summary>
    void Start()
    {
        fireExtinguisher = GameObject.FindGameObjectWithTag("FireExtinguisher");
        player = GameObject.FindGameObjectWithTag("Player");
        fireBarrel = GameObject.FindGameObjectWithTag("Fire");
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update()
    {
		if (fireExtinguisher != null) {
			if (!fireExtinguisherNearBy && Vector3.Distance (fireExtinguisher.transform.position, player.transform.position) < 2) {
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "NearFireExtinguisher");
				fireExtinguisherNearBy = true;
			}

			if (!fireExtinguisherPickedUp && PickupObject.carriedObject != null) {
				if (PickupObject.carriedObject.tag == "FireExtinguisher") {
					DialogueManager.Instance.SendMessage ("OnSequencerMessage", "FireExtinguisherPickedUp");
					fireExtinguisherPickedUp = true;
				}
			}

			if (!fireBarrelNearby && Vector3.Distance (fireBarrel.transform.position, player.transform.position) < 4f) {
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "NearFireBarrel");
				fireBarrelNearby = true;
			}

			if (fireBarrelNearby == true && !fireExtinguished && !PutOutFire.IsActive) {
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "FireExtinguished");
				fireExtinguished = true;
			}
		}
    }
}
