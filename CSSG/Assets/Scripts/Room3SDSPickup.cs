using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class Room3SDSPickup : MonoBehaviour {

	Vector3 currentPosition;
	Vector3 startPosition;
	Quaternion startRotation;
	public static GameObject[] SDSRoom3Buttons;
	public static bool moved;

	// Use this for initialization
	void Start () {
		startPosition = this.gameObject.transform.position;
		startRotation = this.gameObject.transform.rotation;
		moved = false;
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
		SDSRoom3Buttons = GameObject.FindGameObjectsWithTag("SDSButton");
	}
	
	
	public void ShowSideButtons(){
		foreach (GameObject button in SDSRoom3Buttons) {
			if(button.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.name == PickupObject.carriedObject.transform.name
			   || button.transform.name == "ReturnSDS"){
				
				button.SetActive(true);
			}
		}
	}
	
	
	public void RemoveSideButtons(){
		foreach (GameObject button in SDSRoom3Buttons) {
			button.SetActive(false);
		}
		
	}

	public void MoveArrow(){
		GameObject arrow = GameObject.Find ("arrow3");
		if (!moved) {
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 3f, arrow.gameObject.transform.position.z);
			moved = true;
		} else if (moved) {
			arrow.SetActive(false);
		}
	}

	void GetPos(){
		startPosition = this.gameObject.transform.position;
		startRotation = this.gameObject.transform.rotation;
	}
}
