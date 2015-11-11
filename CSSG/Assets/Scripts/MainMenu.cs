using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
    public TextAsset tutorialTest;
    public TextAsset hazardsTest;
    public TextAsset sdsTest;
    public TextAsset safetyTest;

    List<Question> questions;

    public GameObject resultsPanel;
    public Text resultsText;

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

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void PopulateResults()
    {
        DataTable userProgress = SQL.GetProgress();

        if (!string.IsNullOrEmpty(userProgress.Rows[0]["TutorialExamScore"].ToString()))
        {
            resultsText.text = string.Empty;
            BuildTestResults("Tutorial");
            AddTestResultsToMenu("Tutorial");
        }

        if (!string.IsNullOrEmpty(userProgress.Rows[0]["HazardExamScore"].ToString()))
        {
            resultsText.text = string.Empty;
            BuildTestResults("Hazards");
            AddTestResultsToMenu("Hazards");
        }

        if (!string.IsNullOrEmpty(userProgress.Rows[0]["SDSExamScore"].ToString()))
        {
            resultsText.text = string.Empty;
            BuildTestResults("SDS");
            AddTestResultsToMenu("SDS");
        }

        if (!string.IsNullOrEmpty(userProgress.Rows[0]["SafetyExamScore"].ToString()))
        {
            resultsText.text = string.Empty;
            BuildTestResults("Safety");
            AddTestResultsToMenu("Safety");
        }
    }

    public void EraseResults()
    {
        foreach (Transform child in resultsPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void StartNewPlaythrough()
	{
        SQL.EraseProgress();
		Application.LoadLevel ("TutorialLoadingScreen");
	}

    public void ContinuePlaythrough()
    {
        DataTable userProgress = SQL.GetProgress();

        if (string.IsNullOrEmpty(userProgress.Rows[0]["TutorialExamScore"].ToString()))
        {
            Application.LoadLevel("Tutorial");
        }
        else if (string.IsNullOrEmpty(userProgress.Rows[0]["HazardExamScore"].ToString()))
        {
            Application.LoadLevel("Hazards");
        }
        else if (string.IsNullOrEmpty(userProgress.Rows[0]["SDSExamScore"].ToString()))
        {
            Application.LoadLevel("SDS");
        }
        else if (string.IsNullOrEmpty(userProgress.Rows[0]["SafetyExamScore"].ToString()))
        {
            Application.LoadLevel("WorkplaceSafety");
        }
        else
        {
            Application.LoadLevel("FinalExam");
        }
    }

    void AddTestResultsToMenu(string moduleName)
    {
        int questionCount = 1;

        GameObject moduleHeader = Instantiate(Resources.Load("questionText")) as GameObject;
        moduleHeader.name = moduleName + "\nResultsHeader\n";
        moduleHeader.GetComponent<Text>().text = moduleName + " Module";
        moduleHeader.GetComponent<Text>().tag = "MediumText";
        moduleHeader.GetComponent<Text>().rectTransform.localPosition = new Vector3(
            moduleHeader.GetComponent<Text>().rectTransform.localPosition.x,
            moduleHeader.GetComponent<Text>().rectTransform.localPosition.y, 0);
        moduleHeader.GetComponent<Text>().rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        moduleHeader.GetComponent<Text>().rectTransform.localScale = new Vector3(1, 1, 1);
        moduleHeader.GetComponent<Text>().fontStyle = FontStyle.Bold;
        moduleHeader.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        moduleHeader.transform.SetParent(resultsPanel.transform, false);

        foreach (Question q in questions)
        {
            GameObject question = Instantiate(Resources.Load("questionText")) as GameObject;
            question.name = "Question" + questionCount;
            question.GetComponent<Text>().text = "Question " + questionCount + ": " + q.question 
                + "\n\t Correct answer: " + q.answers[q.correctAnswer-1] + "\n";
            question.GetComponent<Text>().tag = "SmallText";
            question.GetComponent<Text>().rectTransform.localPosition = new Vector3(
                question.GetComponent<Text>().rectTransform.localPosition.x,
                question.GetComponent<Text>().rectTransform.localPosition.y, 0);
            question.GetComponent<Text>().rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
            question.GetComponent<Text>().rectTransform.localScale = new Vector3(1, 1, 1);
            question.transform.SetParent(resultsPanel.transform, false);

            questionCount++;
        }
    }

    void BuildTestResults(string moduleName)
    {
        string text = null;

        if (moduleName == "Tutorial")
        {
            text = tutorialTest.text;
        }
        else if (moduleName == "Hazards")
        {
            text = hazardsTest.text;
        }
        else if (moduleName == "SDS")
        {
            text = sdsTest.text;
        }
        else if (moduleName == "Safety")
        {
            text = safetyTest.text;
        }

        questions = new List<Question>();

        // the +=2 is for skipping the new line at the end of each question/answer set
        for (int i = 0; i < text.Length; i += 2)
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

            newCorrectAnswer = Convert.ToInt32(text[i]) - 48;

            questions.Add(new Question { question = newQuestion.ToString(), answers = newAnswers, correctAnswer = newCorrectAnswer });

            i++;
        }
    }
}
