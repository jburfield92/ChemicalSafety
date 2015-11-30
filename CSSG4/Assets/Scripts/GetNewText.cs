using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetNewText : MonoBehaviour {

	public Text txt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateText(string newtxt)
    {
       
		txt.text = newtxt;
		
	}


}
