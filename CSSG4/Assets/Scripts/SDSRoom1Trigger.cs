using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class SDSRoom1Trigger : MonoBehaviour
{
	bool HitTrigger;

	// Use this for initialization
	void Start ()
    {
		HitTrigger = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

	void OnTriggerStay(Collider other)
    {
		if (other.gameObject.transform.name == "Player")
        {
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "tablet");
		}
	}
}
