using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using PixelCrushers.DialogueSystem;

public class Test : MonoBehaviour 
{
	public GameObject testPanel;
	public TextAsset testFile;
	public GameObject submitButton;

	private string text;
    private List<Question> questions;
	private static List<GameObject> buttonGroup;
	private static List<GameObject> selectedAnswerButtons;

    /// <summary> Use this for initialization
    /// </summary>
    void Start () 
	{
		buttonGroup = new List<GameObject> ();
		selectedAnswerButtons = new List<GameObject> ();
		text = testFile.text;
        questions = new List<Question>();

        // the +=2 is for skipping the new line at the end of each question/answer set
		for (int i = 0; i < text.Length; i+=2) 
		{
            StringBuilder newQuestion = new StringBuilder();
            List<string> newAnswers = new List<string>();
            int newCorrectAnswer;

			while (text[i] != '{')
            {
                newQuestion.Append(text[i]);
                i++;
            }

            // skip the '{' and the extra space
            i += 2;

            while (text[i] != '}')
            {
                StringBuilder answer = new StringBuilder();

				while (text[i] != '|')
                {
					if (text[i] == '}')
					{
						i--;
						break;
					}
                    answer.Append(text[i]);
                    i++;
                }

                newAnswers.Add(answer.ToString());
                // skip the extra space
                i++;
            }

            // skip the '}' and the extra space
            i += 2;

            newCorrectAnswer = Convert.ToInt32(text[i])-48;

            questions.Add(new Question { question = newQuestion.ToString(), answers = newAnswers, correctAnswer = newCorrectAnswer });

            i++;
		}

        CreateTestPage();
	}
	
	/// <summary> Update is called once per frame
	/// </summary>
	void Update () 
	{
	
	}

    /// <summary> structure to hold the question and answers along with which is the correct answer
    /// </summary>
    public class Question
    {
        public string question
        {
            get;
            set;
        }

        public List<string> answers;

        public int correctAnswer
        {
            get;
            set;
        }

        public Question()
        {
            question = null;
            answers = new List<string>();
        }
    }

    /// <summary> Creates the test page that'll display on the in-game device
    /// </summary>
    private void CreateTestPage()
    {
        int questionCount = 1;

		questions = questions.OrderRandomly ().ToList();

		foreach(Question q in questions)
        {
			GameObject questionObject = Instantiate(Resources.Load ("questionText")) as GameObject;
			questionObject.name = "question" + questionCount;
			questionObject.GetComponent <Text>().text = "\n" + questionCount + ". " + q.question;

			questionObject.GetComponent <Text>().GetComponentInChildren<Text>().tag = "MediumText";

            questionObject.transform.SetParent(testPanel.transform, false);

			questionObject.GetComponent <Text>().rectTransform.localPosition = new Vector3(
				questionObject.GetComponent <Text>().rectTransform.localPosition.x, 
				questionObject.GetComponent <Text>().rectTransform.localPosition.y, 
				0);

			questionObject.GetComponent <Text>().rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
			questionObject.GetComponent <Text>().rectTransform.localScale = new Vector3(1, 1, 1);

			int answerCount = 1;

            foreach (string answer in q.answers)
            {
				GameObject questionAnswerObject = Instantiate(Resources.Load ("answerButton")) as GameObject;
				questionAnswerObject.name = "answer_" + answerCount + "_ForQuestion_" + questionCount;
				questionAnswerObject.GetComponentInChildren<Text>().text = "\t" + Convert.ToChar(answerCount + 96) + ". " + answer;

                questionAnswerObject.GetComponentInChildren<Text>().tag = "MediumText";

                questionAnswerObject.GetComponent<Button>().onClick.AddListener(() => ChangeButtonColor (questionAnswerObject.name));

                questionAnswerObject.transform.SetParent(testPanel.transform, false);

                questionAnswerObject.GetComponent<RectTransform>().localPosition = new Vector3(
					questionAnswerObject.GetComponent<RectTransform>().localPosition.x, 
					questionAnswerObject.GetComponent<RectTransform>().localPosition.y, 
					0);

				questionAnswerObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
				questionAnswerObject.GetComponent<RectTransform>().localScale = (new Vector3(1, 1, 1));
			
				answerCount++;
			}

			questionCount++;
        }
    }

	/// <summary>
	/// Sets the selected button to "disabled". This is how we'll determine if a answer was selected.
	/// Only allows one button to be "disabled".
	/// </summary>
	/// <param name="buttonName">Button name.</param>
	public void ChangeButtonColor(string buttonName)
	{
		if (buttonGroup.Count > 0) 
		{
			buttonGroup.Clear ();
		}

		GameObject buttonObject = GameObject.Find (buttonName);

		GetTestButtonsThatContain (buttonName.Substring (21), testPanel.transform);

		foreach (GameObject go in buttonGroup) 
		{
			if (!go.GetComponent <Button> ().interactable) 
			{
				go.GetComponent <Button> ().interactable = true;
			}
		}

		buttonObject.GetComponent <Button> ().interactable = false;
	}

	/// <summary> Searches the testPanel for the buttons that belong to the same question as the button selected
	/// </summary>
	/// <param name="search">Search.</param>
	/// <param name="parent">Parent.</param>
	public static void GetTestButtonsThatContain(string search, Transform parent)
	{
		if (parent.name.Contains("_ForQuestion_" + search))
		{
			buttonGroup.Add (parent.gameObject);
			return;
		}

		foreach (Transform child in parent) 
		{
			GetTestButtonsThatContain (search, child);
		}
	}

	/// <summary> submits the answers choosen for the test to the DB and sends a module complete message, and tells the DB the user has completed the game
	/// </summary>
	public void Submit()
	{
		int score = 0;
		// TODO: This method will search the test panel for the selected answers (if any) and will figure out the user's score.
		// it will also write to the DB, the score along with the module complete info

		GameObject[] answerButtons = GameObject.FindGameObjectsWithTag ("TestButton");

		foreach (GameObject go in answerButtons) 
		{
			if (!go.GetComponent<Button>().interactable)
			{
				string answerButtonName = go.name;

				int questionNumber = Convert.ToInt32 (answerButtonName.Substring (21));

				int correctAnswerForQuestion = questions[questionNumber-1].correctAnswer;

				int selectedAnswerForQuestion = Convert.ToInt32 (answerButtonName.Substring (7, 1));

				if (correctAnswerForQuestion == selectedAnswerForQuestion)
				{
					score++;
				}
			}
		}

        DialogueManager.Instance.SendMessage("OnSequencerMessage", "Submit");

		DialogueLua.SetVariable ("UserTestScore", score);

        Debug.Log (score); // TODO: replace with DB method or code to save score and module complete status
	}

	/// <summary> Gets the disabled test buttons.
	/// </summary>
	/// <param name="parent">This should be the test panel</param>
	public static void GetDisabledTestButtons(Transform parent)
	{
		if (parent.gameObject.GetComponent<Button> ()) 
		{
			if (parent.gameObject.GetComponent<Button> ().interactable == false)
			{
				selectedAnswerButtons.Add (parent.gameObject);
			}
		}

		foreach (Transform child in parent) 
		{
			GetDisabledTestButtons (child);
		}
	}
}
