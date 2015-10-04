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

    private CharacterMotor motor;
    private MouseLook look;

    private int testScore;
    private int testTotalScore = 3;

	private bool startMouseTutorial;
    private bool startExerciseTutorial;
    private bool lookedUp;
	private bool lookedDown;
	private bool lookedRight;
	private bool lookedLeft;

    private bool first;

    /// <summary> Use this for initialization
    /// </summary>
    void Start()
    {
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
      
    }

	void StartMouseTutorial(string boolean)
	{
		startMouseTutorial = bool.Parse (boolean);
    }

    void StartExerciseTutorial(string boolean)
    {
        startExerciseTutorial = bool.Parse(boolean);
    }
}