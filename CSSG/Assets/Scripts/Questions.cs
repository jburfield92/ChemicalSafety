using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// TODO: Use this for generating questions and their answers? Clean up so it's not so hardcoded
/// </summary>
public class Questions : MonoBehaviour
{
    private double right = 0;
    private double wrong2 = 0;
    private int count = 0;
    private int count2 = 0;
    private string[] questions = new string[2];
    private string[][] answers = new string[8][];

    public Text question;
	public Text answerOne;
	public Text answerTwo;
	public Text answerThree;
	public Text answerFour;
	public Text rightText;
	public Text wrongText;
	public Text percent;
	public GameObject setOne;
	public GameObject setTwo;

    /// <summary> Use this for initialization
    /// </summary>
    void Start ()
    {
		answerOne = answerOne.gameObject.GetComponent<Text> ();
		answerTwo = answerTwo.gameObject.GetComponent<Text> ();
		answerThree = answerThree.gameObject.GetComponent<Text> ();
		answerFour = answerFour.gameObject.GetComponent<Text> ();
		rightText = rightText.gameObject.GetComponent<Text> ();
		wrongText = wrongText.gameObject.GetComponent<Text> ();
		percent = percent.gameObject.GetComponent<Text> ();

		question = question.gameObject.GetComponent<Text> ();
		questions [0] = "Match the Font";
		questions [1] = "What shape has 4 sides";
		answers [0] = new string[2];
		answers [1] = new string[2];
		answers [2] = new string[2];
		answers [3] = new string[2];
		answers [4] = new string[2];
		answers [5] = new string[2];
		answers [6] = new string[2];
		answers [7] = new string[2];
		answers [0][1] = "0";
		answers [0][0] = "Does this match maybe not!";

		answers [1][1] = "0";
		answers [1][0] = "Button,Button and Button.";

		answers [2][1] = "0";
		answers [2][0] = "Not this one keep moving.";

		answers [3][1] = "1";
		answers [3][0] = "Right.";

		answers [4][1] = "0";
		answers [4][0] = "Circle";

		answers [5][1] = "1";
		answers [5][0] = "Square";

		answers [6][1] = "0";
		answers [6][0] = "Triangle";

		answers [7][1] = "0";
		answers [7][0] = "Hexagon";

		Load ();
	}

    /// <summary> Loads the questions and answers
    /// </summary>
	void Load()
    {
		if(count >= 2)
        {
			rightText.text = right.ToString();
			wrongText.text = (count - right).ToString();
			percent.text = ((right/wrong2)*100).ToString() + "%";
			setOne.SetActive(!setOne.activeSelf);
			setTwo.SetActive(!setTwo.activeSelf);
			return;
		}

		question.text = questions [count];
		count++;
		wrong2++;
		answerOne.text = answers[count2][0];
		count2++;
		answerTwo.text = answers[count2][0];
		count2++;
		answerThree.text = answers[count2][0];
		count2++;
		answerFour.text = answers[count2][0];
		count2++;
	}

    /// <summary> The button for the first answer
    /// </summary>
	public void ButtonOne()
    {
		if("1" == answers [count2-4][1] ){
			right++;

		}
		Load ();
	}

    /// <summary> The button for the second answer
    /// </summary>
    public void ButtonTwo()
    {
		if("1" == answers [count2-3][1] )
        {
			right++;
		}

		Load ();
	}

    /// <summary> The button for the third answer
    /// </summary>
    public void ButtonThree()
    {
		if("1" == answers [count2-2][1] )
        {
			right++;
		}

		Load ();
	}

    /// <summary> The button for the fourth answer
    /// </summary>
    public void ButtonFour()
    {
		if("1" == answers [count2-1][1] )
        {
			right++;
		}

		Load ();
	}


}
