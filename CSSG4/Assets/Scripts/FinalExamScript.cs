using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;

public class FinalExamScript : MonoBehaviour
{
    public GameObject Screen;
    public GameObject ScreenText;

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
        Camera.main.GetComponent<MouseLook>().enabled = true;
        PickupObject.canRun = true;
        TakeLaptopTest.UsingLaptop = false;

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
            ScreenText.GetComponent<Text>().enabled = false;
        }
        else if (imageName == "symbol")
        {
            Screen.GetComponent<Image>().sprite = symbol;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Flame Over Circle\n\nWhich represents oxidizers";
        }
        else if (imageName == "symbol2")
        {
            Screen.GetComponent<Image>().sprite = symbol2;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Flame\n\nWhich is used to mark flammables, self-reactives, pyrophorics, self-heating substances, substances that emit flammable gases and organic peroxides";
        }
        else if (imageName == "symbol3")
        {
            Screen.GetComponent<Image>().sprite = symbol3;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Exploding Bomb\n\nWhich is used to signify explosives, self-reactives and organic peroxides";
        }
        else if (imageName == "symbol4")
        {
            Screen.GetComponent<Image>().sprite = symbol4;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Skull and Crossbones\n\nWhich identifies acutely toxic or fatal substances";
        }
        else if (imageName == "symbol5")
        {
            Screen.GetComponent<Image>().sprite = symbol5;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Corrosion\n\nWhich is used to signify corrosives, substances that are skin corrosives, can cause skin burns or substances that can damage the eye";
        }
        else if (imageName == "symbol6")
        {
            Screen.GetComponent<Image>().sprite = symbol6;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Gas cylinder\n\nWhich indicates gases under pressure";
        }
        else if (imageName == "symbol7")
        {
            Screen.GetComponent<Image>().sprite = symbol7;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Health Hazard\n\nWhich is used to denote carcinogens, respiratory sensitizers, reproductive toxins, target organ toxins, mutagens and aspiration toxins";
        }
        else if (imageName == "symbol9")
        {
            Screen.GetComponent<Image>().sprite = symbol8;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Environmental\n\nWhich denotes substances that are environment toxins";
        }
        else if (imageName == "symbol8")
        {
            Screen.GetComponent<Image>().sprite = symbol9;
            Screen.GetComponent<Image>().enabled = true;
            ScreenText.GetComponent<Text>().enabled = true;
            ScreenText.GetComponent<Text>().text = "Exclamation Mark\n\nWhich is used to signify irritants, skin sensitizers, acute toxins, chemicals with narcotic effects and respiratory tract irritants";
        }
    }

    void EndReview()
    {
        ((CharacterMotor)player.GetComponent("CharacterMotor")).enabled = true;
    }
}
