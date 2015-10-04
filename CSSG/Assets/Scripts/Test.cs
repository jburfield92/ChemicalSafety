using UnityEngine;
using System.Collections;
using System.IO;

public class Test : MonoBehaviour 
{
	public GameObject testPanel;
	public TextAsset testFile;
	private string text;

	/// <summary> Use this for initialization
	/// </summary>
	void Start () 
	{
		text = testFile.text;

		for (int i = 0; i < text.Length; i++) 
		{
			int questionCount = 1;
			int answerCount = 0;
			int correctAnswer = 0;

			while (text[i] != '|')
			{
				// next character is the correct answer
				if (text[i] == '<')
				{
					i++;
					correctAnswer = text[i];
					i++;
				}
			}

			// we are at the end of the question
			if (answerCount == 0)
			{

			}
            // the last answer for the question
            else if (answerCount == 4)
            {

            }
            else
            {

            }
		}
	}
	
	/// <summary> Update is called once per frame
	/// </summary>
	void Update () 
	{
	
	}
}
