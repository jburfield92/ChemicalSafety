using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;


public class Room4SDSPickup : MonoBehaviour {

	Vector3 currentPosition;
	Vector3 startPosition;
	Quaternion startRotation;
	public static GameObject[] SDSRoom4Buttons;
	public static bool moved2;
	
	// Use this for initialization
	void Start () {
		startPosition = this.gameObject.transform.position;
		startRotation = this.gameObject.transform.rotation;
		moved2 = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		currentPosition = this.gameObject.transform.position;
		if (currentPosition != startPosition) {
			ShowSideButtons();
		}
	}

	public void returnThis() {
		this.gameObject.transform.position = startPosition;
		this.gameObject.transform.rotation = startRotation;
		
		GameObject mainCamera = GameObject.FindWithTag ("MainCamera");
		mainCamera.GetComponent<MouseLook>().enabled = true;
		if (GameObject.Find("Player").GetComponent<CharacterMotor>()) 
		{
			GameObject.Find("Player").GetComponent<CharacterMotor>().enabled = true;
			GameObject.Find("Player").GetComponent<MouseLook>().enabled = true;
		}
		PickupObject.UsingTablet = false;
		PickupObject.carriedObject.transform.name=" ";
		GetButtons ();
		RemoveSideButtons ();
		
	}
	
	public void whichButton(int buttonNumber) {
		DialogueLua.SetVariable ("UserQuizAnswer", buttonNumber);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSchose");
		
		
	}
	
	public void GetButtons() {
		SDSRoom4Buttons = GameObject.FindGameObjectsWithTag("SDSButton");
	}
	
	
	public void ShowSideButtons(){
		foreach (GameObject button in SDSRoom4Buttons) {
			if(button.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.name == PickupObject.carriedObject.transform.name
			   || button.transform.name == "ReturnSDS"){
				
				button.SetActive(true);
			}
		}
	}
	
	
	public void RemoveSideButtons(){
		foreach (GameObject button in SDSRoom4Buttons) {
			button.SetActive(false);
		}	
	}

	public void MoveArrow(){
		GameObject arrow = GameObject.Find ("arrow1");
		if (!moved2) {
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 2.5f, arrow.gameObject.transform.position.z);
			moved2 = true;
		} else if (moved2) {
			arrow.SetActive(false);
		}
	}
	
	void GetPos(){
		startPosition = this.gameObject.transform.position;
		startRotation = this.gameObject.transform.rotation;
		moved2 = false;
	}
}
