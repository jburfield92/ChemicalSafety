using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class Room3SDSPickup : MonoBehaviour
{
	Vector3 currentPosition;
	Vector3 startPosition;
	Quaternion startRotation;
	public static GameObject[] SDSRoom3Buttons;
	public static bool moved;

	// Use this for initialization
	void Start ()
    {
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;
		moved = false;
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
		GetButtons ();
	}

	public void whichButton(int buttonNumber)
    {
		DialogueLua.SetVariable ("UserQuizAnswer", buttonNumber);
		DialogueManager.Instance.SendMessage("OnSequencerMessage", "SDSchose");	
	}

	public void GetButtons()
    {
		SDSRoom3Buttons = GameObject.FindGameObjectsWithTag("SDSButton");
	}
	
	public void ShowSideButtons()
    {
		foreach (GameObject button in SDSRoom3Buttons)
        {
			button.SetActive(true);
		}
	}
	
	
	public void RemoveSideButtons()
    {
		foreach (GameObject button in SDSRoom3Buttons)
        {
			button.SetActive(false);
		}
		
	}

	public void MoveArrow()
    {
		GameObject arrow = GameObject.Find ("arrow3");
		if (!moved)
        {
			arrow.gameObject.transform.position = new Vector3 (arrow.gameObject.transform.position.x, 3f, arrow.gameObject.transform.position.z);
			moved = true;
		}
        else if (moved)
        {
			arrow.SetActive(false);
		}
	}

	void GetPos()
    {
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;
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
