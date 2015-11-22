using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;


public class Room2SDSPickup : MonoBehaviour {

	Vector3 currentPosition;
	Vector3 startPosition;
	Quaternion startRotation;
	public static GameObject[] SDSbuttons;
	public static int containersStored;
	public static bool ReadyToStore;
	public static GameObject arrow;
	bool moved;

	// Use this for initialization
	void Start () {
		startPosition = this.gameObject.transform.position;
		startRotation = this.gameObject.transform.rotation;
		containersStored = 0;
		ReadyToStore = false;
		moved = false;
	}
	
	// Update is called once per frame
	void Update () {



		if (PickupObject.carriedObject.gameObject.transform.name == "container1" && ReadyToStore) {
			if (arrow != null) {
				arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 6f, arrow.gameObject.transform.position.z);
			}
			arrow = GameObject.Find ("arrow1");
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 3f, arrow.gameObject.transform.position.z);
		} 

		else if (PickupObject.carriedObject.gameObject.transform.name == "container2" && ReadyToStore) {
			if (arrow != null) {
				arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 6f, arrow.gameObject.transform.position.z);
			}
			arrow = GameObject.Find ("arrow2");
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 3f, arrow.gameObject.transform.position.z);
		} 

		if (containersStored == 3) {
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "cont");
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "Storage1");
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
		RemoveSideButtons ();

	}

	public void whichButton(int buttonNumber) {
		DialogueLua.SetVariable ("UserQuizAnswer", buttonNumber);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSchose");


	}


	public void GetButtons() {
		SDSbuttons = GameObject.FindGameObjectsWithTag("SDSButton");
	}


	public void ShowSideButtons(){
		foreach (GameObject button in SDSbuttons) {
			if(button.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.name == PickupObject.carriedObject.transform.name
			   || button.transform.name == "ReturnSDS"){

			button.SetActive(true);
			}
		}
	}


	public void RemoveSideButtons(){
		foreach (GameObject button in SDSbuttons) {
			button.SetActive(false);
		}

	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.transform.name == "container1" && this.gameObject.transform.name == "AtStorageTrigger1") {
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "Storage1");
			if(PickupObject.carriedObject == null){
				containersStored++;
				DialogueLua.SetVariable ("UserQuizAnswer", containersStored);
				other.gameObject.SetActive(false);
				if (arrow != null) {
					arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 6f, arrow.gameObject.transform.position.z);
				}
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "cont");
			}
		}
	
		else if (other.gameObject.transform.name == "container2" && this.gameObject.transform.name == "AtStorageTrigger2") {
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "Storage1");
			if(PickupObject.carriedObject == null){
				containersStored++;
				DialogueLua.SetVariable ("UserQuizAnswer", containersStored);
				other.gameObject.SetActive(false);
				if (arrow != null) {
					arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 6f, arrow.gameObject.transform.position.z);
				}
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "cont");
			}
		}
	}

	void OnTriggerExit(Collider other){
		DialogueManager.Instance.SendMessage ("OnSequencerMessage", "cont");
	}

	void StorageTime(){
		ReadyToStore = true;
	}

}
