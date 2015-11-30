using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
	public Text timerLabel;

	/// <summary> Use this for initialization
	/// </summary>
	void Start () 
	{
		
	}
	
	/// <summary> Update is called once per frame
	/// </summary>
	void Update () 
	{
		float minutes = Mathf.Floor(Time.timeSinceLevelLoad / 60);
		float seconds = Time.timeSinceLevelLoad % 60;
		timerLabel.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
	}
}
