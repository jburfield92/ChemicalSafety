using UnityEngine;
using PixelCrushers.DialogueSystem;

public class TutorialScripts : MonoBehaviour
{
    private GameObject player;

    // first room bools and objects
    private bool containerPickup;
    private bool containerNearby;
    private bool containerPlaced;
    private bool tableNearby;
	private bool laptopTableNearby;
	private bool testStarted;
	private bool testCompleted;

    private GameObject container;
    private GameObject table;

	private GameObject laptopTable;

    private int testScore;
    private int testTotalScore = 3;

	private bool startMouseTutorial;
	private bool lookedUp;
	private bool lookedDown;
	private bool lookedRight;
	private bool lookedLeft;


    /// <summary> Use this for initialization
    /// </summary>
    void Start()
    {
        container = GameObject.Find("container");
        player = GameObject.FindGameObjectWithTag("Player");
        table = GameObject.FindGameObjectWithTag("Table");
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update()
    {
		if (container != null) 
		{
			if (startMouseTutorial)
			{
				if (!lookedLeft)
				{
					// user looked to the left
					if (Input.GetAxis ("Mouse X") < 0)
					{
						lookedLeft = true;
						DialogueManager.Instance.SendMessage ("OnSequencerMessage", "LookedLeft");
					}
				}
				else if (lookedLeft && !lookedRight)
				{
					// user looked to the right
					if (Input.GetAxis ("Mouse X") > 0)
					{
						lookedRight = true;
						DialogueManager.Instance.SendMessage ("OnSequencerMessage", "LookedRight");
					}
				}
				else if (lookedRight && !lookedUp)
				{
					// user looked up
					if (Input.GetAxis ("Mouse Y") > 0)
					{
						lookedUp = true;
						DialogueManager.Instance.SendMessage ("OnSequencerMessage", "LookedUp");
					}
				}
				else if (lookedUp && !lookedDown)
				{
					// user looked up
					if (Input.GetAxis ("Mouse Y") < 0)
					{
						lookedUp = true;
						DialogueManager.Instance.SendMessage ("OnSequencerMessage", "LookedDown");
					}
				}
			}
			if (!containerNearby && Vector3.Distance (container.transform.position, player.transform.position) < 2) {
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "NearContainer");
				containerNearby = true;
			}

			if (!containerPickup && PickupObject.carriedObject != null) {
				if (PickupObject.carriedObject.tag == "FireExtinguisher") {
					DialogueManager.Instance.SendMessage ("OnSequencerMessage", "ContainerPickedUp");
					containerPickup = true;
				}
			}

			if (!tableNearby && Vector3.Distance (table.transform.position, player.transform.position) < 3) {
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "NearTable");
				tableNearby = true;
			}

			if (tableNearby && !containerPlaced) {
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "ContainerPlaced");
				containerPlaced = true;
			}
		}

		if (containerPlaced && laptopTable == null) {
			laptopTable = GameObject.Find ("glassTable");
		} 
		else 
		{
			if (!laptopTableNearby && Vector3.Distance (laptopTable.transform.position, player.transform.position) < 2)
			{
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "LaptopTableNearby");
				laptopTableNearby = true;
			}

			if (!testStarted && TakeLaptopTest.UsingLaptop == true)
			{
				DialogueManager.Instance.SendMessage ("OnSequencerMessage", "TestStarted");
                testStarted = true;
			}
		}
    }

	void StartMouseTutorial(string boolean)
	{
		startMouseTutorial = bool.Parse (boolean);
	}
}
