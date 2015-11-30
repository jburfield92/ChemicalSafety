using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class Room2SDSPickup : MonoBehaviour
{
	Vector3 currentPosition;
	Vector3 startPosition;
	Quaternion startRotation;
	public static GameObject[] SDSbuttons;
	public static int containersStored;
	public static bool ReadyToStore;
	public static GameObject arrow;
	bool moved;
    public static GameObject containerBox;

	// Use this for initialization
	void Start ()
    {
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;
		containersStored = 0;
		ReadyToStore = false;
		moved = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (PickupObject.carriedObject.gameObject.transform.name == "container1" && ReadyToStore)
        {
			if (arrow != null)
            {
				arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 6f, arrow.gameObject.transform.position.z);
			}

			arrow = GameObject.Find ("arrow1");
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 3f, arrow.gameObject.transform.position.z);
		} 
		else if (PickupObject.carriedObject.gameObject.transform.name == "container2" && ReadyToStore)
        {
			if (arrow != null)
            {
				arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 6f, arrow.gameObject.transform.position.z);
			}

			arrow = GameObject.Find ("arrow2");
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 3f, arrow.gameObject.transform.position.z);
		} 

		if (containersStored == 3)
        {
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "cont");
			DialogueManager.Instance.SendMessage ("OnSequencerMessage", "Storage1");
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
		RemoveSideButtons ();

	}

	public void whichButton(int buttonNumber)
    {
		DialogueLua.SetVariable ("UserQuizAnswer", buttonNumber);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSchose");
	}

	public void GetButtons()
    {
		SDSbuttons = GameObject.FindGameObjectsWithTag("SDSButton");
        containerBox = GameObject.Find("Cube2");
	}

	public void ShowSideButtons()
    {
		foreach (GameObject button in SDSbuttons)
        {
			button.SetActive(true);
		}
	}

	public void RemoveSideButtons()
    {
		foreach (GameObject button in SDSbuttons)
        {
			button.SetActive(false);
		}
	}

	void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.name == "Player" && this.gameObject.transform.name == "AtStorageTrigger1" && PickupObject.carriedObject.transform.name == "container1")
        {
            DialogueManager.Instance.SendMessage("OnSequencerMessage", "Storage1");
        }

        else if (other.gameObject.transform.name == "Player" && this.gameObject.transform.name == "AtStorageTrigger2" && PickupObject.carriedObject.transform.name == "container2")
        {
            DialogueManager.Instance.SendMessage("OnSequencerMessage", "Storage1");
        }

        if (PickupObject.carriedObject == null && other.gameObject.transform.name == "container1" && this.gameObject.transform.name == "AtStorageTrigger1")
        {
            containersStored++;
            DialogueLua.SetVariable("UserQuizAnswer", containersStored);
            other.gameObject.SetActive(false);

            if (arrow != null)
            {
                arrow.gameObject.transform.position = new Vector3(arrow.gameObject.transform.position.x, 6f, arrow.gameObject.transform.position.z);
            }

            DialogueManager.Instance.SendMessage("OnSequencerMessage", "cont");
        }

		else if(PickupObject.carriedObject == null && other.gameObject.transform.name == "container2" && this.gameObject.transform.name == "AtStorageTrigger2")
            {
				containersStored++;
				DialogueLua.SetVariable ("UserQuizAnswer", containersStored);
				other.gameObject.SetActive(false);

				if (arrow != null)
                {
					arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 6f, arrow.gameObject.transform.position.z);
				}

				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "cont");
			}
		
	}

	void OnTriggerExit(Collider other)
    {
		DialogueManager.Instance.SendMessage ("OnSequencerMessage", "cont");
	}

	void StorageTime()
    {
		ReadyToStore = true;
        containerBox.SetActive(false);
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

    public void ChangeIt2()
    {

        if (GameObject.Find("Dropdown2").GetComponent<Dropdown>().value > 0)
        {
            DialogueLua.SetVariable("UserQuizAnswer", GameObject.Find("Dropdown2").GetComponent<Dropdown>().value);
            DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSchose");
            GameObject.Find("Dropdown2").GetComponent<Dropdown>().value = 0;
        }
    }

    public void ChangeIt3()
    {

        if (GameObject.Find("Dropdown3").GetComponent<Dropdown>().value > 0)
        {
            DialogueLua.SetVariable("UserQuizAnswer", GameObject.Find("Dropdown3").GetComponent<Dropdown>().value);
            DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSchose");
            GameObject.Find("Dropdown3").GetComponent<Dropdown>().value = 0;
        }
    }
}