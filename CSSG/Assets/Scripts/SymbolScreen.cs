using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SymbolScreen : MonoBehaviour {

    public GameObject Screen;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        GameObject[] temp;
        if (PickupObject.carriedObject == true)
        {
            
            temp = GameObject.FindGameObjectsWithTag("ScreenImage");

           

            foreach (GameObject g in temp)
            {
                if (PickupObject.carriedObject.name == (g.transform.parent.transform.parent.transform.parent.transform.parent.name))
                {

                    
                    Screen.GetComponent<Image>().sprite = g.GetComponent<Image>().sprite;
                    Screen.GetComponent<Image>().enabled = true;
                    
                   
                }

            }


        }
        else
        {
            Screen.GetComponent<Image>().enabled = false;
            //Screen = null;
        }
	
	}
}
