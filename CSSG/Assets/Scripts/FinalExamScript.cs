using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class FinalExamScript : MonoBehaviour
{
    public GameObject Screen;

    private GameObject player;
    private GameObject laptopTable;
    private bool testStarted;
    private bool testCompleted;
    public Sprite symbol;
    public Sprite symbol2;
    public Sprite symbol3;
    public Sprite symbol4;
    public Sprite symbol5;
    public Sprite symbol6;
    public Sprite symbol7;
    public Sprite symbol8;
    public Sprite symbol9;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Screen.GetComponent<Image>().enabled = false;
        ((CharacterMotor)player.GetComponent("CharacterMotor")).enabled = false;
    }

    // Update is called once per frame
    void Update()
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

    void EndFinal()
    {
        SQL.SaveProgress("FinalExam", (int)(100 * (float)Test.testScore / Test.maxTestScore));
        Application.LoadLevel("MainMenu");
    }

    void ShowSymbol(string imageName)
    {
        if (imageName == "none")
        {
            Screen.GetComponent<Image>().enabled = false;
        }
        else if (imageName == "symbol")
        {
            Screen.GetComponent<Image>().sprite = symbol;
            Screen.GetComponent<Image>().enabled = true;
        }
        else if (imageName == "symbol2")
        {
            Screen.GetComponent<Image>().sprite = symbol2;
            Screen.GetComponent<Image>().enabled = true;
        }
        else if (imageName == "symbol3")
        {
            Screen.GetComponent<Image>().sprite = symbol3;
            Screen.GetComponent<Image>().enabled = true;
        }
        else if (imageName == "symbol4")
        {
            Screen.GetComponent<Image>().sprite = symbol4;
            Screen.GetComponent<Image>().enabled = true;
        }
        else if (imageName == "symbol5")
        {
            Screen.GetComponent<Image>().sprite = symbol5;
            Screen.GetComponent<Image>().enabled = true;
        }
        else if (imageName == "symbol6")
        {
            Screen.GetComponent<Image>().sprite = symbol6;
            Screen.GetComponent<Image>().enabled = true;
        }
        else if (imageName == "symbol7")
        {
            Screen.GetComponent<Image>().sprite = symbol7;
            Screen.GetComponent<Image>().enabled = true;
        }
        else if (imageName == "symbol8")
        {
            Screen.GetComponent<Image>().sprite = symbol8;
            Screen.GetComponent<Image>().enabled = true;
        }
        else if (imageName == "symbol9")
        {
            Screen.GetComponent<Image>().sprite = symbol9;
            Screen.GetComponent<Image>().enabled = true;
        }
    }

    void EndReview()
    {
        ((CharacterMotor)player.GetComponent("CharacterMotor")).enabled = true;
    }
}
