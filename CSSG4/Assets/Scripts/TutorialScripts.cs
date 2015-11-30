using PixelCrushers.DialogueSystem;
using UnityEngine;

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

    private CharacterMotor motor;

	private bool startMouseTutorial;
    private bool startExerciseTutorial;
    private bool startTestTutorial;

    private bool mouseTutorialInitial;
    private bool exerciseTutorialInitial;

	public GameObject containerGhost;

    /// <summary> Use this for initialization
    /// </summary>
    void Start()
    {
        mouseTutorialInitial = true;
        exerciseTutorialInitial = true;

        container = GameObject.Find("container");
        player = GameObject.FindGameObjectWithTag("Player");
        table = GameObject.Find("Table");
        tutorialCanvas = GameObject.Find("TutorialStuff");

        motor = (CharacterMotor)player.GetComponent("CharacterMotor");
    }

    /// <summary> Update is called once per frame
    /// </summary>
    void Update()
    {
        if (container != null)
        {
            if (startMouseTutorial)
            {
                if (mouseTutorialInitial)
                {
                    mouseTutorialInitial = false;

                    // teleport user to spot for doing the mouse look part of the tutorial
                    player.transform.rotation = Quaternion.Euler(0, 0, 0);
                    player.transform.position = new Vector3(0, 1.5f, 0);

                    // restrict movement
                    motor.enabled = false;
                }

                tutorialCanvas.transform.position = new Vector3(0, 2.5f, 5f);

                int x = Screen.width / 2;
                int y = Screen.height / 2;
                string objectName = null;

                Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    objectName = hit.transform.gameObject.name;
                }

                if (objectName == "LeftImage")
                {
                    // user looked to the left
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LookedLeft");
				}
                else if (objectName == "RightImage")
                {
                    // user looked to the right
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LookedRight");
                }
                else if (objectName == "TopImage")
                {
                    // user looked up
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LookedUp");
                }
                else if (objectName == "BottomImage")
                {
                    // user looked down
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LookedDown");
                }
            }
            else if (startExerciseTutorial)
            {
                if (exerciseTutorialInitial)
                {
                    // restore movement
                    motor.enabled = true;

                    Destroy(tutorialCanvas);
                    exerciseTutorialInitial = false;
                }

                if (Vector3.Distance(container.transform.position, player.transform.position) < 3)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "NearContainer");
                }
                if (PickupObject.carriedObject != null && PickupObject.carriedObject.name == "container")
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "ContainerPickedUp");
                }
				if (PickupObject.carriedObject != null && PickupObject.carriedObject.name == "container" && Vector3.Distance(table.transform.position, player.transform.position) < 3)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "NearTable");
                }
				if (containerGhost != null && containerGhost.GetComponent<Placing>().Placed == true)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "ContainerPlaced");
                }
            }
        }
        else if (startTestTutorial)
        {
            if (laptopTable == null)
            {
                laptopTable = GameObject.Find("glassTable");
            }
            else
            {
                if (Vector3.Distance(laptopTable.transform.position, player.transform.position) < 4)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "LaptopTableNearby");
                }

                if (TakeLaptopTest.UsingLaptop == true)
                {
                    DialogueManager.Instance.SendMessage("OnSequencerMessage", "TestStarted");
                    testStarted = true;
                }

                if (testStarted)
                {
                    TakeLaptopTest.CanReturn = false;
                }
            }
        }
    }

	void StartMouseTutorial()
	{
        startMouseTutorial = true;
    }

    void StartExerciseTutorial()
    {
		startMouseTutorial = false;
        startExerciseTutorial = true;
    }

    void StartTestTutorial()
    {
        startExerciseTutorial = false;
        startTestTutorial = true;
    }

    void EnableContainerPickupable()
    {
        GameObject.Find("container").GetComponent<Rigidbody>().isKinematic = false;
        GameObject.Find("container").GetComponent<BoxCollider>().enabled = true;
    }

	void EndTutorial()
	{
        SQL.SaveProgress("Tutorial", (int)(100*(float)Test.testScore/Test.maxTestScore));
        Application.LoadLevel("HazardsLoadingScreen");
	}
}