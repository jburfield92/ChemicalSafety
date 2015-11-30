using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class Room5SDSPickup : MonoBehaviour
{
	Vector3 currentPosition;
	Vector3 startPosition;
	Quaternion startRotation;
	public static GameObject[] SDSRoom5Buttons;
	public static GameObject arrow;
	public static GameObject arrow2;
	public static GameObject container;
	public static bool moved3;
	public static bool readyToStore;

	// Use this for initialization
	void Start ()
    {
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;
		container = GameObject.Find("box1");
	}
	
	// Update is called once per frame
	void Update ()
    {
		currentPosition = gameObject.transform.position;

		if (currentPosition != startPosition)
        {
			ShowSideButtons();
		}

        if (!PickupObject.UsingTablet)
        {
            DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSReturn");
        }
	}

	public void returnThis()
    {
		gameObject.transform.position = startPosition;
		gameObject.transform.rotation = startRotation;
		
		Camera.main.GetComponent<MouseLook>().enabled = true;

		if (GameObject.Find("Player").GetComponent<CharacterMotor>()) 
		{
			GameObject.Find("Player").GetComponent<CharacterMotor>().enabled = true;
			GameObject.Find("Player").GetComponent<MouseLook>().enabled = true;
		}

		PickupObject.UsingTablet = false;
		PickupObject.carriedObject.transform.name = " ";
		RemoveSideButtons();
    }
	
	public void whichButton(int buttonNumber)
    {
		DialogueLua.SetVariable ("UserQuizAnswer", buttonNumber);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSchose");
	}
	
	public void GetButtons()
    {
		SDSRoom5Buttons = GameObject.FindGameObjectsWithTag("SDSButton");
	}

	public void ShowSideButtons()
    {
		foreach (GameObject button in SDSRoom5Buttons)
        {
			if(button.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.name == PickupObject.carriedObject.transform.name
			   || button.transform.name == "ReturnSDS")
            {
				button.SetActive(true);
			}
		}
	}
	
	
	public void RemoveSideButtons()
    {
		foreach (GameObject button in SDSRoom5Buttons)
        {
			button.SetActive(false);
		}	
	}

	public void GetPos()
    {
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;
		moved3 = false;
		container = GameObject.Find("box1");		
	}

	public void MoveArrow()
    {
		arrow = GameObject.Find ("arrow6");
		if (!moved3)
        {
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 2.5f, arrow.gameObject.transform.position.z);
			moved3 = true;
		}
        else if (moved3)
        {
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 9.5f, arrow.gameObject.transform.position.z);
			moved3 = false;
		}
	}

	public void MoveArrowStorage()
    {
		arrow2 = GameObject.Find ("arrow7");

		if (!moved3)
        {
			arrow2.gameObject.transform.position = new Vector3 (arrow2.gameObject.transform.position.x, 3f, arrow2.gameObject.transform.position.z);
			moved3 = true;
		}
        else if (moved3)
        {
			arrow2.gameObject.transform.position = new Vector3 (arrow2.gameObject.transform.position.x, 6f, arrow2.gameObject.transform.position.z);
			moved3 = false;
		}
	}

	void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.name == "Player" && this.gameObject.transform.name == "AtStorageTrigger5" && PickupObject.carriedObject.transform.name == "box1")
        {
            DialogueManager.Instance.SendMessage("OnSequencerMessage", "Storage1");
        }

			if(PickupObject.carriedObject == null && readyToStore && other.gameObject.transform.name == "box1")
            {
				other.gameObject.SetActive(false);

				if (arrow2 != null)
                {
					arrow2.SetActive(false);
				}

				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "cont");
			}
		
	}

	public void ReadyToStore()
    {
		readyToStore = true;
	}

	public void MoveBox()
    {
		container.gameObject.transform.position = new Vector3 (container.gameObject.transform.position.x, 1.411f, container.gameObject.transform.position.z);
	}

    public void ChangeIt()
    {
        if (GameObject.Find("Dropdown").GetComponent<Dropdown>().value > 0)
        {
            DialogueLua.SetVariable("UserQuizAnswer", GameObject.Find("Dropdown").GetComponent<Dropdown>().value);
            DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSchose");
            GameObject.Find("Dropdown").GetComponent<Dropdown>().value = 0;
        }
    }
}