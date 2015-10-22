using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class SymbolTrigger : MonoBehaviour {

	private bool trigger;
	public GameObject point;

	// Use this for initialization
	void Start () {
	
		trigger = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (trigger) {
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "trigger");
			point.SetActive(!point.activeSelf);
			trigger = false;
		}
	}

	void TCheck(){
		trigger = true;
		point.SetActive(!point.activeSelf);
	}
}
