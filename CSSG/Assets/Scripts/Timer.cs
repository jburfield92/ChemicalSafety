using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour 
{
	public Text timerLabel;
	public static float time = 0f;

	/// <summary> Use this for initialization
	/// </summary>
	void Start () 
	{
		
	}
	
	/// <summary> Update is called once per frame
	/// </summary>
	void Update () 
	{
		//timerLabel.fontSize = 14 * AuxiliaryMethods.fontSizeMultiplier;
		time += Time.deltaTime;

		float minutes = time / 60;
		float seconds = time % 60;
		float fraction = (time * 100) % 100;
		timerLabel.text = string.Format ("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
	}
}
