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
    private GameObject tutorialCanvas;

    private int testScore;
    private int testTotalScore = 3;

	private bool startMouseTutorial;
    private bool startExerciseTutorial;
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
        table = GameObject.Find("Table");
        tutorialCanvas = GameObject.Find("TutorialStuff");
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update()
    {
        if (container != null)
        {
            if (startMouseTutorial)
            {
                tutorialCanvas.transform.position = new Vector3(0, 2.5f, 6f);

                int x = Screen.width / 2;
                int y = Screen.height / 2;
                string objectName = null;

                Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    objectName = hit.transform.gameObject.name;
                }

                if (!lookedLeft && objectName == "LeftImage")
                {
                    // user looked to the left
                    lookedLeft = true;
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LookedLeft");
                }
                else if (lookedLeft && !lookedRight && objectName == "RightImage")
                {
                    // user looked to the right
                    lookedRight = true;
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LookedRight");
                }
                else if (lookedRight && !lookedUp && objectName == "TopImage")
                {
                    // user looked up
                    lookedUp = true;
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LookedUp");
                }
                else if (lookedUp && !lookedDown && objectName == "BottomImage")
                {
                    // user looked down
                    lookedDown= true;
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LookedDown");
                    startMouseTutorial = false;
                }
            }
            else if (startExerciseTutorial)
            {
                Debug.Log("TEST");
                tutorialCanvas.SetActive(false);
                if (!containerNearby && Vector3.Distance(container.transform.position, player.transform.position) < 2)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "NearContainer");
                    containerNearby = true;
                }
                else if (!containerPickup && PickupObject.carriedObject != null)
                {
                    if (PickupObject.carriedObject.name == "container")
                    {
                        DialogueManager.Instance.SendMessage("OnSequencerMessage", "ContainerPickedUp");
                        containerPickup = true;
                    }
                }
                else if (!tableNearby && Vector3.Distance(table.transform.position, player.transform.position) < 3)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "NearTable");
                    tableNearby = true;
                }
                else if (tableNearby && !containerPlaced)
                {
                    if (container.transform.position.x > 2.5f && container.transform.position.x < 5.5f && container.transform.position.z > 3.5f && container.transform.position.z < 5.5f && container.transform.position.y < 1.5f)
                    {
                        DialogueManager.Instance.SendMessage("OnSequencerMessage", "ContainerPlaced");
                        containerPlaced = true;
                    }
                }
            }
        }
        else
        {
            if (containerPlaced && laptopTable == null)
            {
                laptopTable = GameObject.Find("glassTable");
            }
            else
            {
                if (!laptopTableNearby && Vector3.Distance(laptopTable.transform.position, player.transform.position) < 2)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LaptopTableNearby");
                    laptopTableNearby = true;
                }

                if (!testStarted && TakeLaptopTest.UsingLaptop == true)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "TestStarted");
                    testStarted = true;
                }
            }
        }
    }

	void StartMouseTutorial(string boolean)
	{
		startMouseTutorial = bool.Parse (boolean);
    }

    void StartExerciseTutorial(string boolean)
    {
        startExerciseTutorial = bool.Parse(boolean);
        Debug.Log(startExerciseTutorial);
    }
}
